using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    private NewControls inputControls;
    
    //Camera and Bullet variables
    [SerializeField] private float speed = 1000f;
    [SerializeField] private Camera fpsCam;
    private bool isShooting = false;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject bulletPrefab;
    public static float ammo = 1000;
    
    //UI variables
    [SerializeField] private TMPro.TextMeshProUGUI ammoUI;

    private void Awake()
    {
        inputControls = new NewControls();
    }

    // Update is called once per frame
    void Update()
    {
        ammoUI.text = "Ammo: " + ammo;
    }

    //Shoots
    public void Shoot()
    {
        if (ammo > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPoint.transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * speed);
            ammo--;
            Destroy(bullet, 2);   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Destroy(gameObject);
        }
    }

    public static void returnAmmo()
    {
        ammo = 1000;
    }
    
    private void OnEnable()
    {
        // inputControls.Player.Shoot.performed += doShoot;
        // inputControls.Player.Shoot.Enable();
    }
    
    public void doShoot(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Debug.Log(obj.ReadValue<float>());
            Shoot();
        }
    }
    
    private void OnDisable()
    {
        // inputControls.Player.Shoot.Disable();
    }
}
