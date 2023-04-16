using BuilderGame.Gameplay.Unit;
using BuilderGame.Gameplay.Unit.UI;
using UnityEngine;
using Zenject;

namespace BuilderGame
{
    public class BuilderGameInstaller : MonoInstaller, IInitializable
    {
        [SerializeField] private UnitUI _unitUI;
        [SerializeField] private Unit _unit;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BuilderGameInstaller>().FromInstance(this);

            Container.Bind<UnitUI>().FromInstance(_unitUI);
            Container.Bind<Unit>().FromInstance(_unit);
        }

        public void Initialize()
        {

        }
    }
}