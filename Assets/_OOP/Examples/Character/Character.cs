using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOPStudy
{
    public class Character : MonoBehaviour
    {
        [SerializeField] protected float _speed;

        void Update()
        {
            HandleMove();
        }
        protected virtual void HandleMove()
        {
            MoveHorizontal();
        }
        void MoveHorizontal()
        {
            float x = Input.GetAxis("Horizontal");
            transform.Translate(x * _speed * Time.deltaTime, 0, 0);
        }
    }
}


