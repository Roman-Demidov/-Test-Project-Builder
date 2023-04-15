using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.Shoppong
{
    public class AreaDebugProjection : MonoBehaviour
    {
        [SerializeField] private ShoppingArea _shoppingArea;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector3(_shoppingArea.width, 1f, _shoppingArea.length));
        }

    }
}