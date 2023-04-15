using BuilderGame.Gameplay.garden;
using BuilderGame.Gameplay.Unit.Trigger;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Interaction
{
    public class GardenTileInteraction : MonoBehaviour
    {
        [SerializeField] private UnitTriggerEvent _unitTriggerEvent;
        [SerializeField] private Unit _unit;

        private Sequence _sequence;

        private void Awake()
        {
            _unitTriggerEvent.onGardenTileTriggerEnter += startAction;
        }

        private void OnDestroy()
        {
            _unitTriggerEvent.onGardenTileTriggerEnter -= startAction;
        }

        private void startAction(ITile<GardenTileState> gardenPart)
        {
            if (gardenPart.getState() == GardenTileState.Grass || gardenPart.getState() == GardenTileState.Ground)
            {
                gardenPart.updateState();
            }
            else if (gardenPart.getState() == GardenTileState.Ripe)
            {
                Transform vegetable = gardenPart.getProduct().transform;
                gardenPart.updateState();
                vegetable.DOJump(transform.position, 0.3f, 1, 0.1f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Vector3 defScale = transform.localScale;
                    
                    _sequence.Kill(true);
                    _sequence = DOTween.Sequence()
                    .Append(transform.DOScale(defScale * 1.2f, 0.3f).SetEase(Ease.OutBounce))
                    .OnComplete(() => transform.localScale = defScale);
                    
                    Destroy(vegetable.gameObject);
                });
            }
        }
    }
}