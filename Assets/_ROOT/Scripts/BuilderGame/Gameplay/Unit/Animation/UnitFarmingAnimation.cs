using BuilderGame.Gameplay.garden;
using BuilderGame.Gameplay.Unit.Trigger;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Animation
{
    public class UnitFarmingAnimation : MonoBehaviour
    {
        [SerializeField] private UnitTriggerEvent _triggerEvent;
        [SerializeField] private Animator _animator;

        private void Start()
        {
            _triggerEvent.onGardenTileTriggerEnter += playAnimation;
        }

        private void playAnimation(ITile<GardenTileState> tile)
        {
            switch (tile.getState())
            {
                case GardenTileState.Grass: _animator.SetTrigger(UnitConstants.ANIMATION_DIGGING); return;
                case GardenTileState.Ground: _animator.SetTrigger(UnitConstants.ANIMATION_PLANTING); return;
                case GardenTileState.Planted: return;
                case GardenTileState.Ripe: _animator.SetTrigger(UnitConstants.ANIMATION_COLLTCTING); return;
            }
        }
    }
}