using System;

public class PlayerHealth
{
    public event Action<int> HealthChanged;
    public int Health
    {
        get => health;
        private set
        {
            health = value;
            HealthChanged?.Invoke(health);
        }
    }

    private int health = 0;

    public void AddHealth(int amount)
    {
        Health += amount;
    }

    public void RemoveHealth(int amount)
    {
        Health -= amount;
    }
}