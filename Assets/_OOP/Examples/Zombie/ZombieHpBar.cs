using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OOPStudy
{
    // 구독자(Subscriber)
    // Zombie의 체력 변경 이벤트를 구독하고
    // 체력바 UI를 갱신하는 역할
    public class ZombieHpBar : MonoBehaviour
    {
        [SerializeField] Zombie _zombie;    // 체력 정보를 가진 Zombie 객체 변수
        [SerializeField] Image _image;  // 체력바 이미지 컴포넌트


        private void Start()
        {
            // Zombie의 체력 변경 이벤트에 함수 등록(구독)
            // Zombie 객체의 체력이 변경될 때마다 UpdateHpBar() 함수 자동 실행
            _zombie.OnHpChangedAction += UpdateHpBar;
        }

        // 체력바 갱신 함수
        // 이벤트로부터 전달 받은 현재 체력, 최대 체력을 바탕으로
        // 이미지의 fillAmount 값 조정
        public void UpdateHpBar(float currentHp, float maxHp)
        {
            _image.fillAmount = currentHp / maxHp;
        }
    }
}


