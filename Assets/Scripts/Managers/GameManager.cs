using Cinemachine;
using System.Collections;
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
    public void CalculatScore(int coins, int score)
    {
        StartCoroutine(CalculatScoreCoroutine(coins, score));
    }
    IEnumerator CalculatScoreCoroutine(int coins, int score)
    {
        while (coins >= 0)
        {

            SoundManager.Instance.PlayEffects("Coin");
            UIManager.Instance.Inventory.SetCoin(coins--);
            UIManager.Instance.Inventory.SetScore(score += 10);
            UIManager.Instance.EndUI.SetScoreText(score);
            yield return new WaitForSeconds(.02f);
        }
        while (UIManager.Instance.TimerUI.GetTime() != "00:00")
        {
            SoundManager.Instance.PlayEffects("Coin");
            UIManager.Instance.TimerUI.SetTime(-1);
            UIManager.Instance.Inventory.SetScore(score += 10);
            UIManager.Instance.EndUI.SetScoreText(score);
            yield return new WaitForSeconds(.02f);
        }
    }
    public enum GameStates
    {
        RUNNING,
        PAUSED
    }
}
[System.Serializable] public class VoidEvent : UnityEvent { }
[System.Serializable] public class Vector3Event : UnityEvent<Vector3, Vector3> { }