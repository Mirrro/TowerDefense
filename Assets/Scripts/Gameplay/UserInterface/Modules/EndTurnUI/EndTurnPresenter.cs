using System;

namespace Gameplay.UserInterface.Modules.EndTurnUI
{
    public class EndTurnPresenter
    {
        private readonly EndTurnModel model;
        private readonly EndTurnView view;

        public event Action TurnEnded;

        public EndTurnPresenter(EndTurnModel model, EndTurnView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Initialize()
        {
            view.ButtonClicked += HandleButtonClicked;
            ActivateButton(true);
        }

        private void HandleButtonClicked()
        {
            TurnEnded?.Invoke();
        }

        public void ActivateButton(bool isActive)
        {
            model.IsButtonActive = isActive;
            view.ActivateButton(model.IsButtonActive);
        }
    }
}