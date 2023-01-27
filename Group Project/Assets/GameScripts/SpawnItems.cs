using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    
    public GameObject Ammo;
    public GameObject Healz;
    public GameObject InvisPowerUp;
    public GameObject DoubleDamagePowerUp;
    public Transform AmmoSpawn;
    public Transform HeartSpawn;
    public Transform PowerUpSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimerAmmoCoroutine());
        StartCoroutine(SpawnTimerHealzCoroutine());
        // StartCoroutine(SpawnTimerInvisCoroutine());
        // StartCoroutine(SpawnTimerDoubleDamageCoroutine());
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
            yield return new WaitForSeconds(Random.Range(40f, 50f));
            Instantiate(Healz, HeartSpawn.position, HeartSpawn.rotation);
        }
    }
    // IEnumerator SpawnTimerInvisCoroutine()
    // {
    //     while(true)
    //     {
    //         yield return new WaitForSeconds(Random.Range(25f, 35f));
    //         Instantiate(InvisPowerUp, PowerUpSpawn.position, PowerUpSpawn.rotation);
    //     }
    // }
    // IEnumerator SpawnTimerDoubleDamageCoroutine()
    // {
    //     while(true)
    //     {
    //         yield return new WaitForSeconds(Random.Range(25f, 35f));
    //         Instantiate(DoubleDamagePowerUp, PowerUpSpawn.position, PowerUpSpawn.rotation);
    //     }
    // }
}
