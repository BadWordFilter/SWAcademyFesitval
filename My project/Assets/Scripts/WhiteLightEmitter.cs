using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class WhiteLightEmitter : MonoBehaviour
{
    //"ȭ��Ʈ ť�� Ray ������
    public GameObject whiteRayPrefab;

    // �ٸ� ��ũ��Ʈ(InteractWithEmitter)���� ȣ���� �� �ֵ��� public���� �α�
    public void FireWhiteRay()
    {
        if (whiteRayPrefab == null)
        {
            return;
        }

        // �߻�� ���� �������� ����
        Vector3 spawnPos = transform.position + transform.forward * 5f;
        Quaternion rot = Quaternion.LookRotation(transform.forward);

        GameObject white = Instantiate(whiteRayPrefab, spawnPos, rot);
        //Debug.Log($"Instantiate WhiteRay: {white.name} at {spawnPos}");
    }
}