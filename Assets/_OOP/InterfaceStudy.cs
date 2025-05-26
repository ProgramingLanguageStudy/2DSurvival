using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
인터페이스(interface)
클래스가 어떤 기능을 갖고 있는지 미리 약속하는 것

추상 클래스처럼 기능들을 직접 구현하지 않고
해당 인터페이스를 상속하는 클래스가 반드시 그 기능들을 구현한다는 것을 보장한다.

C#에서 모든 클래스는 단일 상속(한 클래스가 부모 클래스를 하나만 가질 수 있다.)
한 클래스가 여럿을 상속받고 싶다면 interface를 활용

(C++에서는 다중 상속이 가능하다.
C++에는 interface가 따로 없고 클래스를 변형해서 사용해야 한다.)

유니티에서 활용
1) 인터페이스 변수는 인스펙터뷰에서 사용 불가
2) GetComponent<T>()는 사용 가능
*/

// 인터페이스 정의(인터페이스 이름은 맨 앞에 I를 쓴다.)
public interface IInteractable  // 상호 작용이 가능한 인터페이스
{ 
    // 클래스에서는 접근 제어자(public, private 등)를 생략하면 private
    // 인터페이스에서는 접근 제어자를 생략하면 public
    void Interact();
}

// 클래스 1: 문
public class Door : IInteractable
{
    string _name;

    public Door(string name)
    {
        _name = name;
    }

    public void Interact()
    {
        Debug.Log($"{_name}이(가) 열립니다.");
    }
}

// 클래스 2: 상자
public class Chest : IInteractable
{
    string _name;
    int _gold;

    public Chest(string name, int gold)
    {
        _name = name;
        _gold = gold;
    }

    public void Interact()
    {
        Debug.Log($"{_name}을(를) 열어 {_gold} 골드를 획득했습니다!");
    }
}

public class InterfaceStudy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // IInteractable을 구현하는 객체들을 배열로 관리
        IInteractable[] interactables = new IInteractable[]
        {
            new Door("철문"),
            new Chest("보물상자", 100)
        };

        // 같은 방식으로 상호작용 실행
        foreach (IInteractable obj in interactables)
        {
            obj.Interact();
        }
    }
}
