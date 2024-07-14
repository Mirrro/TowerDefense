using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Vector3 = UnityEngine.Vector3;

public class EnemyPresenter : IEnemyPresenter
{
    [Inject] private GridManager gridManager;
    
    public UnityEvent Died = new ();
    public UnityEvent ReachedGoal = new ();
    
    private EnemyModel model;
    private EnemyView view;

    public Vector3 GetPosition()
    {
        return model.position;
    }

    public Transform GetTransform()
    {
        return view.transform;
    }

    public EnemyPresenter(EnemyView view, EnemyModel model, GridManager gridManager)
    {
        this.view = view;
        this.model = model;
        this.gridManager = gridManager;
    }
    
    public void Initialize()
    {
        view.PositionChanged.AddListener(HandlePositionChange);
        view.DestinationReached.AddListener(HandleDestinationReached);
    }

    public void Move(Vector2Int target)
    {
        model.gridTargetPosition = target;
        var path = gridManager.GetPath(gridManager.WorldToGridPosition(model.position), model.gridTargetPosition);
        view.MoveTo(path[1]);
    }
    
    public void SetPosition(Vector3 position)
    {
        model.position = position;
        view.UpdatePosition(position);
    }

    public void ReceiveDamage(int amount)
    {
        model.Health -= amount;
        view.ReceiveDamage();
        
        if (model.Health <= 0)
        {
            view.Die();
            Died?.Invoke();
            view.DestinationReached.RemoveListener(HandleDestinationReached);
        }
    }

    public void StealGold()
    {
        view.Die();
    }

    private void HandlePositionChange(Vector3 position)
    {
        model.position = position;
    }

    private void HandleDestinationReached()
    {
        if (gridManager.WorldToGridPosition(model.position) == model.gridTargetPosition)
        {
            ReachedGoal?.Invoke();
        }
        else
        {
            var path = gridManager.GetPath(gridManager.WorldToGridPosition(model.position), model.gridTargetPosition);
            view.MoveTo(path[1]);
        }
    }
    
    public class Factory : PlaceholderFactory<EnemyView, EnemyModel, EnemyPresenter>
    {
        
    }
}

public interface IEnemyPresenter
{
    public Vector3 GetPosition();
    public Transform GetTransform();
    public void Initialize();
    public void Move(Vector2Int target);
    public void ReceiveDamage(int amount);
}
