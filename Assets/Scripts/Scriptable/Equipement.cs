using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipement", menuName = "Equipement")]
public class Equipement : ScriptableObject
{
    public float BaseDuration = 2;
    public GameObject EquipementPref;
    public bool IsActif = false;
    public AvailableEquipement EquipementType;

    private float Duration;
    private GameObject equipementGameObject;
    public void Equipe(Player player)
    {
        if (IsActif)
        {
            Duration += BaseDuration;
        }
        else
        {
            IsActif = true;
            UIManager.Instance.Inventory.ShowEquipement(EquipementType);//To refactor
            equipementGameObject = Instantiate(EquipementPref);
            equipementGameObject.transform.parent = player.transform;
            equipementGameObject.transform.localPosition = Vector3.zero;
            Duration = BaseDuration;
            player.StartCoroutine(CountDonwn());
        }
    }
    public void Unequipe()
    {
        UIManager.Instance.Inventory.HideEquipement(EquipementType);//To refactor
        IsActif = false;
        Duration = BaseDuration;
        Destroy(equipementGameObject);
    }
    public IEnumerator CountDonwn()
    {
        while (Duration > 0)
        {
            Duration -= .025f;
            UIManager.Instance.Inventory.SetEquipement(EquipementType, Duration / (5 * BaseDuration));//To refactor
            yield return new WaitForSeconds(.1f);
        }
        Unequipe();
    }
    public enum AvailableEquipement
    {
        SHIELD, MAGNET
    }
}
