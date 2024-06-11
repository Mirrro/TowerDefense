using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text namefield;
    [SerializeField] private TMP_Text pricefield;
    [SerializeField] private Button button;

    public Button Button => button;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetName(string text)
    {
        namefield.text = text;
    }

    public void SetPrice(string price)
    {
        pricefield.text = price;
    }
}
