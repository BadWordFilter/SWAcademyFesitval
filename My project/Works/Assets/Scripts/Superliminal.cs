using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 //서우진
public class Superliminal : MonoBehaviour
{
    [Header("Components")]
    public Transform target;            
 
    [Header("Parameters")]
    public LayerMask targetMask;        
    public LayerMask ignoreTargetMask;  
    public float offsetFactor;          
 
    float originalDistance;             
    float originalScale;                
    Vector3 targetScale;                
 
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
 
    void Update()
    {
        HandleInput();
        ResizeTarget();
    }
 
    void HandleInput()
{
    if (Input.GetMouseButtonDown(0))
    {
        if (target == null)
        {
            RaycastHit hit;

            // 벽을 포함한 레이어 마스크로 Raycast
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, targetMask | ignoreTargetMask))
            {
                // 맞은 오브젝트가 "타겟 가능한 오브젝트인지" 확인
                if (((1 << hit.collider.gameObject.layer) & targetMask) != 0)
                {
                    target = hit.transform;
                    target.GetComponent<Rigidbody>().isKinematic = true;

                    originalDistance = Vector3.Distance(transform.position, target.position);
                    originalScale = target.localScale.x;
                    targetScale = target.localScale;
                }
            }
        }
        else
        {
            target.GetComponent<Rigidbody>().isKinematic = false;
            target = null;
        }
    }
}
    void ResizeTarget()
    {
       
        if (target == null)
        {
            
            return;
        }
 
       
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, ignoreTargetMask))
        {
            
            target.position = hit.point - transform.forward * offsetFactor * targetScale.x;
 
            
            float currentDistance = Vector3.Distance(transform.position, target.position);
 
            
            float s = currentDistance / originalDistance;
 
            
            targetScale.x = targetScale.y = targetScale.z = s;
 
            
            target.localScale = targetScale * originalScale;
        }
    }
}
