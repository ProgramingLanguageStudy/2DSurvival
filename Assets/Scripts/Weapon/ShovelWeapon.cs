using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ѿ��� ��ġ�ϰ� ������ ȸ���Ͽ� ��ġ�� �Ѿ˵�� ���� �����ϴ� ����
/// </summary>
public class ShovelWeapon : MonoBehaviour
{
    [Header("----- ���� ������ -----")]
    // ���� ����
    [SerializeField] int _level;

    // ������
    [SerializeField] float _damage;

    // �����Ÿ�(�Ѿ� ��ġ ������)
    [SerializeField] float _shootingRange;

    // �Ѿ� �ӷ�(�Ѿ��� ȸ���ϴ� �ӷ�)
    [SerializeField] float _bulletSpeed;

    // �Ѿ� ��(�Ѿ��� ���ÿ� �� �� ��ġ�Ǿ� ������)
    [SerializeField] int _bulletCount;

    [Header("----- �Ѿ� ������ -----")]
    [SerializeField] Bullet _bulletPrefab;

    // ������ �Ѿ� ����Ʈ
    List<Bullet> _bullets = new List<Bullet>();

    /// <summary>
    /// �������� �Ѿ˵��� ��ġ�ϴ� �Լ�
    /// </summary>
    void SpawnBullets()
    {
        // �Ѿ˵� ���� ���� ���� ���
        float angle = 360.0f / _bulletCount;

        // bulletCount��ŭ �Ѿ� ����
        for (int i = 0; i < _bulletCount; i++)
        {

        }
    }
}
