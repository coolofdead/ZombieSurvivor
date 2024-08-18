using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum State { MainMenu, Playing, EndGame }

    public State CurrentState = State.MainMenu;
    public GameData CurrentGameData;
    public bool IsGamePaused;

    [Header("Intro")]
    public GameObject mapIntro;
    public CharacterController playerCharacterController;
    public Animator transitionAnimator;
    public AudioClip menuThemeClip;

    [Header("Game")]
    public Map map;
    public Vector3 startGamePos;
    public AudioClip[] gameThemeClips;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        ChangeState(State.MainMenu);
    }

    public void ChangeState(State newState)
    {
        if (newState == CurrentState) return;

        CurrentState = newState;

        switch (newState)
        {
            case State.MainMenu:
                HandleMenuState();
                break;
            case State.Playing:
                HandlePlayState();
                break;
            case State.EndGame:
                HandleEndGameState();
                break;
        }
    }

    private void HandleMenuState()
    {
        playerCharacterController.enabled = false;

        AudioManager.Instance.PlayTheme(menuThemeClip);
        PlayerManager.Instance.transform.localPosition = Vector3.zero;
        mapIntro.SetActive(true);
    }

    private void HandlePlayState()
    {
        transitionAnimator.Play("TransitionMenuGame");

        DOTween.Sequence()
               .AppendInterval(7)
               .AppendCallback(() =>
               {
                   playerCharacterController.enabled = true;

                   PlayerManager.Instance.transform.localPosition = startGamePos;
                   PlayerManager.Instance.InitPlayer();
                   AudioManager.Instance.PlayTheme(gameThemeClips);
                   WaveMaanger.Insntance.StartWave();

                   map.UnlockWeaponBox(false);
                   mapIntro.SetActive(false);

                   CurrentGameData = new();
               })
               .Play();
    }

    private void HandleEndGameState()
    {
        playerCharacterController.enabled = false;
        UIManager.Instance.EndGameView.EndGameDataUI.ShowEndGameData(CurrentGameData);
        WaveMaanger.Insntance.ClearZombies();
    }
}

public struct GameData
{
    public int finalWave;
    public int totalZombiesKilled;
    public int totalBulletsShot;
    public int totalHeadshot;
}
