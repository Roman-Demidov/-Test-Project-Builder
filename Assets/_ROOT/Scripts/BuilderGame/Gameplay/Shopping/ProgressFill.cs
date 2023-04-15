using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BuilderGame.Gameplay.Shoppong
{
    public class ProgressFill : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void setFillAmount(float value, float duration)
        {
            value = Mathf.Clamp(value, 0.0f, 1.0f);

            _image.DOKill(true);
            _image.DOFillAmount(value, duration);
        }
    }
}