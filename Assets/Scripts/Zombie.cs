using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Zombie : MonoBehaviour, IShootable
{
    [Header("Stats")]
    public int damage;
    public int health;
    public int MaxHealth;
    public AnimationCurve MaxHealthPerWave;
    public AnimationCurve SpeedPerWave;

    public float headOffset, legsOffset;

    public float damageDelay = 1.15f;
    private float lastDamageDone;

    [Header("Animators")]
    public SpriteRenderer zombieSR;
    public Animator zombieHitAnimator;
    public Animator hitAnimator;
    public Transform hitTransform;
    public ParticleSystem diePS;

    [Header("Audio")]
    public AudioClip dieClip;
    public List<AudioClip> hitClips;
    public List<AudioClip> randomClips;
    public AudioSource audioSource;

    [Header("Drops")]
    public HealthPack healthPack;
    public Ammo ammo;
    [Range(0, 100)] public float dropRate = 15f;

    public NavMeshAgent agent;

    private void Start()
    {
        StartCoroutine(MoveToPlayer());

        agent.speed = SpeedPerWave.Evaluate(WaveMaanger.Insntance.CurrentWave);
        MaxHealth = (int)MaxHealthPerWave.Evaluate(WaveMaanger.Insntance.CurrentWave);
        health = MaxHealth;
    }

    public void Hit(int damage)
    {
        if (health == 0) return;

        health = Mathf.Clamp(health - damage, 0, MaxHealth);

        zombieHitAnimator.SetTrigger("hit");

        hitAnimator.Play("ZombieHit");
        hitTransform.localPosition = new Vector2(Random.Range(-0.028f, 0.028f), Random.Range(-0.028f, 0.028f));

        if (health == 0 && !agent.isStopped)
        {
            agent.isStopped = true;
            diePS.Play();

            zombieSR.DOFade(0, 0.45f).SetEase(Ease.OutSine);
            transform.DOLocalMove(transform.localPosition - transform.forward * 1.18f, 1.2f).SetEase(Ease.OutQuint).OnComplete(() =>
            {
                Drop();

                UIManager.Instance.GameView.WaveUI.UpdateZombiesLeft();
                GameManager.Instance.CurrentGameData.totalZombiesKilled++;
                audioSource.clip = dieClip;
                audioSource.Play();

                gameObject.SetActive(false);
            });
        }
        else
        {
            audioSource.clip = hitClips[Random.Range(0, hitClips.Count)];
            if (!audioSource.isPlaying) audioSource.Play();
        }
    }

    private void Drop()
    {
        if (Random.Range(0, 100) > dropRate) return;

        var drop = Random.Range(0, 100) > 50 ? ammo.gameObject : healthPack.gameObject; 
        Instantiate(drop, transform.position, transform.localRotation);
    }

    private IEnumerator MoveToPlayer()
    {
        while (health > 0)
        {
            agent.SetDestination(PlayerManager.Instance.transform.position);

            if (!audioSource.isPlaying)
            {
                if (Random.Range(0, 100) < 10)
                {
                    audioSource.clip = randomClips[Random.Range(0, hitClips.Count)];
                    audioSource.Play();
                }
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (lastDamageDone + damageDelay > Time.time) return;

        lastDamageDone = Time.time;

        PlayerManager.Instance.PlayerController.TakeDamage(damage);
    }

    public int GetPointForHit()
    {
        return 10;
    }

    public int GetPointForKill()
    {
        return 70;
    }

    public bool IsDead()
    {
        return health == 0;
    }

    public BodyPart GetBodyPart(Vector3 position)
    {
        if (position.y > transform.position.y + headOffset)
            return BodyPart.Head;

        if (position.y < transform.position.y - legsOffset) 
            return BodyPart.Legs;

        return BodyPart.Chest;
    }
}
