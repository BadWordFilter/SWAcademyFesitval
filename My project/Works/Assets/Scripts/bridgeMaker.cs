using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeMaker : MonoBehaviour
{
    public GameObject real;
    public GameObject fake;
    public Vector3 bridgeActivePosition = new Vector3(23.83f, 13.13f, 38.17f);
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (real) real.transform.position = bridgeActivePosition;
            if (fake) fake.SetActive(false);
        }
    }

    
}
