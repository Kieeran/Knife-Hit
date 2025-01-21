using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowKnife : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private float force;

    private GameObject currentKnife;
    private void Start()
    {
        CreateNewKnife();
    }

    private void CreateNewKnife()
    {
        currentKnife = Instantiate(knifePrefab, transform);

        currentKnife.GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Debug.Log("Touch the screen");

            Rigidbody2D rigidbody2D = currentKnife.AddComponent<Rigidbody2D>();

            rigidbody2D.gravityScale = 0;

            rigidbody2D.AddForce(Vector2.up * force * Time.deltaTime, ForceMode2D.Impulse);

            currentKnife.GetComponent<Collider2D>().isTrigger = false;

            Destroy(currentKnife, 1f);

            CreateNewKnife();
        }
    }
}
