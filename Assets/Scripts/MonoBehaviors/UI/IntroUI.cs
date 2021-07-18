using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    public Button PlayButton;
    public TextMeshProUGUI BestScore;

    void Start()
    {
        PlayButton.onClick.AddListener(() =>
        {
            Hide();
            GameManager.Instance.ResetGame();
            GameManager.Instance.UpdateState(GameManager.GameStates.RUNNING);
            UIManager.Instance.Inventory.Show();
            UIManager.Instance.TimerUI.Show();
            UIManager.Instance.PauseButton.Show();
            SoundManager.Instance.PlayMusic(Random.Range(0, 1));
        });
    }
    public void Show()
    {
        gameObject.SetActive(true);
        BestScore.text = "Best Score\n" + GameManager.Instance.GetBestScore();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
