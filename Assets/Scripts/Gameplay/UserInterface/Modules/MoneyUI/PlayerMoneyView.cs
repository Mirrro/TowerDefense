using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerMoneyView : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyTextfield;
    private Sequence sequence;
    
    public void UpdateMoneyTextfield(string text)
    {
        moneyTextfield.text = text;
    }

    public void FlashRed()
    {
        sequence?.Kill(true);
        sequence = DOTween.Sequence();
        sequence.Join(moneyTextfield.DOColor(Color.red, .3f).SetLoops(2, LoopType.Yoyo));
        sequence.Join(moneyTextfield.rectTransform.DOPunchScale(Vector3.one * .2f, .3f));
    }
    
    public void FlashWhite()
    {
        sequence?.Kill(true);
        sequence = DOTween.Sequence();
        sequence.Join(moneyTextfield.DOColor(Color.white, .3f).SetLoops(2, LoopType.Yoyo));
        sequence.Join(moneyTextfield.rectTransform.DOPunchScale(Vector3.one * .2f, .3f));
    }
}