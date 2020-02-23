using UnityEngine;
using Zenject;

public class GameDependenciesInstaller : MonoInstaller
{
    [SerializeField] private GameStateMachine gameStateMachine;
    [SerializeField] private Camera mainCamera;
    
    [SerializeField] private EnemiesMovement enemiesMovement;
    
    public override void InstallBindings()
    {
        Container.Bind<GameStateMachine>().FromInstance(gameStateMachine).AsSingle();
        Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        
        Container.Bind<EnemiesMovement>().FromInstance(enemiesMovement).AsSingle();
        Container.Bind<Wave>().FromNewComponentOnNewGameObject().AsTransient();
        
        Container.Bind<ObjectPool>().FromNewComponentOnNewGameObject().AsSingle();
        Container.BindIFactory<Object, PooledObject>().FromFactory<PooledPrefabFactory>();
    }
}