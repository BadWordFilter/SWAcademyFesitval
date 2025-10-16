using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondStageButton : MonoBehaviour
{
    public float requiredSizeMin = 33f;
    public float requiredSizeMax = 58f;  // 큐브 크기 조건
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

        // 발판 위에 있던 물체가 나갈 때, 같은 크기 조건이라면 문 다시 열기
        
            if (door != null)
            {
                door.SetActive(true);
                
            }
        
    }
}
