using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//¼­¿ìÁø
public class StageSelector : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject stageCanvas;
    public GameObject mainGame;
    public GameObject player;

    
    public Transform[] stageStartPoints; 

    public void SelectStage(int stageIndex)
    {
        stageCanvas.SetActive(false);
        mainGame.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        player.transform.position = stageStartPoints[stageIndex].position;
        player.transform.rotation = stageStartPoints[stageIndex].rotation;
        player.SetActive(true);
    }

    public void OnClickBack()
    {
        stageCanvas.SetActive(false);
        titleCanvas.SetActive(true);
    }
}
