using Gameplay.Towers.MVP;

namespace Gameplay.Towers.Strategies
{
    public interface ITowerCooldownStrategy
    {
        public void Cooldown(ref float cooldown, float deltaTime);
    }
}