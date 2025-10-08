using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������

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

            // �±׸� ����� emitter������ ���� �� �ֵ��� �ϱ�
            if (!hitObj.CompareTag("Emitter"))
            {
                //Debug.Log("Emitter�� �ƴ�: " + hitObj.name);
                return;
            }

            WhiteLightEmitter emitter = hitObj.GetComponent<WhiteLightEmitter>();
            if (emitter != null)
            {
                //���� ���� ����
                ColorSensor[] sensors = FindObjectsOfType<ColorSensor>();
                foreach (ColorSensor sensor in sensors)
                {
                    sensor.ResetSensor();
                }

                // �׸��� �� �߻�
                emitter.FireWhiteRay();
            }
        }
    }
}