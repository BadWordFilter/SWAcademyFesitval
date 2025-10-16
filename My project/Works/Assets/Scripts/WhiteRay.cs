using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현
public class WhiteRay : MonoBehaviour
{

    public float speed = 20f;

    void Update()
    {
        // 앞쪽(z 방향)으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 센서 태그 비교
        if (other.CompareTag("ColorSensor"))
        {
            Debug.Log("센서에 빔이 닿았다!");
            // 추가 로직(예: 점수 처리, 파티클 재생 등)
            Destroy(gameObject);
        }
        // 벽이나 프리즘 등 충돌 시 파괴
        else if (other.CompareTag("Wall") || other.CompareTag("Prism") || other.CompareTag("Mirror"))
        {
            Destroy(gameObject);
        }
    }
}
