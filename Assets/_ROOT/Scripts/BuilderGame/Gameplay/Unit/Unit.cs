using BuilderGame.Gameplay.Unit.UI;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private UnitUI _unitUI;
        [SerializeField] private int _money;

        private void Start()
        {
            updateMoneyUI();
        }

        public void addMoney(int value)
        {
            _money += value;
            updateMoneyUI();
        }

        public int getMoney() => _money;

        private void updateMoneyUI() => _unitUI.setMoney(_money.ToString());
    }
}