using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name.Contains("KnifeSpawn_32"))
        {
            Debug.Log("Collide with " + collider2D.gameObject.name + " as trigger");

            Destroy(gameObject);
        }
    }
}