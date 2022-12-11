using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerBehaviour player;

    public override void InstallBindings()
    {
        Container.Bind<PlayerBehaviour>().
            FromComponentInHierarchy(player).
            AsSingle().
            NonLazy();
    }
}