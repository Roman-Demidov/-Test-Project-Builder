using BuilderGame.Gameplay.Unit.UI;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private int _money;
        
        private UnitUI _unitUI;

        [Inject]
        private void constructor(UnitUI unitUI)
        {
            _unitUI = unitUI;
        }

        private void Start()
        {
            updateMoneyUI();
        }

        public void addMoney(int value)
        {
            if(value < 0)
            {
                _money += value;
                updateMoneyUI();
            }
            else
            {
                _unitUI.showMoneyAnimation(0.3f, () =>
                {
                    _money += value;
                    updateMoneyUI();
                });
            }
        }

        public int getMoney() => _money;

        private void updateMoneyUI() => _unitUI.setMoney(_money.ToString());
    }
}