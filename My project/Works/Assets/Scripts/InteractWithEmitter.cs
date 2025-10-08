using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현

public class InteractWithEmitter : MonoBehaviour
{
    public float interactRange = 30f;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryFireEmitter();
        }
    }

    private void TryFireEmitter()
    {
        if (cam == null) return;

        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = cam.ScreenPointToRay(screenCenter);

        if (Physics.Raycast(ray, out RaycastHit hit, interactRange))
        {
            GameObject hitObj = hit.collider.gameObject;

            // 태그를 사용해 emitter에서만 사용될 수 있도록 하기
            if (!hitObj.CompareTag("Emitter"))
            {
                //Debug.Log("Emitter가 아님: " + hitObj.name);
                return;
            }

            WhiteLightEmitter emitter = hitObj.GetComponent<WhiteLightEmitter>();
            if (emitter != null)
            {
                //먼저 센서 리셋
                ColorSensor[] sensors = FindObjectsOfType<ColorSensor>();
                foreach (ColorSensor sensor in sensors)
                {
                    sensor.ResetSensor();
                }

                // 그리고 빛 발사
                emitter.FireWhiteRay();
            }
        }
    }
}