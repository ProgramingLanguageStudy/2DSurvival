using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWeapon : MonoBehaviour
{
    [SerializeField] float _rotSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotSpeed * Time.deltaTime);
    }
}
