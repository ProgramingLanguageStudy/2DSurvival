using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
추상화(Abstaction)
불필요한 세부 사항은 숨기고 외부에 현실적인 기능(사용법)만 제공하는 것

추상 클래스
(외부에 제공하는 사용법만 있고) 실제 객체는 만들 수 없는 클래스
공통 기능들을 갖고 있고 본인만의 객체는 만들 수 없는 부모 클래스 역할 클래스

abstract
interface (나중에)
 */

public class AbstractionStudy : MonoBehaviour
{
    // 추상 클래스
    public abstract class Enemy
    {
        public string Name;
        public Enemy(string name)
        {
            Name = name;
        }

        public void Move()
        {
            Debug.Log($"{Name}이(가) 이동합니다.");
        }

        // 반드시 자식이 구현해야 하는 함수
        // 이 클래스를 상속받는 클래스들은
        // Attack() 기능이 있다는 걸 보장받는다.
        public abstract void Attack();
    }

    // 자식 클래스
    public class Slime : Enemy
    {
        public Slime(string name) : base(name)
        {
        }

        public override void Attack()
        {
            Debug.Log($"{Name}이(가) 산성 액체로 공격합니다!");
        }
    }

    // 자식 클래스2
    public class Orc : Enemy
    {
        public Orc(string name) : base(name)
        {
        }

        public override void Attack()
        {
            Debug.Log($"{Name}이(가) 큰 도끼로 내려칩니다!");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Enemy는 추상 클래스라서 객체를 직접 만들 수 없다.
        //Enemy enemy = new Enemy("기본");

        List<Enemy> enemies = new List<Enemy>()
        {
            new Slime("말랑이"),
            new Orc("오크대장")
        };

        foreach(Enemy enemy in enemies)
        {
            enemy.Move();       // 공통 기능, 공통 동작
            enemy.Attack();     // 공통 기능, 다른 동작
        }
    }
}
