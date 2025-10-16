using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현

public class ColorRay : MonoBehaviour
{
    //이동 속도
    public float speed = 20f;

    //현재 큐브 Ray 색상
    public Color rayColor = Color.white;

    private MeshRenderer mr;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
        if (mr != null)
            mr.material.color = rayColor;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // 1) Filter 충돌
        if (other.CompareTag("Filter"))
        {
            ColorFilter filter = other.GetComponent<ColorFilter>();
            if (filter != null)
            {
                Color oldColor = rayColor;
                Color newColor = oldColor + filter.filterColor;
                newColor.r = Mathf.Clamp01(newColor.r);
                newColor.g = Mathf.Clamp01(newColor.g);
                newColor.b = Mathf.Clamp01(newColor.b);
                rayColor = newColor;
                if (mr != null)
                    mr.material.color = rayColor;
                speed *= filter.speedMultiplier;
            }
            return;
        }

        // 2) Sensor 충돌
        if (other.CompareTag("ColorSensor"))
        {
            Destroy(gameObject);
            return;
        }

        // 3) Wall 충돌
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
            return;
        }

        // 4) Mirror 반사(처리)
        if (other.CompareTag("Mirror"))
        {
            Vector3 incomingDir = transform.forward.normalized;
            Vector3 mirrorNormal = other.transform.forward.normalized;
            Vector3 reflectDir = Vector3.Reflect(incomingDir, mirrorNormal);

            float offset = 0.2f;
            Vector3 spawnPos = transform.position + reflectDir * offset;
            Quaternion spawnRot = Quaternion.LookRotation(reflectDir);

            GameObject reflected = Instantiate(gameObject, spawnPos, spawnRot);
            ColorRay cr = reflected.GetComponent<ColorRay>();
            if (cr != null)
            {
                cr.rayColor = rayColor;
                if (cr.mr != null)
                    cr.mr.material.color = rayColor;
                cr.speed = speed;
            }
            Destroy(gameObject);
            return;
        }

        // 5) Prism(분리) 처리는 PrismSplitter에서 OnTriggerEnter로
    }
}

