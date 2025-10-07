using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//정다현

public class Pickup : MonoBehaviour
{
    public Transform holdPoint;                  // 오브젝트를 들고 있을 위치
    public KeyCode pickupKey = KeyCode.Mouse1;   // 집기/놓기 키
    public float rotationSpeed = 90f;            // Q/E 회전 속도
    public float maxPickupDistance = 100f;       // 자동 놓기 거리

    public string pickupTag = "Mirror";          // 태그로 집을 수 있는 오브젝트 제한
    

    private Rigidbody rb;
    private Collider col;
    private bool isPickedUp = false;
    private Quaternion fixedRotation;
    private Camera cam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        cam = Camera.main;

        // 처음에는 물리 반응 안 함 (움직이지 않음)
        rb.useGravity = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        fixedRotation = transform.rotation;
    }

    private void Update()
    {
        if (holdPoint == null || cam == null) return;

        if (isPickedUp)
        {
            // 거리 초과시 자동 놓기
            if (Vector3.Distance(transform.position, holdPoint.position) > maxPickupDistance + 5f)
            {
                Drop();
                return;
            }

            // Q/E 키로 Y축 회전
            float rotateInput = 0f;
            if (Input.GetKey(KeyCode.Q)) rotateInput = -1f;
            if (Input.GetKey(KeyCode.E)) rotateInput = +1f;

            if (rotateInput != 0f)
            {
                transform.Rotate(Vector3.up, rotateInput * rotationSpeed * Time.deltaTime, Space.World);
            }

            rb.MovePosition(holdPoint.position);

            if (Input.GetKeyUp(pickupKey))
            {
                Drop();
            }
        }
        else
        {
            if (Input.GetKeyDown(pickupKey))
            {
                TryPickup();
            }
        }
    }

    private void TryPickup()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxPickupDistance))
        {
            if (hit.collider == col && gameObject.CompareTag(pickupTag))
            {
                PickupObject();
            }
        }
    }

    private void PickupObject()
    {
        isPickedUp = true;
        col.enabled = false;

        rb.useGravity = false;
        rb.isKinematic = false; // 이때만 움직이도록 활성화
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        transform.rotation = fixedRotation;
        transform.SetParent(null);
    }

    private void Drop()
    {
        isPickedUp = false;

        col.enabled = true;              // Collider 켜고
        rb.isKinematic = false;          // 물리 계산 가능하게 만들고
        rb.useGravity = true;            // 중력 다시 켜주기

        transform.SetParent(null);       // 부모 연결 해제
        fixedRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPickedUp)
        {
            // 바닥과 충돌하면 고정
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}