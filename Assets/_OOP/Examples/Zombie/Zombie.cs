using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OOPStudy
{
    // 이벤트(UnityAction) 사용법
    // += 구독
    // -= 구독 해제
    // = null; 모든 구독 해제
    // Invoke() 알리기

    // 발행자(Publisher)
    // Zombie 클래스는 체력이 변경되었을 때
    // 이를 외부에 알리는 이벤트를 갖고 있는 클래스
    public class Zombie : MonoBehaviour
    {
        [SerializeField] float _maxHp;
        [SerializeField] float _currentHp;

        // UnityEvent: 인스펙터뷰에서 연결 가능한 이벤트
        // 이벤트 매개변수로 현재 체력, 최대 체력을 전달
        // Hp 변경 이벤트
        [SerializeField] UnityEvent<float, float> OnHpChangedEvent;

        // UnityAction: 코드로만 연결 가능한 이벤트
        // 'event' 키워드를 사용해
        // 외부에서 구독, 구독 해제는 가능하지만
        // 그 외 사용은 불가능하게 제한
        // event: 외부에서 +=, -= 두 가지만 가능하게 해 주는 키워드
        public event UnityAction<float, float> OnHpChangedAction;

        private void Update()
        {
            // A 키를 누르면 체력 감소
            if (Input.GetKeyDown(KeyCode.A))
            {
                TakeDamage(5.0f);
            }

            // S 키를 누르면 체력 회복
            if (Input.GetKeyDown(KeyCode.S))
            {
                Heal(5.0f);
            }
        }

        void TakeDamage(float amount)
        {
            // 현재 체력 변경 후
            // 0 ~ _maxHp 범위로 제한
            _currentHp = Mathf.Clamp(_currentHp - amount, 0, _maxHp);

            // 이벤트 발행
            // 1. 인스펙터뷰에서 연결할 수 있는 UnityEvent 방식
            OnHpChangedEvent?.Invoke(_currentHp, _maxHp);
        }

        void Heal(float amount)
        {
            // 현재 체력 변경 후
            // 0 ~ _maxHp 범위로 제한
            _currentHp = Mathf.Clamp(_currentHp + amount, 0, _maxHp);

            // 이벤트 발행
            // 2. 코드로 연결할 수 있는 UnityAction 방식
            OnHpChangedAction?.Invoke(_currentHp, _maxHp);
        }
    }
}