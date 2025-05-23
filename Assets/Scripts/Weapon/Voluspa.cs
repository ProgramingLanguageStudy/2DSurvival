using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoluspaSkill : MonoBehaviour
{
    public GameObject swordPrefab;      // 검 프리팹
    public int artifactLevel = 3;       // 발동 시 생성할 검 개수
    public float spawnX = 10f;          // 화면 가장자리 위치

    public void Activate()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        if (enemies.Length == 0) return;

        List<float> uniqueY = new List<float>();

        foreach (var enemy in enemies)
        {
            // 적의 월드 좌표 → 뷰포트 좌표로 변환
            Vector3 viewPos = Camera.main.WorldToViewportPoint(enemy.transform.position);

            // 화면 안에 있는 적만 대상으로 함
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
                spawnPos = new Vector3(-spawnX, y, 0); // 왼쪽
                moveDir = Vector3.right;
            }
            else
            {
                spawnPos = new Vector3(spawnX, y, 0); // 오른쪽
                moveDir = Vector3.left;
            }

            GameObject sword = Instantiate(swordPrefab, spawnPos, Quaternion.identity);
            sword.GetComponent<Sword>().Initialize(moveDir);
        }
    }

}
