using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Manager<GameManager>
{
    internal VoidEvent PlayerKilled { get; set; } = new VoidEvent();
    internal Vector3Event FloorExtended { get; set; } = new Vector3Event();

    internal GameStates CurrentState;

    public GameObject[] SystemPrefabs;

    public GameObject PlayerPref;

    public GameObject GroundPref;
    public EnemiesGenerator EnemiesGeneratorPref;
    public CollectablesGenerator CollectablesGeneratorPref;
    public WorldElementsGenerator WorldElementsGenerator;




    private void Start()
    {
        InitSystemPrefabs();
        UIManager.Instance.IntroUI.Show();
    }
    void InitSystemPrefabs()
    {
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            Instantiate(SystemPrefabs[i]);
        }
    }
    public void UpdateState(GameStates state)
    {
        if (state == CurrentState)
            return;
        SoundManager.Instance.TogglePauseMusic();
        switch (state)
        {
            case GameStates.RUNNING:
                Time.timeScale = 1;
                break;
            case GameStates.PAUSED:
                Time.timeScale = 0;
                break;
        }
        CurrentState = state;
    }
    public void ResetGame()
    {
        Instantiate(EnemiesGeneratorPref);
        Instantiate(CollectablesGeneratorPref);
        Instantiate(WorldElementsGenerator);
        var Ground = Instantiate(GroundPref);

        var player = Instantiate(PlayerPref);
        player.transform.position = Ground.transform.position + Vector3.up;

        Camera.main.GetComponentInChildren<CinemachineVirtualCamera>().Follow = player.transform;
        UIManager.Instance.TimerUI.ResetTimer();
    }

    #region Save
    private string BestScoreString = "BestScore";
    public void SetBestScore(int score)
    {
        if (score > GetBestScore())
        {
            PlayerPrefs.SetInt(BestScoreString, score);
            PlayerPrefs.Save();
        }
    }
    public int GetBestScore()
    {
        if (PlayerPrefs.HasKey(BestScoreString))
            return PlayerPrefs.GetInt("BestScore");
        return 0;
    }
    #endregion
    public enum GameStates
    {
        RUNNING,
        PAUSED
    }


}
[System.Serializable] public class VoidEvent : UnityEvent { }
[System.Serializable] public class Vector3Event : UnityEvent<Vector3, Vector3> { }