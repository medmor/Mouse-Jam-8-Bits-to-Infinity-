using System.Collections;
using UnityEngine;

public class EquipementCollect : MonoBehaviour
{
    public Equipement.AvailableEquipement ThisEquipement;

    private void Start()
    {
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            var player = collision.GetComponent<Player>();
            if (ThisEquipement == Equipement.AvailableEquipement.SHIELD)
                player.PlayerDefinition.Shield.Equipe(player);
            else if (ThisEquipement == Equipement.AvailableEquipement.MAGNET)
                player.PlayerDefinition.Magnet.Equipe(player);
        }
    }
}
