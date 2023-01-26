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
    
    //Init
    private int playerIndex;
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
    
    //Paused
    private PauseMenuControl pauseMenu;

    private void Start()
    {
        hitmarkerUI.gameObject.SetActive(false);
        tickSource = GetComponent<AudioSource>();
        pauseMenu = FindObjectOfType<PauseMenuControl>();
    }

    public void Init(int id)
    {
        if (id == 0)
        {
            playerIndex = 0;
        }
        else
        {
            playerIndex = 1;
        }
        GetComponent<Renderer>().material = id == 0 ? bulletMaterial1 : bulletMaterial2;
    }

    // Update is called once per frame
    void Update()
    {
        ammoUI.text = "Ammo: " + ammo;
        pauseMenu = FindObjectOfType<PauseMenuControl>();
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
                if (target != null && playerIndex != target.playerIndex)
                {
                    if (target.lives == 1)
                    {
                        
                    }
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
        if (obj.performed && pauseMenu.isPaused == false)
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
