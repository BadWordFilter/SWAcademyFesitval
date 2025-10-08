using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//������
public class EndingTrigger : MonoBehaviour
{
    public GameObject playerCamera;       
    public GameObject mainCamera;         
    public EndingCameraMover cameraMover; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCamera.SetActive(false);      
            mainCamera.SetActive(true);         
            cameraMover.StartEnding();          
        }
    }
}
