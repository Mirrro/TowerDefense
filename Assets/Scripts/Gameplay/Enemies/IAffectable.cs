namespace Gameplay.Enemies
{
    public interface IAffectable
    {
        void AddEffect(IEnemyEffect effect);
        void RemoveEffect(IEnemyEffect effect);
    }
}