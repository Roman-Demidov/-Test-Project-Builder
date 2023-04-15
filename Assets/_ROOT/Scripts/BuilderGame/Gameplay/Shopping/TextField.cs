using TMPro;
using UnityEngine;

namespace BuilderGame.Gameplay.Shoppong
{
    public class TextField: MonoBehaviour
    {
        [SerializeField] private ShoppingArea shoppingArea;
        [SerializeField] private TextMeshProUGUI _textField;

        private void Start()
        {
            setText(shoppingArea.cost.ToString());
        }

        public void setText(string text)
        {
            _textField.text = text;
        }
    }
}