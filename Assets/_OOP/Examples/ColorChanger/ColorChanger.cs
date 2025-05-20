using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOPStudy
{
    public class ColorChanger : MonoBehaviour
    {
        [SerializeField] SpriteRenderer _renderer;
        Color _color = Color.white;

        private void Start()
        {
            ApplyColor();
        }
        protected void ChangeColor(Color color)
        {
            _color = color;
            ApplyColor();
        }
        private void ApplyColor()
        {
            _renderer.color = _color;
        }
    }
}
