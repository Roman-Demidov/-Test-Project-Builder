using TMPro;
using UnityEngine;
using Zenject;

namespace BuilderGame.Gameplay.Unit.UI
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMoney;

        public void setMoney(string value)
        {
            _textMoney.text = value;
        }
    }
}