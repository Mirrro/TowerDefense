using System;
using Gameplay.GameplayLoop.GameplayStateMachine;
using Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates;
using UnityEngine;

namespace Gameplay.GameplayLoop
{
    public class DefeatState : IGameplayState
    {
        public event Action StateComplete;
        public GameplayStateStatus GameplayStateStatus { get; set; }
        public void Activate()
        {
            Debug.Log("Unlucky... You lost :(");
        }

        public void OnPause()
        {
        }

        public void OnUnpause()
        {
        }

        public void Update()
        {
        }

        public void Deactivate()
        {
        }
    }
}