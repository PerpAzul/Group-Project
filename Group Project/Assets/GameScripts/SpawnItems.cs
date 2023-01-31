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
    public Transform InvisibleSpawn;
    public Transform DoubleSpawn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnTimerAmmoCoroutine());
        StartCoroutine(SpawnTimerHealzCoroutine());
        StartCoroutine(SpawnTimerInvisCoroutine());
        StartCoroutine(SpawnTimerDoubleDamageCoroutine());
    }
    
    IEnumerator SpawnTimerAmmoCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(5f, 15f));
            Instantiate(Ammo, AmmoSpawn.position, AmmoSpawn.rotation);
        }
    }
    IEnumerator SpawnTimerHealzCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(50f, 60f));
            Instantiate(Healz, HeartSpawn.position, HeartSpawn.rotation);
        }
    }
    IEnumerator SpawnTimerInvisCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(30f, 40f));
            Instantiate(InvisPowerUp, InvisibleSpawn.position, InvisibleSpawn.rotation);
        }
    }
    IEnumerator SpawnTimerDoubleDamageCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(20f, 30f));
            Instantiate(DoubleDamagePowerUp, DoubleSpawn.position, DoubleSpawn.rotation);
        }
    }
}
