using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject Ammo;
    public GameObject Healz;
    public GameObject SkyTP;
    public Transform AmmoSpawn;
    public Transform HealzSpawn;
    public Transform SkyTPSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimerAmmoCoroutine());
        StartCoroutine(SpawnTimerHealzCoroutine());
        StartCoroutine(SpawnTimerSkyTpCoroutine());

        /*
        Instantiate(Ammo, AmmoSpawn.position, AmmoSpawn.rotation);
        Instantiate(Healz, HealzSpawn.position, HealzSpawn.rotation);
        Instantiate(SkyTP, SkyTPSpawn.position, SkyTPSpawn.rotation);
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnTimerAmmoCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(10f);
            Instantiate(Ammo, AmmoSpawn.position, AmmoSpawn.rotation);
        }
    }
    IEnumerator SpawnTimerHealzCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(30f);
            Instantiate(Healz, AmmoSpawn.position, AmmoSpawn.rotation);
        }
    }
    IEnumerator SpawnTimerSkyTpCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(60f);
            Instantiate(SkyTP, SkyTPSpawn.position, SkyTPSpawn.rotation);
        }
    }
}
