using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHeart : MonoBehaviour
{
    [SerializeField] float speedZ;
    private MeshRenderer Body;

    private void Awake()
    {
        Body = GetComponent<MeshRenderer>();
        StartCoroutine(BlinkBeforeDespawn());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * speedZ * Time.deltaTime);
    }

    /* //Alte Version
    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
    */
    IEnumerator BlinkBeforeDespawn()
    {
        float m = 2; //um die Liegedauer der Items anzupassen
        yield return new WaitForSeconds(m*3.5f);
        
        //Blinkt, um anzuzeigen, dass es gleich verschwindet
        Body.enabled = false;
        yield return new WaitForSeconds(m*0.1f);
        Body.enabled = true;
        yield return new WaitForSeconds(m*0.5f);
        Body.enabled = false;
        yield return new WaitForSeconds(m*0.1f);
        Body.enabled = true;
        yield return new WaitForSeconds(m*0.4f);
        Body.enabled = false;
        yield return new WaitForSeconds(m*0.1f);
        Body.enabled = true;
        yield return new WaitForSeconds(m*0.3f);

        Destroy(gameObject);
    }
}
