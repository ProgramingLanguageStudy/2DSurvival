using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
캡슐화(Encapsulation)
객체의 내부 상태(속성, 데이터, 변수)를 외부로부터 보호하고,
필요한 기능만 제한적으로 외부에 공개하는 설계 원칙

ex. 캡슐화를 하지 않는 경우
캐릭터 클래스
public int Hp = 100;

character.Hp -= damage;
if(character.Hp < 0){
사망과 관련된 처리
}
Hp바 UI 처리.

캐릭터의 Hp가 변경될 때마다
외부에서 사망 처리, UI 처리 등등
필요한 모든 작업을 개발자가 기억했다가 처리해 줘야 한다.

접근 제한자(접근 한정자)
private: 해당 클래스 내부에서만 접근 가능
public: 해당 클래스 외부에서도 접근 가능
protected: 해당 클래스와 상속받는 자식 클래스에서만 접근 가능
*/

public class EncapsulationStudy : MonoBehaviour
{
    // 부모 클래스
    public class Parent
    {
        private string _privateMessage = "비공개 메시지 (private)";
        protected string _protectedMessage = "보호된 메시지 (protected)";
        public string PublicMessage = "공개된 메시지 (public)";

        private void PrivateMethod()
        {
            Debug.Log("Parent의 비공개 함수 실행!");
        }
        protected void ProtectedMethod()
        {
            Debug.Log("Parent의 보호된 함수 실행!");
        }
        public void PublicMethod()
        {
            Debug.Log("Parent의 공개된 함수 실행!");
        }

        public void PrintPriavateMessage()
        {
            Debug.Log(_privateMessage);
        }
    }

    // 자식 클래스
    public class Child : Parent
    {
        public void Test()
        {
            //Debug.Log(_privateMessage);   // 접근 불가
            //PrivateMethod();              // 접근 불가

            Debug.Log(_protectedMessage);   // 자식에서 접근 가능
            ProtectedMethod();              // 자식에서 접근 가능

            Debug.Log(PublicMessage);       // 외부에서도 접근 가능
            PublicMethod();                 // 외부에서도 접근 가능


            PrintPriavateMessage();
        }
    }


    private void Start()
    {
        Child child = new Child();
        child.Test();

        //child.ProtectedMethod();    // 외부에서 접근 불가
        Debug.Log(child.PublicMessage);
        child.PublicMethod();

        child.PrintPriavateMessage();
    }
}
