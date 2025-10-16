using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현
public class DoorController : MonoBehaviour
{
    //문이 올라갈 높이
    public float openHeight = 100f;

    //문 이동 속도 (유닛/초)
    public float moveSpeed = 2f;

    //문이 열린 뒤 자동으로 닫히기까지 걸리는 시간(초)
    public float autoCloseDelay = 5f;

    private Vector3 closedPosition;   // 닫혀 있던 위치
    private Vector3 openPosition;     // 열려 있을 위치
    private bool isOpen = false;
    private bool isMoving = false;

    private void Awake()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }
    public void OpenDoor()
    {
        if (isOpen || isMoving) return;
        StartCoroutine(MoveDoor(closedPosition, openPosition));
        isOpen = true;

        // 열리고 나서 5초 후에 자동으로 닫히도록
        StartCoroutine(AutoCloseAfterDelay());
    }

    public void CloseDoor()
    {
        if (!isOpen || isMoving) return;
        StartCoroutine(MoveDoor(openPosition, closedPosition));
        isOpen = false;
    }

    private IEnumerator AutoCloseAfterDelay()
    {
        yield return new WaitForSeconds(autoCloseDelay);
        CloseDoor();
    }

    private IEnumerator MoveDoor(Vector3 fromPos, Vector3 toPos)
    {
        isMoving = true;
        float elapsed = 0f;
        float distance = Vector3.Distance(fromPos, toPos);
        float duration = distance / moveSpeed;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.position = Vector3.Lerp(fromPos, toPos, t);
            yield return null;
        }

        transform.position = toPos;
        isMoving = false;
    }
}