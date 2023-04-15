using BuilderGame.Gameplay.Shoppong;
using BuilderGame.Gameplay.Unit.Trigger;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace BuilderGame.Gameplay.Unit.Interaction
{
    public class ShoppingInteraction : MonoBehaviour
    {
        [SerializeField] private UnitTriggerEvent _unitTriggerEvent;
        [SerializeField] private Unit _unit;
        [SerializeField] private Transform _moneyPf;
        [SerializeField] private Transform _moneyPoint;
        [SerializeField] private float _paymentDuration;
        [SerializeField] private int _paymentStep;

        private ObjectPool<Transform> _moneyPool;

        private Sequence _sequence;
        private bool _isPaymentActive;

        private void Awake()
        {
            initMoneyPool();

            _isPaymentActive = false;

            _unitTriggerEvent.onShopTriggerEnter += InShopRadius;
            _unitTriggerEvent.onShopTriggerExit += stopPayment;
        }
        
        private void OnDestroy()
        {

            _unitTriggerEvent.onShopTriggerEnter -= InShopRadius;
            _unitTriggerEvent.onShopTriggerExit -= stopPayment;
        }

        private void initMoneyPool()
        {
            _moneyPool = new ObjectPool<Transform>(() =>
            {
                return Instantiate(_moneyPf);
            }, item =>
            {
                item.gameObject.SetActive(true);
            }, item =>
            {
                item.gameObject.SetActive(false);
            }, item =>
            {
                Destroy(item.gameObject);
            }, false, 5, 10);
        }

        private void InShopRadius(ShoppingArea shoppingArea)
        {
            _isPaymentActive = true;
            startPayment(shoppingArea);
        }

        private void startPayment(ShoppingArea shoppingArea)
        {
            if (_isPaymentActive == false || _unit.getMoney() <= 0)
            {
                return;
            }

            if (shoppingArea.cost <= 0)
            {
                shoppingArea.Buy();
                return;
            }

            int money = getMoneyCount(shoppingArea);

            _unit.addMoney(-money);

            Transform moneyItem = _moneyPool.Get();
            moneyItem.position = _moneyPoint.position;

            _sequence = DOTween.Sequence()
                .Append(moneyItem.DOJump(shoppingArea.getCenter(), 0, 1, _paymentDuration).SetEase(Ease.Linear))
                .OnComplete(() =>
                {
                    shoppingArea.enterMoney(money, _paymentDuration);
                    _moneyPool.Release(moneyItem);
                    startPayment(shoppingArea);
                });

            _sequence.Play();
        }

        private int getMoneyCount(ShoppingArea shoppingArea)
        {
            int money = _paymentStep;

            if (money > _unit.getMoney())
            {
                money = _unit.getMoney();
            }

            if (money > shoppingArea.cost)
            {
                money = shoppingArea.cost;
            }

            return money;
        }

        private void stopPayment(ShoppingArea shoppingArea)
        {
            _isPaymentActive = false;
            _sequence.Kill(true);
        }
    }
}