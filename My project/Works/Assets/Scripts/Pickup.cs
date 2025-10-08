using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������

public class Pickup : MonoBehaviour
{
    public Transform holdPoint;                  // ������Ʈ�� ��� ���� ��ġ
    public KeyCode pickupKey = KeyCode.Mouse1;   // ����/���� Ű
    public float rotationSpeed = 90f;            // Q/E ȸ�� �ӵ�
    public float maxPickupDistance = 100f;       // �ڵ� ���� �Ÿ�

    public string pickupTag = "Mirror";          // �±׷� ���� �� �ִ� ������Ʈ ����
    

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

        // ó������ ���� ���� �� �� (�������� ����)
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
            // �Ÿ� �ʰ��� �ڵ� ����
            if (Vector3.Distance(transform.position, holdPoint.position) > maxPickupDistance + 5f)
            {
                Drop();
                return;
            }

            // Q/E Ű�� Y�� ȸ��
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
        rb.isKinematic = false; // �̶��� �����̵��� Ȱ��ȭ
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        transform.rotation = fixedRotation;
        transform.SetParent(null);
    }

    private void Drop()
    {
        isPickedUp = false;

        col.enabled = true;              // Collider �Ѱ�
        rb.isKinematic = false;          // ���� ��� �����ϰ� �����
        rb.useGravity = true;            // �߷� �ٽ� ���ֱ�

        transform.SetParent(null);       // �θ� ���� ����
        fixedRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPickedUp)
        {
            // �ٴڰ� �浹�ϸ� ����
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}