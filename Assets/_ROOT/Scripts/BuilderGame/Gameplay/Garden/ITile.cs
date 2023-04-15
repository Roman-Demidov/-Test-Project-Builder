using UnityEngine;

namespace BuilderGame.Gameplay.garden
{
    public interface ITile<T>
    {
        public T getState();
        public GameObject getProduct();
        public void updateState();
    }
}