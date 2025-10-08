using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//서우진
public class EndingCameraMover : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float moveDuration = 5f;
    public float delayBeforeMove = 3f;   // 시작 전 대기 시간
    public float delayAfterMove = 2f;    // 끝나고 대기 시간

    public GameObject playerCamera;
    public GameObject mainCamera;
    public GameObject stageCanvas;
    public GameObject mainGame;

    private float timer = 0f;
    private enum State { Idle, WaitingToMove, Moving, WaitingAfterMove, Done }
    private State state = State.Idle;

    public void StartEnding()
    {
        transform.position = startPoint.position;
        transform.rotation = startPoint.rotation;

        timer = 0f;
        state = State.WaitingToMove;
    }

    void Update()
    {
        switch (state)
        {
            case State.WaitingToMove:
                timer += Time.deltaTime;
                if (timer >= delayBeforeMove)
                {
                    timer = 0f;
                    state = State.Moving;
                }
                break;

            case State.Moving:
                timer += Time.deltaTime;
                float t = Mathf.Clamp01(timer / moveDuration);
                transform.position = Vector3.Lerp(startPoint.position, endPoint.position, t);
                transform.rotation = Quaternion.Lerp(startPoint.rotation, endPoint.rotation, t);

                if (t >= 1f)
                {
                    timer = 0f;
                    state = State.WaitingAfterMove;
                }
                break;

            case State.WaitingAfterMove:
                timer += Time.deltaTime;
                if (timer >= delayAfterMove)
                {
                    state = State.Done;
                    EndSequence();
                }
                break;
        }
    }

    void EndSequence()
    {
        if (playerCamera != null)
            playerCamera.SetActive(true);

        if (mainCamera != null)


            if (stageCanvas != null)
                stageCanvas.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        mainGame.SetActive(false);

        
    }
}