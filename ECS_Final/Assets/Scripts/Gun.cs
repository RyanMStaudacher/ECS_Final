using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public static event Action PlacedGun;

    [Tooltip("The drawer that the gun will be placed into")]
    [SerializeField] private GameObject drawer;

    private bool drawerIsOpen = false;
    private bool hasPlacedGun = false;

    private void OnEnable()
    {
        Drawer.opened += SetBoolTrue;
        Drawer.closed += SetBoolFalse;
        Door.Flushed += GunStolen;
    }

    private void OnDisable()
    {
        Drawer.opened -= SetBoolTrue;
        Drawer.closed -= SetBoolFalse;
        Door.Flushed -= GunStolen;
    }

    private void SetBoolTrue()
    {
        drawerIsOpen = true;
    }

    private void SetBoolFalse()
    {
        drawerIsOpen = false;
    }

    private void Update()
    {
        PlaceGun();
    }

    private void PlaceGun()
    {
        if (drawerIsOpen)
        {
            if (Input.GetButtonDown("PlaceGun") && !hasPlacedGun)
            {
                this.gameObject.transform.SetParent(null);
                this.gameObject.transform.SetPositionAndRotation(new Vector3(18.321f, 0.925f, -12.917f), Quaternion.Euler(0f, -180f, 90f));
                this.gameObject.transform.SetParent(drawer.transform);
                if (PlacedGun != null)
                    PlacedGun.Invoke();
                hasPlacedGun = true;
            }
        }
    }

    private void GunStolen()
    {
        this.gameObject.SetActive(false);
    }
}
