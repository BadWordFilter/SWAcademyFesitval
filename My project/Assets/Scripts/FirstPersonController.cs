using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//¼­¿ìÁø
public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform cameraTransform;
    public float jumpForce = 5f; // ? ?”„ ?˜ ì¶”ê??

    private float rotationX = 0f;
    private Rigidbody rb;
    private bool isGrounded = true; // ë°”ë‹¥?— ?ˆ?Š”ì§? ì²´í¬

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ë§ˆìš°?Š¤ ?šŒ? „
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        // ?´?™

        /*float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;
        Vector3 move = cameraTransform.right * moveX + cameraTransform.forward * moveZ;

        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);*/

        // ?´?™ [?ˆ˜?‰ë©´ì—?„œ ?´?™ ë°©í–¥ ê³„ì‚°] (?œ„, ?•„?˜ë´ë„ ?˜ ???ì§ì—¬ì§?)
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;

        
        Vector3 flatForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 flatRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;

        Vector3 move = flatRight * moveX + flatForward * moveZ;
        rb.velocity = new Vector3(move.x, rb.velocity.y, move.z);



        // ? ?”„ ?…? ¥
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            
            // ì¤‘ë ¥ ë°©í–¥?˜ ë°˜ë?? ë°©í–¥?œ¼ë¡? ? ?”„
            Vector3 jumpDirection = -Physics.gravity.normalized;
            rb.velocity = Vector3.zero; // ?´? „ ?†?„ ? œê±? (?Š¹?ˆ ì¤‘ë ¥ ë°˜ì „ ì§í›„ ?•„?š”)
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


    // ë°”ë‹¥ ê°ì?? [? ?”„?• ?•Œ]
    void CheckGrounded(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Vector3 gravityDir = Physics.gravity.normalized;
            float alignment = Vector3.Dot(contact.normal, -gravityDir);

            if (alignment > 0.5f) // ë°”ë‹¥ê°ì?? ë¯¼ê°?„
            {
                isGrounded = true;
                return;
            }
        }
    }
}
