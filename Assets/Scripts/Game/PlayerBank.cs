using System;

public class PlayerBank
{
    public event Action<int> CoinsChanged; 
    public int Coins
    {
        get => coins;
        private set
        {
            coins = value;
            CoinsChanged?.Invoke(coins);
        }
    }

    private int coins = 0;

    public void AddMoney(int amount)
    {
        Coins += amount;
    }

    public void RemoveMoney(int amount)
    {
        Coins -= amount;
    }
}