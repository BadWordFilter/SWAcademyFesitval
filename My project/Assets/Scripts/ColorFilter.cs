using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
[RequireComponent(typeof(Collider))]
public class ColorFilter : MonoBehaviour
{
    //�� ���Ͱ� ������ ����
    public Color filterColor = Color.red;

    //��� �� �ӵ� ����
    public float speedMultiplier = 1f;

    void OnTriggerEnter(Collider other)
    {
        // 1) �� ������Ʈ�� ColorRay(ť��)���� Ȯ��
        ColorRay cr = other.GetComponent<ColorRay>();
        if (cr == null) return;

        // 2) ���� Ray ���� ���� ���� ���ϰ�, 0~1 ������ Ŭ����
        Color oldColor = cr.rayColor;
        Color newColor = oldColor + filterColor;
        newColor.r = Mathf.Clamp01(newColor.r);
        newColor.g = Mathf.Clamp01(newColor.g);
        newColor.b = Mathf.Clamp01(newColor.b);
        cr.rayColor = newColor;

        // 3) MeshRenderer�� �����ͼ� �ǽð����� ť�� �� ����
        MeshRenderer mr = cr.GetComponent<MeshRenderer>();
        if (mr != null)
            mr.material.color = newColor;

        // 4) �ӵ� ����
        cr.speed *= speedMultiplier;

        
        //Debug.Log($"[{gameObject.name}] Filter ���: ���� �� {oldColor} �� ���ο� �� {newColor}");
        
    }
}