using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    
    public GameObject Ammo;
    public GameObject Healz;
    public Transform AmmoSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimerAmmoCoroutine());
        StartCoroutine(SpawnTimerHealzCoroutine());
    }
    
    IEnumerator SpawnTimerAmmoCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 20f));
            Instantiate(Ammo, AmmoSpawn.position, AmmoSpawn.rotation);
        }
    }
    IEnumerator SpawnTimerHealzCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(25f, 35f));
            Instantiate(Healz, AmmoSpawn.position, AmmoSpawn.rotation);
        }
    }
}
