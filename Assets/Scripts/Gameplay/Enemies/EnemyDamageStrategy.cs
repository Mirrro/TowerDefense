namespace Gameplay.Enemies
{
    public class EnemyDamageStrategy : IEnemyDamageStrategy
    {
        public void ReceiveDamage(ref int health, int damage)
        {
            health -= damage;
        }
    }
}