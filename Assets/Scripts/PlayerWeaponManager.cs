using System.Globalization;
using System.Collections;
using System;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("Sterowanie")]
    [SerializeField]
    private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField]
    private KeyCode reloadKey = KeyCode.R;

    public Action ShootHandler;
    public Action ReloadHandler;
    void Update()
    {
        if (Input.GetKeyDown(shootKey)) Shoot();
        if (Input.GetKeyDown(reloadKey)) Reload();
    }

    private void Shoot()
    {
        ShootHandler?.Invoke();

    }
    private void Reload()
    {
        ReloadHandler?.Invoke();
    }
}
