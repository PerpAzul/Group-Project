using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTpPlatform : MonoBehaviour
{
    private static BoxCollider boden;
    private static MeshRenderer bodenTextur;
    void Start()
    {
        boden = this.GetComponent<BoxCollider>();
        bodenTextur = this.GetComponent<MeshRenderer>();
    }
    /*private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter()");
        Invoke("removePlatform", 5);
    }*/
    public static void removePlatform()
    {
        Debug.Log("removePlatform()");
        boden.enabled = false;
        bodenTextur.enabled = false;
        //Invoke("addPlatform", 5);
    }
    public static void addPlatform()
    {
        Debug.Log("addPlatform()");
        boden.enabled = true;
        bodenTextur.enabled = true;
    }
}
