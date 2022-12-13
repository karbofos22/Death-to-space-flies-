using UnityEngine;
using Zenject;

public class BeamRayInstaller : MonoInstaller
{
    [SerializeField] private BeamRayWeapon beamRay;

    public override void InstallBindings()
    {
        Container.Bind<BeamRayWeapon>().
            FromComponentInHierarchy(beamRay).
            AsSingle().
            NonLazy();
    }
}