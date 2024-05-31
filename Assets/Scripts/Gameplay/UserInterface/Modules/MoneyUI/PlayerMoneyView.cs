using TMPro;
using UnityEngine;

public class PlayerMoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyTextfield;

    public void UpdateMoneyTextfield(string text)
    {
        moneyTextfield.text = text;
    }
}