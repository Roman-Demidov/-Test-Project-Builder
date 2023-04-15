using BuilderGame.Gameplay.garden;
using UnityEngine;

namespace BuilderGame.Gameplay.Shoppong
{
    [RequireComponent(typeof(ProgressFill), typeof(TextField))]
    public class ShoppingArea : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _gardenWidth;
        [SerializeField, Min(1)] private int _gardenLength;
        [SerializeField, Min(0)] private int _cost;
        [SerializeField] private Garden _garden;
        
        private ProgressFill _progressFill;
        private TextField _textField;

        private int _startCost;

        public int width { get => _gardenWidth; private set => _gardenWidth = value; }
        public int length { get => _gardenLength; private set => _gardenLength = value; }
        public int cost { get => _cost; private set => _cost = value; }

        private void Awake()
        {
            _startCost = _cost;
            _progressFill = GetComponent<ProgressFill>();
            _textField = GetComponent<TextField>();
        }

        public void Buy()
        {
            CreateGarden();

            Destroy(gameObject);
        }

        private void CreateGarden()
        {
            Garden garden = Instantiate(_garden, transform.position, Quaternion.identity);
            Vector3 topLeftPos = transform.position;
            topLeftPos.x -= (float)_gardenWidth / 2f;
            topLeftPos.z += (float)_gardenLength / 2f;
            
            garden.init(_gardenWidth, _gardenLength, topLeftPos);
        }

        public Vector3 getCenter() => transform.position;

        public void enterMoney(int moneyCount, float duration)
        {
            _cost -= moneyCount;

            float percentage = 1f - ((float)_cost / (float)_startCost);

            _progressFill.setFillAmount(percentage, duration);
            _textField.setText(_cost.ToString());
        }
    }
}