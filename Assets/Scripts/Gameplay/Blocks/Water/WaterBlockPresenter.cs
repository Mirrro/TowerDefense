
using UnityEngine;
using Zenject;

public class WaterBlockPresenter : IGridElement, ITickable
{
    private readonly WaterBlockView view;
    private readonly WaterBlockModel model;
    public bool IsSolid => true;

    public void OnGridPosition(Vector3 position)
    {
        model.Position = position;
        view.SetPosition(model.Position);
    }
    
    public WaterBlockPresenter(WaterBlockView view, WaterBlockModel model)
    {
        this.view = view;
        this.model = model;
    }

    public void Initialize()
    {
        view.SetPosition(model.Position);
    }

    public void Tick()
    {
        if (Time.time >= model.LastTimeParticleActivated + model.ParticleCooldown)
        {
            view.FireLava();
            model.ParticleCooldown = Random.Range(3, 200);
            model.LastTimeParticleActivated = Time.time;
        }
    }
}
