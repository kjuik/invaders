using UnityEngine;
using Zenject;

public class GameDependenciesInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Camera mainCamera;
    
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        
        Container.Bind<ObjectPool>().FromNewComponentOnNewGameObject().AsSingle();
    }
}