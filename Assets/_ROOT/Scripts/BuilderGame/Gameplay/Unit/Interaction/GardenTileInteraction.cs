using System.Collections;
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
                    _sequence.Kill(true);
                    
                    Vector3 defScale = transform.localScale;
                    _sequence = DOTween.Sequence()
                    .Append(transform.DOScale(defScale * 1.2f, 0.15f).SetEase(Ease.InOutQuad))
                    .Append(transform.DOScale(defScale, 0.15f).SetEase(Ease.InOutQuad))
                    .OnComplete(() => {
                        transform.localScale = defScale;
                    });

                    _unit.addMoney(gardenPart.getCost());
                    Destroy(vegetable.gameObject);
                });
            }
        }
    }
}