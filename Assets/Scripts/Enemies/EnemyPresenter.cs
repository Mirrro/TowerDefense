using UnityEngine;
using UnityEngine.Events;
using Vector3 = UnityEngine.Vector3;

public class EnemyPresenter : IEnemyPresenter
{
    public UnityEvent Died = new ();
    private EnemyModel model;
    private EnemyView view;
    private GridManager gridManager;

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
        if (model.Health <= 0)
        {
            Debug.Log("Dead");
            view.Die();
            Died?.Invoke();
        }
        else
        {
            Debug.Log($"Damaged for {amount}");
            model.Health -= amount;
            view.ReceiveDamage();
        }
    }

    private void HandlePositionChange(Vector3 position)
    {
        model.position = position;
    }

    private void HandleDestinationReached()
    {
        var path = gridManager.GetPath(gridManager.WorldToGridPosition(model.position), model.gridTargetPosition);
        view.MoveTo(path[1]);
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
