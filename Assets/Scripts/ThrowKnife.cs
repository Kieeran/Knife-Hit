using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowKnife : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private float force;
    [SerializeField] private float touchCoolDown;
    [SerializeField] private Transform holder;
    private bool canTouch = true;

    private Vector2 desPoint;
    private GameObject currentKnife;
    private void Start()
    {
        CreateNewKnife();
        desPoint = Vector2.zero;
        FindDesPoint();
    }

    private void FindDesPoint()
    {
        float radius = holder.GetComponent<SpawnObjects>().GetRadius();
        Vector2 ToTarget = new Vector2();
        ToTarget.x = holder.position.x - transform.position.x;
        ToTarget.y = holder.position.y - transform.position.y;

        float distanceToTarget = (float)Math.Sqrt(ToTarget.x * ToTarget.x + ToTarget.y * ToTarget.y);

        desPoint.x = holder.position.x - radius * (ToTarget.x / distanceToTarget);
        desPoint.y = holder.position.y - radius * (ToTarget.y / distanceToTarget);

        Debug.Log(desPoint);
    }

    private void CreateNewKnife()
    {
        currentKnife = Instantiate(knifePrefab, transform);

        currentKnife.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        FindDesPoint();

        if (canTouch == false) return;

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            Rigidbody2D rigidbody2D = currentKnife.GetComponent<Rigidbody2D>();
            rigidbody2D.AddForce(Vector2.up * force * Time.deltaTime, ForceMode2D.Impulse);

            currentKnife.GetComponent<Collider2D>().enabled = true;

            Destroy(currentKnife, 1f);

            CreateNewKnife();

            canTouch = false;

            StartCoroutine(TurnOnCanTouch());
        }
    }

    private IEnumerator TurnOnCanTouch()
    {
        yield return new WaitForSeconds(touchCoolDown);
        canTouch = true;
    }
}
