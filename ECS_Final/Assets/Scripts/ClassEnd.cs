using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassEnd : MonoBehaviour
{
    public static event Action SchoolsOut;

    private bool hasRung = false;

    private void OnEnable()
    {
        Door.Flushed += EnableBox;
    }

    private void OnDisable()
    {
        Door.Flushed -= EnableBox;
    }

    private void EnableBox()
    {
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!hasRung)
        {
            GetComponent<AudioSource>().Play();
            if (SchoolsOut != null)
                SchoolsOut.Invoke();
            hasRung = true;
        }
    }
}
