using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class WhiteRay : MonoBehaviour
{

    public float speed = 20f;

    void Update()
    {
        // ����(z ����)���� �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // ���� �±� ��
        if (other.CompareTag("ColorSensor"))
        {
            Debug.Log("������ ���� ��Ҵ�!");
            // �߰� ����(��: ���� ó��, ��ƼŬ ��� ��)
            Destroy(gameObject);
        }
        // ���̳� ������ �� �浹 �� �ı�
        else if (other.CompareTag("Wall") || other.CompareTag("Prism") || other.CompareTag("Mirror"))
        {
            Destroy(gameObject);
        }
    }
}
