using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOPStudy
{
    public class BWColorChanger : ColorChanger
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChangeColor(Color.white);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                ChangeColor(Color.black);
            }
        }
    }
}