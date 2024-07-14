using System;
using Zenject;

public class DefeatCondition : IInitializable
{
    [Inject] private PlayerHealth playerHealth;

    public event Action Defeat;

    public void Initialize()
    {
        playerHealth.HealthChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(int health)
    {
        if (health <= 0)
        {
            Defeat?.Invoke();
        }
    }
}