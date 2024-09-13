using Gameplay.Player;
using Zenject;

namespace Gameplay.UserInterface.Modules.Health
{
    public class PlayerHealthPresenter : IInitializable
    {
        private readonly PlayerHealth playerHealth;
        private readonly PlayerHealthView view;
        private readonly PlayerHealthModel model;

        public PlayerHealthPresenter(PlayerHealthView view, PlayerHealthModel model, PlayerHealth playerHealth)
        {
            this.view = view;
            this.model = model;
            this.playerHealth = playerHealth;
        }

        public void Initialize()
        {
            playerHealth.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged(int health)
        {
            model.Health = health;
            view.DisplayHealth(model.Health);
        }
    }
}