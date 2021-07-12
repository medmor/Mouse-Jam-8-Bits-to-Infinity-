using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    public Button PlayButton;
    void Start()
    {
        PlayButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetGame();
            Hide();
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
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
