using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현
[RequireComponent(typeof(Collider))]
public class ColorFilter : MonoBehaviour
{
    //이 필터가 더해줄 색상
    public Color filterColor = Color.red;

    //통과 시 속도 배율
    public float speedMultiplier = 1f;

    void OnTriggerEnter(Collider other)
    {
        // 1) 이 오브젝트가 ColorRay(큐브)인지 확인
        ColorRay cr = other.GetComponent<ColorRay>();
        if (cr == null) return;

        // 2) 기존 Ray 색상에 필터 색을 더하고, 0~1 범위로 클램프
        Color oldColor = cr.rayColor;
        Color newColor = oldColor + filterColor;
        newColor.r = Mathf.Clamp01(newColor.r);
        newColor.g = Mathf.Clamp01(newColor.g);
        newColor.b = Mathf.Clamp01(newColor.b);
        cr.rayColor = newColor;

        // 3) MeshRenderer를 가져와서 실시간으로 큐브 색 변경
        MeshRenderer mr = cr.GetComponent<MeshRenderer>();
        if (mr != null)
            mr.material.color = newColor;

        // 4) 속도 조정
        cr.speed *= speedMultiplier;

        
        //Debug.Log($"[{gameObject.name}] Filter 통과: 이전 색 {oldColor} → 새로운 색 {newColor}");
        
    }
}