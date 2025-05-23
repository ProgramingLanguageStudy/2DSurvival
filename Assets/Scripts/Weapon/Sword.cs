using UnityEngine;

public class Sword : MonoBehaviour
{
    public float speed = 10f;
    // �Ѿ� ������
    [SerializeField] float _damage;
    // �浹�� ���̾� ����ũ
    [SerializeField] LayerMask _targetLayerMask;
    private Vector3 moveDirection;

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
        Destroy(gameObject, 2f); // 2�� �� �ڵ� ����
    }

    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��� ���ӿ�����Ʈ�� ���̾
        // _targetLayerMask�� ���ԵǸ�
        if (_targetLayerMask.Contains(collision.gameObject.layer))
        {
            // �浹�� ��� ���ӿ�����Ʈ�� Enemy ������Ʈ�� �����´�.
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Enemy ������Ʈ�� ������ ������
            if (enemy != null)
            {
                enemy.TakeHit(_damage);
            }
        }
    }
}
