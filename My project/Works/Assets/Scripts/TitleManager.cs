using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//¼­¿ìÁø
public class TitleManager : MonoBehaviour
{
    public GameObject titleCanvas;
    public GameObject stageSelectCanvas;

    void start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnStartClicked()
    {
        titleCanvas.SetActive(false);
        stageSelectCanvas.SetActive(true);
    }

    public void OnExitClicked()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
