using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.name.Contains("SpawnKnife"))
        {
            Debug.Log("Collide with " + collider2D.gameObject.name);

            Destroy(gameObject);
        }

        else if (collider2D.gameObject.name.Contains("Apple"))
        {
            Debug.Log("Collide with " + collider2D.gameObject.name);

            if (collider2D.gameObject.name.Contains("Golden"))
            {
                AppleManager.Instance.ReturnGoldenApple(collider2D.gameObject);
            }

            else
            {
                AppleManager.Instance.ReturnApple(collider2D.gameObject);
            }
        }
    }
}