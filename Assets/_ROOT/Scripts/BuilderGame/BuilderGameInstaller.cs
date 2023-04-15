using BuilderGame.Gameplay.Unit.UI;
using UnityEngine;
using Zenject;

namespace BuilderGame
{
    public class BuilderGameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] UnitUI _unitUI;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BuilderGameInstaller>().FromInstance(this);
        }

        public void Initialize()
        {
            Container.Bind<UnitUI>().FromInstance(_unitUI);
        }
    }
}