using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public Button Close;
    public Button Quit;
    public Button Musics;
    public Button Sounds;
    public Sprite DisabledSounds;
    public Sprite EnabledSounds;
    public Sprite DisabledMusics;
    public Sprite EnabledMusics;

    public Image MusicsImage;
    public Image SoundsImage;

    private void Start()
    {
        Close.onClick.AddListener(OnCloseButtonClick);
        Quit.onClick.AddListener(OnQuitButtonClick);
        Musics.onClick.AddListener(OnMusicButtonClick);
        Sounds.onClick.AddListener(OnSoundsButtonClick);

        MusicsImage.sprite = EnabledMusics;
        SoundsImage.sprite = EnabledSounds;
    }

    internal void Show()
    {
        gameObject.SetActive(true);
    }
    internal void Hide()
    {
        gameObject.SetActive(false);
    }
    void OnCloseButtonClick()
    {
        GameManager.Instance.UpdateState(GameManager.GameStates.RUNNING);
        Hide();
    }
    void OnQuitButtonClick()
    {
        Application.Quit();
    }
    void OnMusicButtonClick()
    {
        SoundManager.Instance.ToggleMusicsMute();
        if (MusicsImage.sprite == EnabledMusics)
            MusicsImage.sprite = DisabledMusics;
        else
            MusicsImage.sprite = EnabledMusics;

    }
    void OnSoundsButtonClick()
    {
        SoundManager.Instance.ToggleEffectsMute();
        if (SoundsImage.sprite == EnabledSounds)
            SoundsImage.sprite = DisabledSounds;
        else
            SoundsImage.sprite = EnabledSounds;
    }
}
