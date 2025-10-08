using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class DoorController : MonoBehaviour
{
    //���� �ö� ����
    public float openHeight = 100f;

    //�� �̵� �ӵ� (����/��)
    public float moveSpeed = 2f;

    //���� ���� �� �ڵ����� ��������� �ɸ��� �ð�(��)
    public float autoCloseDelay = 5f;

    private Vector3 closedPosition;   // ���� �ִ� ��ġ
    private Vector3 openPosition;     // ���� ���� ��ġ
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

        // ������ ���� 5�� �Ŀ� �ڵ����� ��������
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