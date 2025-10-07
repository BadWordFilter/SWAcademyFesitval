using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//¼­¿ìÁø

public class ResettableBox : MonoBehaviour, IStageResettable
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 initialScale;
    private bool initialActiveSelf; 
    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        initialScale = transform.localScale;
        initialActiveSelf = gameObject.activeSelf; 
    }

    public void ResetStage()
    {
        
        gameObject.SetActive(initialActiveSelf);

        
        if (!gameObject.activeInHierarchy) return;

        transform.position = initialPosition;
        transform.rotation = initialRotation;
        transform.localScale = initialScale;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
