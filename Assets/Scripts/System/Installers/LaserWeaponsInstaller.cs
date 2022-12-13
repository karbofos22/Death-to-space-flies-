using UnityEngine;
using Zenject;

public class LaserWeaponsInstaller : MonoInstaller
{
    [SerializeField] private LaserWeapons laser;

    public override void InstallBindings()
    {
        Container.Bind<LaserWeapons>().
            FromComponentInHierarchy(laser).
            AsSingle().
            NonLazy();
    }
}