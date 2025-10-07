using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//¼­¿ìÁø
public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject mainGame;
    public GameObject stageCanvas;

    private bool isPaused = false;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ContinueGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitToStageCanvas()
    {    
        ResetStageObjects();

        pauseMenu.SetActive(false);
        mainGame.SetActive(false);
        stageCanvas.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = false;
    }

    private void ResetStageObjects()
    {
        
        var resettableObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IStageResettable>()
            .ToArray();

        foreach (var obj in resettableObjects)
        {
            obj.ResetStage();
        }
    }
}