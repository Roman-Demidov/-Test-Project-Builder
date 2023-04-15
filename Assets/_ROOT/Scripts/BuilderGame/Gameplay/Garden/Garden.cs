using System;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Gameplay.garden
{
    public class Garden : MonoBehaviour
    {
        [SerializeField] private GardenTile _tilePf;

        private List<GardenTile> _gardenTiles;
        private GardenTileState _currentGardenState;
        private int _width;
        private int _length;

        public void init(int width, int length, Vector3 topLeftPos)
        {
            _width = width;
            _length = length;

            _gardenTiles = new List<GardenTile>();
            _currentGardenState = GardenTileState.Grass;

            createGardenTiles(topLeftPos);
        }

        private void createGardenTiles(Vector3 topLeftPos)
        {
            Vector3 tilePos = Vector3.zero;
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _length; j++)
                {
                    var tile = Instantiate(_tilePf, transform);
                    tile.init(this);
                    _gardenTiles.Add(tile);
                    tilePos.x = topLeftPos.x + (i * tile.size.x);
                    tilePos.y = topLeftPos.y;
                    tilePos.z = topLeftPos.z - (j * (tile.size.y));

                    tile.transform.position = tilePos;
                }
            }
        }

        public void tryUpdateGardenState()
        {
            GardenTileState tileState = _gardenTiles[0].getState();

            foreach (GardenTile gardenTile in _gardenTiles)
            {
                if (tileState != gardenTile.getState())
                {
                    return;
                }
            }

            _currentGardenState++;
        }


        public bool canUpdateTileState(GardenTileState tileState)
        {
            if (_currentGardenState == GardenTileState.Planted)
            {
                return true;
            }

            return _currentGardenState == tileState;
        }
    }
}