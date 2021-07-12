using System.Collections;
using TMPro;
using UnityEngine;

public class BonusScore : MonoBehaviour
{
    public TextMeshProUGUI BonusText;
    public Transform TargetPos;
    private Vector3 StartPos;

    private void Start()
    {
        StartPos = transform.position;
        gameObject.SetActive(false);
    }

    public void Show(string bonus)
    {
        Hide();
        BonusText.text = bonus;
        BonusText.gameObject.SetActive(true);
        StartCoroutine(AnimBonusText());

    }
    public void Hide()
    {
        BonusText.transform.position = StartPos;
        BonusText.gameObject.SetActive(false);
    }
    IEnumerator AnimBonusText()
    {
        var max = Vector3.Distance(transform.position, TargetPos.position);
        while (Vector3.Distance(transform.position, TargetPos.position) > max / 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPos.position, max / 70);
            yield return new WaitForFixedUpdate();
        }
        Hide();
    }
}
