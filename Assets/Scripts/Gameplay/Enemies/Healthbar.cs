using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Enemies
{
    public class Healthbar : MonoBehaviour
    {
        private static readonly int FillAmount = Shader.PropertyToID("_FillAmount");
        private RectTransform rectTransform;
        [SerializeField] private Image image;
        private Camera mainCamera;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            image.material = new Material(image.material);
            mainCamera = Camera.main;
        }

        private void Start()
        {
            SetHealth(1);
        }

        public void SetHealth(float percentage)
        {
            Debug.Log(percentage);
            float fillAmount = (percentage - 0.5f) * rectTransform.rect.width;
            image.material.SetFloat(FillAmount, fillAmount);
        }
        
        private void Update()
        {
            // Make the health bar look at the main camera
            LookAtCamera();
        }
        
        private void LookAtCamera()
        {
            if (mainCamera != null)
            {
                // Ensure the health bar always faces the camera
                // Get direction from health bar to camera
                Vector3 directionToCamera = transform.position - mainCamera.transform.position;

                // Update the health bar's rotation to face the camera
                transform.rotation = Quaternion.LookRotation(-directionToCamera, Vector3.up);
            }
        }
    }
}