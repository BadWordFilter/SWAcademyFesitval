using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//������
public class Goal : MonoBehaviour
{
    public int stageNumber = 1;
    public float requiredDoorWidth = 12f;
    public Vector3 playerStartPosition;  // �÷��̾ �̵���ų ���� ��ǥ

    private bool hasCleared = false;

    void OnEnable()
    {
        hasCleared = false; 
    }
    void OnCollisionEnter(Collision collision)
    {
        if (hasCleared) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            float width = GetComponent<Collider>().bounds.size.x;
            if (width >= requiredDoorWidth)
            {
                hasCleared = true;
                ResetStageObjects();
                // �÷��̾� ��ġ �̵�
                collision.gameObject.transform.position = playerStartPosition;

                if (stageNumber < 4)
                {
                    PlayerPrefs.SetInt("Stage" + (stageNumber + 1) + "Unlocked", 1); // ���� �������� ����
                }
                PlayerPrefs.SetInt("Stage" + stageNumber + "Cleared", 1);
                PlayerPrefs.Save();
            }
        }
    }
    void ResetStageObjects()
{
    IStageResettable[] resettableObjects = FindObjectsOfType<MonoBehaviour>(true)
        .OfType<IStageResettable>()
        .ToArray();

    foreach (var obj in resettableObjects)
    {
        //obj.ResetStage();
    }
}
}
