using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float speed = 1000f;
    [SerializeField] private Camera fpsCam;
    private bool isShooting = false;
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject bulletPrefab;
    public static float ammo = 1000;
    [SerializeField] private TMPro.TextMeshProUGUI ammoUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
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
}
