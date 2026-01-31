using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator doorAnim;
    private bool doorLocked = true;




    public void OpenDoor()
    {
        doorAnim.SetTrigger("openDoor");
        doorLocked = false;
    }

    public void CloseDoor()
    {
        doorAnim.SetTrigger("closeDoor");
        doorLocked = true;
    }



}
