using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondStageButton : MonoBehaviour
{
    public float requiredSizeMin = 33f;
    public float requiredSizeMax = 58f;  // ť�� ũ�� ����
    public GameObject door;

    private void OnTriggerEnter(Collider other)
    {


        Vector3 scale = other.transform.localScale;
        float maxSize = Mathf.Max(scale.x, scale.y, scale.z);

        if (maxSize >= requiredSizeMin && maxSize <= requiredSizeMax)
        {
            if (door != null)
            {
                door.SetActive(false);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Vector3 scale = other.transform.localScale;
        float maxSize = Mathf.Max(scale.x, scale.y, scale.z);

        // ���� ���� �ִ� ��ü�� ���� ��, ���� ũ�� �����̶�� �� �ٽ� ����
        
            if (door != null)
            {
                door.SetActive(true);
                
            }
        
    }
}
