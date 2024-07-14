using System;
using UnityEngine;

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