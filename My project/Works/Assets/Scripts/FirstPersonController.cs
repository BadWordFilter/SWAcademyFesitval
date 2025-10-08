using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public float jumpForce = 5f; // ?��?�� ?�� 추�??

    private float rotationX = 0f;
    private Rigidbody rb;
    private bool isGrounded = true; // 바닥?�� ?��?���? 체크

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우?�� ?��?��
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // ?��?��

        /*float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 move = cameraTransform.right * moveX + cameraTransform.forward * moveZ;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);*/

        // ?��?�� [?��?��면에?�� ?��?�� 방향 계산] (?��, ?��?��봐도 ?�� ???직여�?)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        
        Vector3 flatForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 flatRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        Vector3 move = flatRight * moveX + flatForward * moveZ;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);



        // ?��?�� ?��?��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            // 중력 방향?�� 반�?? 방향?���? ?��?��
            Vector3 jumpDirection = -Physics.gravity.normalized;
            rb.velocity = Vector3.zero; // ?��?�� ?��?�� ?���? (?��?�� 중력 반전 직후 ?��?��)
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    public void ForceUnground()
    {
        isGrounded = false;
    }

   

    void OnCollisionEnter(Collision collision)
    {
        CheckGrounded(collision);
    }

    void OnCollisionStay(Collision collision)
    {
        CheckGrounded(collision);
    }


    // 바닥 감�?? [?��?��?��?��]
    void CheckGrounded(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 gravityDir = Physics.gravity.normalized;
            float alignment = Vector3.Dot(contact.normal, -gravityDir);

            if (alignment > 0.5f) // 바닥감�?? 민감?��
            {
                isGrounded = true;
                return;
            }
        }
    }
}
