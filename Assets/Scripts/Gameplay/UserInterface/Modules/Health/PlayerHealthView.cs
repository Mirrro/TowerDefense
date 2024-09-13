using TMPro;
using UnityEngine;

namespace Gameplay.UserInterface.Modules.Health
{
    public class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textfield;

        public void DisplayHealth(int heath)
        {
            textfield.text = heath.ToString();
        }
    }
}