using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현
public class WhiteLightEmitter : MonoBehaviour
{
    //"화이트 큐브 Ray 프리팹
    public GameObject whiteRayPrefab;

    // 다른 스크립트(InteractWithEmitter)에서 호출할 수 있도록 public으로 두기
    public void FireWhiteRay()
    {
        if (whiteRayPrefab == null)
        {
            return;
        }

        // 발사기 앞쪽 지점에서 생성
        Vector3 spawnPos = transform.position + transform.forward * 5f;
        Quaternion rot = Quaternion.LookRotation(transform.forward);

        GameObject white = Instantiate(whiteRayPrefab, spawnPos, rot);
        //Debug.Log($"Instantiate WhiteRay: {white.name} at {spawnPos}");
    }
}