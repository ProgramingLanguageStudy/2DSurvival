using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoluspaSkill : MonoBehaviour
{
    public GameObject swordPrefab;      // �� ������
    public int artifactLevel = 3;       // �ߵ� �� ������ �� ����
    public float spawnX = 10f;          // ȭ�� �����ڸ� ��ġ

    public void Activate()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0) return;

        List<float> uniqueY = new List<float>();

        foreach (var enemy in enemies)
        {
            // ���� ���� ��ǥ �� ����Ʈ ��ǥ�� ��ȯ
            Vector3 viewPos = Camera.main.WorldToViewportPoint(enemy.transform.position);

            // ȭ�� �ȿ� �ִ� ���� ������� ��
            if (viewPos.x >= 0f && viewPos.x <= 1f && viewPos.y >= 0f && viewPos.y <= 1f)
            {
                float y = Mathf.Round(enemy.transform.position.y * 10f) / 10f;
                if (!uniqueY.Contains(y))
                    uniqueY.Add(y);
            }
        }

        if (uniqueY.Count == 0) return;

        List<float> selectedY = uniqueY.OrderBy(x => Random.value).Take(artifactLevel).ToList();

        foreach (float y in selectedY)
        {
            Vector3 spawnPos;
            Vector3 moveDir;

            if (Random.value < 0.5f)
            {
                spawnPos = new Vector3(-spawnX, y, 0); // ����
                moveDir = Vector3.right;
            }
            else
            {
                spawnPos = new Vector3(spawnX, y, 0); // ������
                moveDir = Vector3.left;
            }

            GameObject sword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);
            sword.GetComponent<Sword>().Initialize(moveDir);
        }
    }

}
