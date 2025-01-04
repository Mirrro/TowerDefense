namespace Gameplay.Enemies
{
    public interface IEnemyEffect
    {
        void Apply(EnemyPresenter enemyPresenter);
        void Cancel();
    }
}