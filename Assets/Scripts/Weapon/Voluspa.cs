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

        // 적들의 Y 위치 수집 및 중복 제거
        List<float> uniqueY = new List<float>();
        foreach (var enemy in enemies)
        {
            float y = Mathf.Round(enemy.transform.position.y * 10f) / 10f;
            if (!uniqueY.Contains(y))
                uniqueY.Add(y);
        }

        // 레벨 수만큼 랜덤 Y 선택
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
