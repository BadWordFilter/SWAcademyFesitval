using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class ColorSensor : MonoBehaviour
{
    //���� ���� ����
    public Color expectedColor = Color.white;

    //"�� �� ��� ����
    public float tolerance = 0.05f;

    //������ �¾��� �� ������ ��Ƽ����
    public Material correctMaterial;

    //������ Ʋ���� �� ������ ��Ƽ����
    public Material incorrectMaterial;

    //���� �ʱ� �⺻ ��Ƽ����
    public Material defaultMaterial;

    //������ ���� �������� ��ȣ
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

        // ���� �� �⺻ ��Ƽ���� ����
        if (defaultMaterial != null)
            _renderer.material = defaultMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTriggered) return;

        // �浹�� ��ü�� ColorRay���� Ȯ��
        ColorRay ray = other.GetComponent<ColorRay>();
        if (ray == null) return;

        Color received = ray.rayColor;
        bool correct =
            Mathf.Abs(received.r - expectedColor.r) < tolerance &&
            Mathf.Abs(received.g - expectedColor.g) < tolerance &&
            Mathf.Abs(received.b - expectedColor.b) < tolerance;

        if (correct)
        {
            isTriggered = true; //�� ó���� �� ���� �־� �ߺ� ������!

            //Debug.Log($"{name}: ���� ��({received}) ������");

            if (correctMaterial != null)
                _renderer.material = correctMaterial;
            else
                _renderer.material.color = received;

            if (GameManager.Instance != null)
                GameManager.Instance.NotifySensorActivated(this);
        }

        else
        {
            //Debug.Log($"{name}: ���� ��({received}) ������");

            if (incorrectMaterial != null)
                _renderer.material = incorrectMaterial;

            else
                _renderer.material.color = defaultMaterial != null
                                          ? defaultMaterial.color
                                          : Color.white;
        }

        Destroy(other.gameObject); //Ray ����
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

    // Raycast�� ������ ���� �������� ���� �����ؼ�
    // ���� ����� ���� ��� ���� ���� �ִ� ��� �������� �Ǵ�
    // ��Ƽ������ �����ϰ� GameManager�� ���� ó���� �˸�

    /// ������ ���� �޾� ���� ���θ� �Ǵ��ϰ� ��Ƽ������ ������Ʈ��
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