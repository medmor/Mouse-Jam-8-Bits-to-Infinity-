using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public Button PlayButton;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI BestScore;
    bool waitingScoreCalculation = true;
    void Start()
    {
        PlayButton.onClick.AddListener(() =>
        {
            if (waitingScoreCalculation)
            {
                waitingScoreCalculation = false;
            }
            else
            {
                GameManager.Instance.ResetGame();
                GameManager.Instance.UpdateState(GameManager.GameStates.RUNNING);
                UIManager.Instance.Inventory.Show();
                UIManager.Instance.Inventory.SetScore(0);
                UIManager.Instance.Inventory.SetHealthBar(1);
                UIManager.Instance.TimerUI.Show();
                UIManager.Instance.PauseButton.Show();
                SoundManager.Instance.TogglePauseMusic();
                Hide();
            }
        });
    }
    public void Show(int coins, int score)
    {
        gameObject.SetActive(true);
        BestScore.text = "Best Score\n" + GameManager.Instance.GetBestScore();
        waitingScoreCalculation = true;
        CalculatScore(coins, score);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetScoreText(int t)
    {
        Score.text = "Score\n" + t;
    }
    public void SetBestScoreText(string t)
    {
        BestScore.text = "Best Score\n" + t;
    }
    public void CalculatScore(int coins, int score)
    {
        StartCoroutine(CalculatScoreCoroutine(coins, score));
    }
    IEnumerator CalculatScoreCoroutine(int coins, int score)
    {

        while (coins >= 100)
        {

            SoundManager.Instance.PlayEffects("Coin");
            UIManager.Instance.Inventory.SetCoin(coins--);
            UIManager.Instance.Inventory.SetScore(score += 10);
            UIManager.Instance.EndUI.SetScoreText(score);
            if (waitingScoreCalculation)
                yield return new WaitForSeconds(.02f);

        }
        while (UIManager.Instance.TimerUI.GetTime() != "00:00")
        {
            SoundManager.Instance.PlayEffects("Coin");
            UIManager.Instance.TimerUI.SetTime(-1);
            UIManager.Instance.Inventory.SetScore(score += 10);
            UIManager.Instance.EndUI.SetScoreText(score);
            if (waitingScoreCalculation)
                yield return new WaitForSeconds(.02f);

        }
        if (waitingScoreCalculation)
            waitingScoreCalculation = false;
        GameManager.Instance.SetBestScore(score);
    }
}
