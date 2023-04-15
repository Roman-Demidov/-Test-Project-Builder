using System;
using BuilderGame.Gameplay.garden;
using BuilderGame.Gameplay.Shoppong;
using UnityEngine;

namespace BuilderGame.Gameplay.Unit.Trigger
{
    public class UnitTriggerEvent : MonoBehaviour
    {
        public event Action<ShoppingArea> onShopTriggerEnter;
        public event Action<ShoppingArea> onShopTriggerExit;
        public event Action<ITile<GardenTileState>> onGardenTileTriggerEnter;
        public event Action<ITile<GardenTileState>> onGardenTileTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ShoppingArea shop))
            {
                onShopTriggerEnter?.Invoke(shop);
            }
            else if (other.TryGetComponent(out ITile<GardenTileState> gardenTile))
            {
                onGardenTileTriggerEnter?.Invoke(gardenTile);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ShoppingArea shop))
            {
                onShopTriggerExit?.Invoke(shop);
            }
            else if (other.TryGetComponent(out ITile<GardenTileState> gardenPart))
            {
                onGardenTileTriggerExit?.Invoke(gardenPart);
            }
        }
    }
}