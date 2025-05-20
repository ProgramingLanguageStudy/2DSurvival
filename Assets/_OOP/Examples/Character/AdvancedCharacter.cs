using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 0) AdvancedCharacter 클래스는 OOPStudy 네임스페이스 안에 있게 해 주세요.
// 1) AdvancedCharacter 클래스가 Character 클래스를 상속받게 해 주세요.
// 2) AdvancedCharacter 클래스 객체는 수직으로도(위아래, y방향) 이동할 수 있게 해 주세요.
// 3) 반드시 override 키워드를 사용해 주세요.

// 작업이 빨리 되신 분들은 주인공 캐릭터 애니메이션 포함해서 좌우, 위아래 이동 기능

namespace OOPStudy
{
    public class AdvancedCharacter : Character
    {
        protected override void HandleMove()
        {
            base.HandleMove();
            MoveVertical();
        }
        void MoveVertical()
        {
            float y = Input.GetAxis("Vertical");
            transform.Translate(Vector3.up * y * _speed * Time.deltaTime);
        }
    }
}
