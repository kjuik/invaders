using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Camera mainCamera;
    
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
    }
}