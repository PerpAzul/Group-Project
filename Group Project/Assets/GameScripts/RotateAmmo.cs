using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAmmo : MonoBehaviour
{
    [SerializeField] float speedY;

    private void Awake()
    {
        StartCoroutine(Despawn());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 360 * speedY * Time.deltaTime, 0);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
