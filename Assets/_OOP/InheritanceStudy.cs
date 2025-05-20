using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
상속(Inheritance)
기존에 있는 클래스를 기반으로
새로운 클래스를 만들 수 있도록 하는 문법

부모 클래스가 가진 변수와 함수를
자식 클래스가 그대로 물려받아 사용할 수 있다.

유용한 경우
1) 코드 재사용
공통된 속성(변수)과 동작(함수)을 한 곳에 모아 두고,
이를 필요로 하는 클래스들이 상속받아 사용하게 할 때

2) 다형성 활용
곧 배울 예정!
*/

// IngeritanceStudy는 MonoBehaviour를 상속받는 클래스이다.
public class InheritanceStudy : MonoBehaviour
{
    public class Animal
    {
        public string Name;
        public void Speak()
        {
            Debug.Log($"{Name}(이)가 소리를 냅니다.");
        }
    }

    // Dog 클래스는 Animal 클래스를 상속받는 자식 클래스다.
    // * Dog 객체는 Animal 객체이기도 하다.
    public class Dog : Animal
    {
        public void Bark()
        {
            Debug.Log($"{Name}(이)가 짖습니다: 멍멍!");
        }
    }



    private void Start()
    {
        // 유니티 MonoBehaivour 상속 클래스 객체는 new 키워드 사용 제한
        // 일반 C# 클래스는 new 키워드를 사용해야 객체 생성
        // 유니티 MonoBehaivour 상속 클래스는
        // 에디터에서 사용하거나,
        // Instantiate, AddComponent 같은 유니티 함수를 사용해야 한다.

        Dog dog = new Dog();
        dog.Name = "초코";

        dog.Speak();
        dog.Bark();

        // 자식 클래스 변수는 부모 클래스 변수로 사용될 수 있다.
        Animal animal = dog;
        animal.Speak();
        //animal.Bark();    // X. Animal 클래스에 Bark() 함수가 없기 때문
    }

}
