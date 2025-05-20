using UnityEngine;

/*
네임스페이스(namespace, 이름공간)
- 클래스명, 변수명, 함수명 등 이름 충돌을 방지할 수 있는 기능
*/

namespace MyGame.Characters
{
    public class Player
    {
        public void Move()
        {
            Debug.Log("플레이어가 이동합니다.");
        }
    }
}