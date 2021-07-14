using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndUI : MonoBehaviour
{
    public Button PlayButton;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI BestScore;
    void Start()
    {
        PlayButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetGame();
            GameManager.Instance.UpdateState(GameManager.GameStates.RUNNING);
            UIManager.Instance.Inventory.Show();
            UIManager.Instance.TimerUI.Show();
            UIManager.Instance.PauseButton.Show();
            UIManager.Instance.Inventory.SetScore(0);
            SoundManager.Instance.TogglePauseMusic();
            Hide();
        });
    }
    public void Show()
    {
        gameObject.SetActive(true);
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
}
