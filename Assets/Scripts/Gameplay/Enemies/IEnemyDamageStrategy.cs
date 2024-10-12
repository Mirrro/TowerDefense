namespace Gameplay.Enemies
{
    public interface IEnemyDamageStrategy
    {
        public void ReceiveDamage(ref int health, int damage);
    }
}