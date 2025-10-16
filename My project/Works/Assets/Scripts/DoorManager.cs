using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//¼­¿ìÁø
public class DoorManager : MonoBehaviour
{
    public GameObject door;
    public GameObject img1;
    public GameObject img2;
    public GameObject wall;
    public GameObject fakeWall;
    public Vector3 doorActivePosition = new Vector3(-58f, -14.6f, 1797.7f);
    public Vector3 wallActivePosition = new Vector3(-4.3f, 90f, 1901.3f);
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            wall.transform.position = wallActivePosition;
            door.transform.position = doorActivePosition;
            img1.SetActive(false);
            img2.SetActive(false);
            fakeWall.SetActive(false);
        }
    }
}
