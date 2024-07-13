using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private CameraView cameraView;
    [SerializeField] private UIViewReferences uiViewReferences;
    [SerializeField] private BuildMenuItemsContainer buildMenuItemsContainer;
    
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GridManager>().AsSingle();        
        Container.BindInterfacesAndSelfTo<LevelGenerator>().AsSingle();
        Container.BindInterfacesAndSelfTo<CameraPresenter>().AsSingle().WithArguments(cameraView, new CameraModel());
        Container.BindInterfacesAndSelfTo<PlayerBank>().AsSingle();
        Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().WithArguments(uiViewReferences);
        
        Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyDeathRewardSystem>().AsSingle();     
        
        Container.BindInterfacesAndSelfTo<TowerBuildSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<GridPlacementSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<GridInteraction>().AsSingle();
        Container.BindInterfacesAndSelfTo<MouseRayCast>().AsSingle();
        Container.BindInterfacesAndSelfTo<TowerBuilder>().AsSingle();
        Container.BindInterfacesAndSelfTo<EnemyBuilder>().AsSingle();
        
        Container.BindFactory<TowerView, TowerModel, TowerPresenter, TowerPresenter.Factory>().AsSingle();
        Container.BindFactory<EnemyView, EnemyModel, EnemyPresenter, EnemyPresenter.Factory>().AsSingle();

        Container.BindInterfacesAndSelfTo<GameplayLoop>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<GameplayStateMachine>().AsSingle();
        Container.Bind<PlayerTurnState>().AsSingle().WithArguments(buildMenuItemsContainer);
        Container.Bind<EnemyTurnState>().AsSingle();
        Container.Bind<InitializationState>().AsSingle();
    }
}
