using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveMaanger : MonoBehaviour
{
    public static WaveMaanger Insntance;

    public Map map;

    public int CurrentWave = 1;
    public ZombieWaveData[] zombiesWaveData;

    private List<Zombie> zombies = new();

    [Header("Wave Data")]
    public int defaultTotalZombie = 3;
    public int extraZombiePerWave = 3;
    public int totalMaxZombie = 100;

    public AnimationCurve inBetweenWaveDelay;
    public AnimationCurve minSpawnDelay, maxSpawnDelay;

    public int TotalZombieToSpawn => Mathf.Clamp(defaultTotalZombie + CurrentWave * extraZombiePerWave, 0, totalMaxZombie);
    public int ZombiesLeft => zombies.Count(zombie => zombie.health > 0);

    private System.Random rnd;

    private void Awake()
    {
        Insntance = this;
        rnd = new();
    }

    public void StartWave()
    {
        UIManager.Instance.GameView.WaveUI.UpdateWave();

        StartCoroutine(HandleSpawningWave());
    }

    private void FinishWave()
    {
        CurrentWave++;
        GameManager.Instance.CurrentGameData.finalWave = CurrentWave;

        StartWave();
    }

    public void ClearZombies()
    {
        foreach (var zombie in zombies)
        {
            zombie.Hit(zombie.MaxHealth);
        }

        zombies.Clear();
    }

    IEnumerator HandleSpawningWave()
    {
        for (int i = 0; i < TotalZombieToSpawn; i++)
        {
            var nearestSpawnPoses = map.zombieSpawnPoses.OrderBy(spawnPos => Vector3.Distance(spawnPos.position, PlayerManager.Instance.transform.position)).Take(5).ToArray();
            var spawnPos = nearestSpawnPoses[Random.Range(0, 3)];

            var nextZombieRate = rnd.Next(0, 100);
            var zombieWaveData = zombiesWaveData.Where(zombieWaveData => zombieWaveData.startWaveSpawn <= CurrentWave).First(zombieWaveData => nextZombieRate <= zombieWaveData.spawnRateChance);
            var zombie = Instantiate(zombieWaveData.zombiePrefab, spawnPos.position, spawnPos.localRotation, map.zombieParent);

            zombies.Add(zombie);

            var spawnDelay = Random.Range(minSpawnDelay.Evaluate(CurrentWave), maxSpawnDelay.Evaluate(CurrentWave));

            UIManager.Instance.GameView.WaveUI.UpdateZombiesLeft();

            yield return new WaitForSeconds(spawnDelay);
        }

        yield return new WaitWhile(() => !zombies.All(zombie => zombie.health == 0));

        map.OnWaveFinished(CurrentWave);

        ClearZombies();
        Invoke(nameof(FinishWave), inBetweenWaveDelay.Evaluate(CurrentWave));
    }

    [System.Serializable]
    public struct ZombieWaveData
    {
        public Zombie zombiePrefab;
        public int startWaveSpawn;
        [Range(0, 100)] public float spawnRateChance;
    }
}
