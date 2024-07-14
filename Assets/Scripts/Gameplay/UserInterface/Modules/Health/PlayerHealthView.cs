using TMPro;
using UnityEngine;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private TMP_Text textfield;

    public void DisplayHealth(int heath)
    {
        textfield.text = heath.ToString();
    }
}