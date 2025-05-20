using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
다형성(Polymorphism)
상속 - 코드를 물려주는 방식
캡슐화 - 코드를 보호하는 방식

다형성 - 같은 방식으로 다양한 동작을 실행하는 것

같은 이름의 함수여도 어떤 객체냐에 따라 다르게 동작하도록 하는 것.

virtual
override

 */

public class PolymorphismStudy : MonoBehaviour
{
    // 부모 클래스
    public class Enemy
    {
        public string Name;

        public Enemy(string name)
        {
            Name = name;
        }

        // virtual
        // 자식 클래스에서 이 함수를 재정의해서 사용할 수 있다는 선언
        public virtual void Move()
        {
            Debug.Log($"{Name}이(가) 이동하려고 합니다. (기본 동작)");
        }
    }

    // 자식 클래스 1
    public class Slime : Enemy
    {
        // base는 부모 클래스를 의미
        // 부모 클래스의 생성자가 먼저 실행이 되고
        // 자식 클래스의 생성자 코드가 이어서 실행이 된다.
        public Slime(string name) : base(name)
        {
        }
        // override
        // 자식 클래스에서 해당 virtual 함수를 재정의하겠다는 선언
        public override void Move()
        {
            // 부모 클래스에서 정의된 Move() 함수를 그대로 실행
            base.Move();

            Debug.Log($"{Name}이(가) 통통 튀며 이동합니다.");
        }
    }

    // 자식 클래스 2
    public class Orc : Enemy
    {
        public Orc(string name) : base(name) { }

        public override void Move()
        {
            base.Move();
            Debug.Log($"{Name}이(가) 돌진합니다.");
        }
    }

    // 자식 클래스 3
    public class Bat : Enemy
    {
        public Bat(string name) : base(name) { }
    }


    private void Start()
    {
        // Enemy 타입 리스트에 Enemy를 상속받는 다양한 클래스의 객체들을 저장
        List<Enemy> enemies = new List<Enemy>();

        enemies.Add(new Slime("슬라임"));
        enemies.Add(new Orc("오크"));
        enemies.Add(new Bat("박쥐"));

        // 동일한 함수명으로 다양한 행동을 실행
        foreach(Enemy enemy in enemies)
        {
            enemy.Move();
        }
    }
    // -> virtual void Update()를 사용하고
    // 자식 클래스에서 override void Update()도 사용 가능
}
