using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Coins;
    public Image HealthBar;
    public Image ShieldBar;
    public Image MagnetBar;
    void Start()
    {
        Score.text = "Score : 0";
        Coins.text = "Coins : 0";
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void SetScore(int score)
    {
        Score.text = "Score : " + score;
    }
    public void SetCoin(int coin)
    {
        Coins.text = "Coins : " + coin;
    }
    public void SetHealthBar(float fill)
    {
        HealthBar.fillAmount = fill;
    }
    public void ShowShieldBar()
    {
        ShieldBar.transform.parent.gameObject.SetActive(true);
    }
    public void HideShieldBar()
    {
        ShieldBar.transform.parent.gameObject.SetActive(false);
    }
    public void SetShieldBar(float fill)
    {
        ShieldBar.fillAmount = fill;
    }
    public void ShowMagnetBar()
    {
        MagnetBar.transform.parent.gameObject.SetActive(true);
    }
    public void HideMagnetBar()
    {
        MagnetBar.transform.parent.gameObject.SetActive(false);
    }
    public void SetMagnetBar(float fill)
    {
        MagnetBar.fillAmount = fill;
    }
    public void HideEquipement(Equipement.AvailableEquipement equipement)
    {
        switch (equipement)
        {
            case Equipement.AvailableEquipement.SHIELD:
                HideShieldBar();
                break;
            case Equipement.AvailableEquipement.MAGNET:
                HideMagnetBar();
                break;
        }
    }
    public void ShowEquipement(Equipement.AvailableEquipement equipement)
    {
        switch (equipement)
        {
            case Equipement.AvailableEquipement.SHIELD:
                ShowShieldBar();
                break;
            case Equipement.AvailableEquipement.MAGNET:
                ShowMagnetBar();
                break;
        }
    }
    public void SetEquipement(Equipement.AvailableEquipement equipement, float fill)
    {
        switch (equipement)
        {
            case Equipement.AvailableEquipement.SHIELD:
                SetShieldBar(fill);
                break;
            case Equipement.AvailableEquipement.MAGNET:
                SetMagnetBar(fill);
                break;
        }
    }
}
