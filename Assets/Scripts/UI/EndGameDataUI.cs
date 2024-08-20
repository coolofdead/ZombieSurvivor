using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndGameDataUI : MonoBehaviour
{
    public float delayBeforeEarlySkip = 3f;
    public float waitTimeBeforeContinuing = 12f;

    public TMP_Text endGameTMP;
    public TMP_Text continueTMP;
    public Image continueFillBar;
    public Image endGameTransition;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new();
        controls.Player.Shoot.performed += _ => Continue();
    }

    public void ShowEndGameData(GameData gameData)
    {
        gameObject.SetActive(true);

        StartCoroutine(ShowEndGameDataProgress(gameData));
    }

    IEnumerator ShowEndGameDataProgress(GameData gameData)
    {
        endGameTMP.text = $"You survived : 0 waves\nagainst a total of : 0 zombies\nfor a total of : 0 damage\nfor : 0 bullets shot\nwith : 0 headhsot";

        yield return new WaitForSeconds(1.25f);

        for (int i = 0; i <= gameData.finalWave; i++)
        {
            endGameTMP.text = $"You survived : {i} waves\nagainst a total of : 0 zombies\nfor a total of : 0 damage\nfor : 0 bullets shot\nwith : 0 headhsot";

            yield return new WaitForSeconds(0.03f);
        }

        yield return new WaitForSeconds(0.65f);

        for (int i = 0; i <= gameData.totalZombiesKilled; i++)
        {
            endGameTMP.text = $"You survived : {gameData.finalWave} waves\nagainst a total of : {i} zombies\nfor a total of : 0 damage\nfor : 0 bullets shot\nwith : 0 headhsot";

            yield return new WaitForSeconds(0.012f);
        }

        for (int i = 0; i <= gameData.totalDamageDealt; i++)
        {
            endGameTMP.text = $"You survived : {gameData.finalWave} waves\nagainst a total of : {gameData.totalZombiesKilled} zombies\nfor a total of : {i} damage\nfor : 0 bullets shot\nwith : 0 headhsot";

            yield return new WaitForSeconds(0.001f);
        }

        yield return new WaitForSeconds(0.65f);

        for (int i = 0; i <= gameData.totalBulletsShot; i++)
        {
            endGameTMP.text = $"You survived : {gameData.finalWave} waves\nagainst a total of : {gameData.totalZombiesKilled} zombies\nfor a total of : {gameData.totalDamageDealt} damage\nfor : {i} bullets shot\nwith : 0 headhsot";

            yield return new WaitForSeconds(0.008f);
        }

        yield return new WaitForSeconds(0.65f);

        for (int i = 0; i <= gameData.totalHeadshot; i++)
        {
            endGameTMP.text = $"You survived : {gameData.finalWave} waves\nagainst a total of : {gameData.totalZombiesKilled} zombies\nfor a total of : {gameData.totalDamageDealt} damage\nfor : {gameData.totalBulletsShot} bullets shot\nwith : {i} headhsot";

            yield return new WaitForSeconds(0.01f);
        }

        continueFillBar.DOFillAmount(1, waitTimeBeforeContinuing).SetEase(Ease.OutSine).SetDelay(delayBeforeEarlySkip)
                        .OnStart(() => controls.Enable())
                        .OnComplete(() => continueTMP.DOFade(1, 0.65f).SetEase(Ease.OutSine));
    }

    private void Continue()
    {
        if (continueFillBar.fillAmount < 1)
        {
            continueFillBar.DOKill();
            continueFillBar.fillAmount = 1;
            continueTMP.color = Color.white;
        }
        else
        {
            controls.Disable();

            endGameTransition.DOFade(1, 0.65f).SetEase(Ease.InCubic).OnComplete(() =>
            {
                gameObject.SetActive(false);

                GameManager.Instance.ChangeState(GameManager.State.MainMenu);
            });
        }
    }
}
