using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollectible : MonoBehaviour
{
    public int collectedItems = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            collectedItems++;

            Destroy(other.gameObject);
        }
    }
}
