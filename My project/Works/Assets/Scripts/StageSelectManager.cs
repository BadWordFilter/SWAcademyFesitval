using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//서우진
public class StageSelectManager : MonoBehaviour
{
    public GameObject stageSelectCanvas;
    public GameObject mainGameRoot;
    public Transform player;
    public Transform[] stageStartPoints; // 각 스테이지 시작 위치
    public Button[] stageButtons;

    void Start()
    {
        LoadStageUnlocks();
        stageSelectCanvas.SetActive(true);
        mainGameRoot.SetActive(false);
    }

    void OnEnable()
    {
        LoadStageUnlocks(); 
    }
    void LoadStageUnlocks()
    {
        for (int i = 0; i < stageButtons.Length; i++)
        {
            if (i == 0)
                stageButtons[i].interactable = true;
            else
                stageButtons[i].interactable = PlayerPrefs.GetInt("Stage" + i + "Cleared", 0) == 1;
        }
    }

    public void OnClickStage(int stageIndex)
    {
        // 플레이어 위치 이동
        player.position = stageStartPoints[stageIndex].position;

        // 게임 시작
        stageSelectCanvas.SetActive(false);
        mainGameRoot.SetActive(true);
    }
}
