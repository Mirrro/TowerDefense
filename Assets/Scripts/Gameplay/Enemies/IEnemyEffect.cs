namespace Gameplay.Enemies
{
    public interface IEnemyEffect
    {
        void Apply(IEnemyPresenter enemyPresenter);
        void Cancel();
    }
}