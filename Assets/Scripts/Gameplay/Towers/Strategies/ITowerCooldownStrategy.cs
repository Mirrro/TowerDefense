using Gameplay.Towers.MVP;

namespace Gameplay.Towers.Strategies
{
    public interface ITowerCooldownStrategy
    {
        public void Initialize(TowerPresenter towerPresenter);
        public bool IsCooldown { get; }
        public void Cooldown();
    }
}