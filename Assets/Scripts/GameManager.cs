using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum State { MainMenu, Playing, EndGame }

    public State CurrentState = State.MainMenu;

    [Header("Intro")]
    public GameObject mapIntro;
    public CharacterController playerCharacterController;
    public Animator transitionAnimator;
    public AudioClip menuThemeClip;

    [Header("Game")]
    public Vector3 startGamePos;
    public AudioClip[] gameThemeClips;

    public void Start()
    {
        Instance = this;

        ChangeState(State.MainMenu);
    }

    public void ChangeState(State newState)
    {
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
        PlayerManager.Instance.transform.position = Vector3.zero;
        mapIntro.SetActive(true);
    }

    private void HandlePlayState()
    {
        transitionAnimator.Play("TransitionMenuGame");

        DOTween.Sequence()
               .AppendInterval(5)
               .AppendCallback(() =>
               {
                   playerCharacterController.enabled = true;

                   PlayerManager.Instance.transform.position = startGamePos;
                   PlayerManager.Instance.InitPlayer();
                   AudioManager.Instance.PlayTheme(menuThemeClip);
                   WaveMaanger.Insntance.StartWave();

                   mapIntro.SetActive(false);
               })
               .Play();
    }

    private void HandleEndGameState()
    {
        playerCharacterController.enabled = false;
    }
}
