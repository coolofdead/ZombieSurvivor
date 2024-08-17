using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveMaanger : MonoBehaviour
{
    public static WaveMaanger Insntance;

    public Map map;

    public int CurrentWave = 1;
    public Zombie zombiePrefab;

    private List<Zombie> zombies = new();

    [Header("Wave Data")]
    public int defaultTotalZombie = 3;
    public int extraZombiePerWave = 3;
    public int totalMaxZombie = 100;

    public float inBetweenWaveDelay = 8;
    public float minSpawnDelay = 0, maxSpawnDelay = 3;
    public float delayReducePerWave = 0.1f;

    public int TotalZombieToSpawn => Mathf.Clamp(defaultTotalZombie + CurrentWave * extraZombiePerWave, 0, totalMaxZombie);
    public int ZombiesLeft => zombies.Count(zombie => zombie.health > 0);

    private void Awake()
    {
        Insntance = this;
    }

    public void StartWave()
    {
        UIManager.Instance.GameView.WaveUI.UpdateWave();

        StartCoroutine(HandleSpawningWave());
    }

    private void FinishWave()
    {
        CurrentWave++;

        StartWave();
    }

    private void CheckWaveFinished()
    {
        UIManager.Instance.GameView.WaveUI.UpdateZombiesLeft();

        var isWaveFinished = zombies.All(zombie => zombie.health == 0);

        if (isWaveFinished)
        {
            map.OnWaveFinished(CurrentWave);
            Invoke(nameof(FinishWave), inBetweenWaveDelay);
        }
    }

    IEnumerator HandleSpawningWave()
    {
        for (int i = 0; i < TotalZombieToSpawn; i++)
        {
            var nearestSpawnPoses = map.zombieSpawnPoses.OrderBy(spawnPos => Vector3.Distance(spawnPos.position, PlayerManager.Instance.transform.position)).Take(3).ToArray();
            var spawnPos = nearestSpawnPoses[Random.Range(0, 3)];

            var zombie = Instantiate(zombiePrefab, spawnPos.position, spawnPos.localRotation, map.zombieParent);
            zombie.onDie += CheckWaveFinished;

            zombies.Add(zombie);

            var spawnDelay = Random.Range(minSpawnDelay, Mathf.Clamp(maxSpawnDelay - delayReducePerWave * CurrentWave, 0, maxSpawnDelay));

            UIManager.Instance.GameView.WaveUI.UpdateZombiesLeft();

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
