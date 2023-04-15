using System;
using System.Collections.Generic;
using BuilderGame.Gameplay.garden;
using DG.Tweening;
using UnityEngine;

namespace BuilderGame.Gameplay.garden
{
    public class GardenTile : MonoBehaviour, ITile<GardenTileState>
    {
        [SerializeField] private Vector2 _size;
        [SerializeField] private float _minTimeToRipe;
        [SerializeField] private float _maxTimeToRipe;
        [SerializeField] private List<ObjectToState> _tileStruct;
        [SerializeField] private Transform _vegetable;

        private Garden _garden;
        private GardenTileState _tileState;
        private bool _isEnableForUpdate;

        public Vector2 size { get => _size; private set => _size = value; }

        public void init(Garden garden)
        {
            _garden = garden;
            _isEnableForUpdate = true;
            _tileState = GardenTileState.Grass;
            setActiveTilePart(_tileState, true);
        }

        public GardenTileState getState() => _tileState;

        public GameObject getProduct()
        {
            Transform vegetable = Instantiate(_vegetable);
            vegetable.position = _vegetable.position;

            return vegetable.gameObject;
        }

        public void updateState()
        {
            if (_isEnableForUpdate == false)
            {
                return;
            }

            switch (_tileState)
            {
                case GardenTileState.Grass: updateToGround(); break;
                case GardenTileState.Ground: updateToPlanted(); break;
                case GardenTileState.Planted: return;
                case GardenTileState.Ripe: resetToGround(); break;
            }
        }

        public bool isActive()
        {
            return true;
        }

        private void resetToGround()
        {
            _tileState = GardenTileState.Ground;
            _vegetable.localScale = Vector3.zero;
        }

        private void updateToGround()
        {
            if (_garden.canUpdateTileState(_tileState) == false)
            {
                return;
            }

            _isEnableForUpdate = false;
            Transform grass = getViewTransform(GardenTileState.Grass);
            setActiveTilePart(GardenTileState.Ground, true);

            grass.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBounce)
                .OnComplete(() =>
                {
                    grass.gameObject.SetActive(false);
                    _tileState = GardenTileState.Ground;
                    _garden.tryUpdateGardenState();
                    _isEnableForUpdate = true;
                });
        }

        private void updateToPlanted()
        {
            if (_garden.canUpdateTileState(_tileState) == false)
            {
                return;
            }

            Transform ground = getViewTransform(GardenTileState.Ground);
            Transform planted = getViewTransform(GardenTileState.Planted);
            planted.gameObject.SetActive(true);
            _isEnableForUpdate = false;

            ground.DOMoveY(planted.position.y, 0.5f).SetEase(Ease.Linear).OnComplete(() => ground.gameObject.SetActive(false));
            planted.DOMoveY(0, 0.5f).SetEase(Ease.Linear).OnComplete(() =>
            {
                _isEnableForUpdate = true;
                _tileState = GardenTileState.Planted;
                _garden.tryUpdateGardenState();
                startRipeningProcess();
            });
        }

        private void startRipeningProcess()
        {
            float duration = UnityEngine.Random.Range(_maxTimeToRipe, _minTimeToRipe);
            _vegetable.gameObject.SetActive(true);
            _vegetable.localScale = Vector3.zero;

            _vegetable.DOScale(Vector3.one, duration).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                _tileState = GardenTileState.Ripe;
            });
        }

        private void setActiveTilePart(GardenTileState gardenTileState, bool isActive)
        {
            foreach (var item in _tileStruct)
            {
                if (item.gardenTileState == gardenTileState)
                {
                    item.transform.gameObject.SetActive(isActive);
                }
            }
        }

        private Transform getViewTransform(GardenTileState gardenTileState)
        {
            foreach (var item in _tileStruct)
            {
                if (gardenTileState == item.gardenTileState)
                {
                    return item.transform;
                }
            }

            Debug.LogError("The required tile was not found!!!");
            return null;
        }

        [Serializable]
        private struct ObjectToState
        {
            public Transform transform;
            public GardenTileState gardenTileState;
        }
    }

    public enum GardenTileState
    {
        Grass,
        Ground,
        Planted,
        Ripe
    }
}