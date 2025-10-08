using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class PrismSplitter : MonoBehaviour
{
    //RGB ť�� Ray ������
    public GameObject redRayPrefab;
    public GameObject greenRayPrefab;
    public GameObject blueRayPrefab;

    //�и� ����
    public float splitAngle = 15f;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("WhiteRay"))
            return;

        //Debug.Log("PrismSplitter: WhiteRay �浹 ���� �� �и� ����");
        Destroy(other.gameObject);

        Vector3 hitPos = other.transform.position;
        Vector3 baseDir = transform.forward;

        SpawnRay(greenRayPrefab, hitPos, baseDir);
        Vector3 redDir = Quaternion.Euler(0, splitAngle, 0) * baseDir;
        SpawnRay(redRayPrefab, hitPos, redDir);
        Vector3 blueDir = Quaternion.Euler(0, -splitAngle, 0) * baseDir;
        SpawnRay(blueRayPrefab, hitPos, blueDir);
    }

    void SpawnRay(GameObject prefab, Vector3 pos, Vector3 dir)
    {
        if (prefab == null)
        {
            //Debug.LogWarning("PrismSplitter: prefab�� �Ҵ���� ����");
            return;
        }
        float offset = 0.3f;
        Vector3 spawnPos = pos + dir.normalized * offset;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        Instantiate(prefab, spawnPos, rot);
        //Debug.Log($"PrismSplitter: SpawnRay -> {prefab.name} at {spawnPos}");
    }
}