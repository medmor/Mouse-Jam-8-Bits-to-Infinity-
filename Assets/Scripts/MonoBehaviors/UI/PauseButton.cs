using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public Button PauseButtonUI;
    void Start()
    {
        PauseButtonUI.onClick.AddListener(() =>
        {
            GameManager.Instance.UpdateState(GameManager.GameStates.PAUSED);
            UIManager.Instance.PauseMenu.Show();
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
