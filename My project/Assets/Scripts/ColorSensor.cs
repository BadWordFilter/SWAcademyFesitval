using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현
public class ColorSensor : MonoBehaviour
{
    //센서 정답 색상
    public Color expectedColor = Color.white;

    //"색 비교 허용 오차
    public float tolerance = 0.05f;

    //센서가 맞았을 때 적용할 머티리얼
    public Material correctMaterial;

    //센서가 틀렸을 때 적용할 머티리얼
    public Material incorrectMaterial;

    //센서 초기 기본 머티리얼
    public Material defaultMaterial;

    //센서가 속한 스테이지 번호
    public int stageNumber;

    private Renderer _renderer;
    private bool isTriggered = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer == null)
        {
            enabled = false;
            return;
        }

        // 시작 시 기본 머티리얼 설정
        if (defaultMaterial != null)
            _renderer.material = defaultMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        // 충돌한 객체가 ColorRay인지 확인
        ColorRay ray = other.GetComponent<ColorRay>();
        if (ray == null) return;

        Color received = ray.rayColor;
        bool correct =
            Mathf.Abs(received.r - expectedColor.r) < tolerance &&
            Mathf.Abs(received.g - expectedColor.g) < tolerance &&
            Mathf.Abs(received.b - expectedColor.b) < tolerance;

        if (correct)
        {
            isTriggered = true; //맨 처음에 이 줄을 둬야 중복 방지됨!

            //Debug.Log($"{name}: 정답 색({received}) 감지됨");

            if (correctMaterial != null)
                _renderer.material = correctMaterial;
            else
                _renderer.material.color = received;

            if (GameManager.Instance != null)
                GameManager.Instance.NotifySensorActivated(this);
        }

        else
        {
            //Debug.Log($"{name}: 오답 색({received}) 감지됨");

            if (incorrectMaterial != null)
                _renderer.material = incorrectMaterial;

            else
                _renderer.material.color = defaultMaterial != null
                                          ? defaultMaterial.color
                                          : Color.white;
        }

        Destroy(other.gameObject); //Ray 제거
    }
    public void ResetSensor()
    {
        isTriggered = false;

        if (_renderer != null)
        {
            if (defaultMaterial != null)
                _renderer.material = defaultMaterial;
        }

    }

    // Raycast로 센서에 닿은 레이저의 색을 감지해서
    // 정답 색상과 일정 허용 오차 내에 있는 경우 정답으로 판단
    // 머티리얼을 변경하고 GameManager에 정답 처리를 알림

    /// 레이저 색을 받아 정답 여부를 판단하고 머티리얼을 업데이트함
    public void OnRayHit(Color incomingColor)
    {
        if (isTriggered) return;

        bool correct =
            Mathf.Abs(incomingColor.r - expectedColor.r) < tolerance &&
            Mathf.Abs(incomingColor.g - expectedColor.g) < tolerance &&
            Mathf.Abs(incomingColor.b - expectedColor.b) < tolerance;

        if (correct)
        {
            isTriggered = true;
            if (correctMaterial != null)
                _renderer.material = correctMaterial;

            if (GameManager.Instance != null)
                GameManager.Instance.NotifySensorActivated(this);
        }
        else
        {
            if (incorrectMaterial != null)
                _renderer.material = incorrectMaterial;
        }
    }
}