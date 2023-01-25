using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    //Camera and Bullet variables
    [SerializeField] private Camera fpsCam;
    [SerializeField] private float range;
    private bool isShooting = false;
    public float ammo = 10;
    
    //bulletPrefabs and Materials
    [SerializeField] private GameObject bulletPoint;
    [SerializeField] private GameObject bulletPrefab1;
    [SerializeField] private GameObject bulletPrefab2;
    [SerializeField] private Material bulletMaterial1;
    [SerializeField] private Material bulletMaterial2;
    
    private GameObject bulletPrefab;
    
    //UI variables
    [SerializeField] private TMPro.TextMeshProUGUI ammoUI;
    [SerializeField] private GameObject hitmarkerUI;
    [SerializeField] private GameObject crosshairUI;
    
    //Tags
    private Player playerTag;
    
    //Particle System
    [SerializeField] private ParticleSystem flash;
    
    //Audio Source
    [SerializeField] private AudioSource tickSource;

    private void Start()
    {
        hitmarkerUI.gameObject.SetActive(false);
        tickSource = GetComponent<AudioSource>();
    }

    public void Init(int id)
    {
        bulletPrefab = id == 0 ? bulletPrefab1 : bulletPrefab2;
        GetComponent<Renderer>().material = id == 0 ? bulletMaterial1 : bulletMaterial2;
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
            ammo--;
            tickSource.Play();
            flash.Play();
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Player target = hit.transform.GetComponent<Player>();
                if (target != null)
                {
                    hitActive();
                    Invoke("hitDisable", 0.2f);
                    target.TakeDamage();
                }
            }
        }
    }

    public void returnAmmo()
    {
        ammo = 10;
    }

    public void pickUpAmmo()
    {
        ammo = ammo + 10;
    }

    public void doShoot(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            Shoot();
        }
    }

    private void hitActive()
    {
        crosshairUI.gameObject.SetActive(false);
        hitmarkerUI.gameObject.SetActive(true);
    }

    private void hitDisable()
    {
        crosshairUI.gameObject.SetActive(true);
        hitmarkerUI.gameObject.SetActive(false);
    }
}
