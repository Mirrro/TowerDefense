using System;

namespace Gameplay.Player
{
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
            if (amount <= 0)
            {
                return;
            }
        
            Coins += amount;
        }

        public void RemoveMoney(int amount)
        {
            if (amount <= 0)
            {
                return;
            }
        
            Coins -= amount;
        }
    }
}