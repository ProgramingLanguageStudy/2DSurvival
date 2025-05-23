using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speed = 10f;
    // 총알 데미지
    [SerializeField] float _damage;
    // 충돌할 레이어 마스크
    [SerializeField] LayerMask _targetLayerMask;
    private Vector3 moveDirection;

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
        Destroy(gameObject, 2f); // 2초 후 자동 삭제
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 상대 게임오브젝트의 레이어가
        // _targetLayerMask에 포함되면
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            // 충돌한 상대 게임오브젝트의 Enemy 컴포넌트를 가져온다.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Enemy 컴포넌트가 실제로 있으면
            if (enemy != null)
            {
                enemy.TakeHit(_damage);
            }
        }
    }
}
