using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class SaveResetter : MonoBehaviour
{
    // �ν����� ��Ŭ�� �޴������� ���� ����
    [ContextMenu("���� �ʱ�ȭ")]
    public void ResetAllProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("���� ������ �ʱ�ȭ �Ϸ�!");
    }

    // ��ư���� ���� ������ �� �ְ� public �޼��� �߰�
    public void OnResetButtonClick()
    {
        ResetAllProgress();
    }
}