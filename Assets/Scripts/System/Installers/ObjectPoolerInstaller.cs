using UnityEngine;
using Zenject;

public class ObjectPoolerInstaller : MonoInstaller
{
    [SerializeField] private ObjectPooler objectPooler;

    public override void InstallBindings()
    {
        Container.Bind<ObjectPooler>().
            FromComponentInHierarchy(objectPooler).
            AsSingle().
            NonLazy();
    }
}
