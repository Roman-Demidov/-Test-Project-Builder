using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace BuilderGame.Gameplay.Unit.UI
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMoney;
        [SerializeField] private Transform _pfMoneyUI;
        
        private Unit _unit;
        private Camera _camera;
        private ObjectPool<Transform> _moneyPool;

        [Inject]
        private void constructor(Unit unit)
        {
            _unit = unit;
        }

        private void Start()
        {
            _camera = Camera.main;
            initMoneyPool();
        }

        private void initMoneyPool()
        {
            _moneyPool = new ObjectPool<Transform>(() =>
            {
                return Instantiate(_pfMoneyUI);
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

        public void setMoney(string value)
        {
            _textMoney.text = value;
        }

        public void showMoneyAnimation(float duration, Action callback = null)
        {
            Vector3 pos = _camera.WorldToScreenPoint(_unit.transform.position);
            Transform money = _moneyPool.Get();
            money.SetParent(this.transform);
            money.position = pos;
            money.localScale = Vector3.one;

            money.DOMove(_textMoney.transform.position, duration).OnComplete(() => {
                callback?.Invoke();
                _moneyPool.Release(money);
            });

        }
    }
}