using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1) OOPStudy 네임스페이스 안에
// 2) R 키 - 빨간색, G 키 - 초록색, B 키 - 파란색으로 변경해 주는
// 3) ColorChanger의 자식 클래스인 RGBColorChanger

namespace OOPStudy
{
    public class RGBColorChanger : ColorChanger
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ChangeColor(Color.red);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                ChangeColor(Color.green);
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                ChangeColor(Color.blue);
            }
        }
    }
}


