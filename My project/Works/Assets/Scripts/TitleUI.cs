using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//¼­¿ìÁø
public class TitleUI : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject stageSelectCanvas;

    public void OnClickStart()
    {
        titleCanvas.SetActive(false);         
        stageSelectCanvas.SetActive(true);    
    }

    public void OnClickQuit()
    {
        Application.Quit(); 
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#endif
    }
}
