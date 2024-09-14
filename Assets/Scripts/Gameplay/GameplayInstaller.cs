using Gameplay.Enemies;
using Gameplay.GameplayCards;
using Gameplay.GameplayLoop;
using Gameplay.GameplayLoop.GameplayStateMachine;
using Gameplay.GameplayLoop.GameplayStateMachine.GameplayStates;
using Gameplay.GameplayTasks;
using Gameplay.Grid;
using Gameplay.Player;
using Gameplay.Player.Camera;
using Gameplay.Systems;
using Gameplay.Systems.EnemyPathMVP;
using Gameplay.Towers.MVP;
using Gameplay.Towers.StateMachine;
using Gameplay.Towers.Strategies;
using Gameplay.UserInterface;
using Gameplay.Util;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private CameraView cameraView;
        [SerializeField] private UIViewReferences uiViewReferences;
        [SerializeField] private EnemyPathView enemyPathView;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GridManager>().AsSingle();        
            Container.BindInterfacesAndSelfTo<LevelGenerator.LevelGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PresenterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraPresenter>().AsSingle().WithArguments(cameraView, new CameraModel());
            Container.BindInterfacesAndSelfTo<PlayerBank>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealth>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayCardDeck>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayCardBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle().WithArguments(uiViewReferences);
            Container.BindInterfacesAndSelfTo<EnemyPathPresenter>().AsSingle().WithArguments(enemyPathView, new EnemyPathModel());

            // Enemies
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyBuilder>().AsSingle();
            
            // Mechanics
            Container.BindInterfacesAndSelfTo<EnemyDeathRewardSystem>().AsSingle();   
            Container.BindInterfacesAndSelfTo<EnemyReachGoalSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatCondition>().AsSingle();
            Container.BindInterfacesAndSelfTo<VictoryCondition>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayCardExecutionSystem>().AsSingle();
            
            // Towers
            Container.BindInterfacesAndSelfTo<TowerBuildSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridPlacementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridInteraction>().AsSingle();
            Container.BindInterfacesAndSelfTo<MouseRayCast>().AsSingle();
            Container.BindInterfacesAndSelfTo<TowerBuilder>().AsSingle();

            // Factories
            Container.BindFactory<TowerStateMachine, TowerView, TowerModel, TowerPresenter, TowerPresenter.Factory>().AsSingle();
            Container.BindFactory<EnemyView, EnemyModel, EnemyPresenter, EnemyPresenter.Factory>().AsSingle();
                // strategy Factories
            Container.BindFactory<SingleTargetAttackingStrategy, SingleTargetAttackingStrategy.Factory>().AsSingle();
            Container.BindFactory<MultipleTargetAttackingStrategy, MultipleTargetAttackingStrategy.Factory>().AsSingle();
            Container.BindFactory<TowerCooldownStrategy, TowerCooldownStrategy.Factory>().AsSingle();
            Container.BindFactory<SimpleSortingStrategy, SimpleSortingStrategy.Factory>().AsSingle();
            Container.BindFactory<TowerDetectingStrategy, TowerDetectingStrategy.Factory>().AsSingle();
                // GameplayTask Factories
            Container.BindFactory<BuildTowerATask, BuildTowerATask.Factory>().AsSingle();
            Container.BindFactory<BuildTowerBTask, BuildTowerBTask.Factory>().AsSingle();
            Container.BindFactory<HelloWorldGameplayTask, HelloWorldGameplayTask.Factory>().AsSingle();
            
            // Gameplay Loop
            Container.BindInterfacesAndSelfTo<GameplayLoop.GameplayLoop>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayStateMachine>().AsSingle();
            Container.Bind<PlayerTurnState>().AsSingle();
            Container.Bind<EnemyTurnState>().AsSingle();
            Container.Bind<DefeatState>().AsSingle();
            Container.Bind<VictoryState>().AsSingle();
            Container.Bind<InitializationState>().AsSingle();
        }
    }
}
