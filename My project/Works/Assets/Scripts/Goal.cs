using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//서우진
public class Goal : MonoBehaviour
{
    public int stageNumber = 1;
    public float requiredDoorWidth = 12f;
    public Vector3 playerStartPosition;  // 플레이어를 이동시킬 시작 좌표

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
                // 플레이어 위치 이동
                collision.gameObject.transform.position = playerStartPosition;

                if (stageNumber < 4)
                {
                    PlayerPrefs.SetInt("Stage" + (stageNumber + 1) + "Unlocked", 1); // 다음 스테이지 열기
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
