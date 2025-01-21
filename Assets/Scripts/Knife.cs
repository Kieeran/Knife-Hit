using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("Collide with " + collider2D.gameObject.name + " as trigger");
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Collide with " + collision2D.gameObject.name + " as not trigger");
        Destroy(gameObject);
    }
}