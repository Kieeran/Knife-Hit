using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowKnife : MonoBehaviour
{
    [SerializeField] private Transform objectHolder;
    [SerializeField] private float force;
    [SerializeField] private float touchCoolDown;
    [SerializeField] private Transform holder;
    private bool canTouch = true;

    private Vector2 desPoint;
    private GameObject currentKnife;
    private int currentKnifeAmount = 0;

    public void SetCurrentKnifeAmount(int amount) { currentKnifeAmount = amount; }
    private void Start()
    {
        CreateNewKnife();
        desPoint = Vector2.zero;
        FindDesPoint();
    }

    private void FindDesPoint()
    {
        float radius = holder.GetComponent<SpawnObjects>().GetKnifeRadius();
        Vector2 ToTarget = new Vector2();
        ToTarget.x = holder.position.x - transform.position.x;
        ToTarget.y = holder.position.y - transform.position.y;

        float distanceToTarget = (float)Math.Sqrt(ToTarget.x * ToTarget.x + ToTarget.y * ToTarget.y);

        desPoint.x = holder.position.x - radius * (ToTarget.x / distanceToTarget);
        desPoint.y = holder.position.y - radius * (ToTarget.y / distanceToTarget);

        //Debug.Log(desPoint);
    }

    private void CreateNewKnife()
    {
        currentKnife = KnifeManager.Instance.GetThrowKnifeByID(0);
        currentKnife.transform.SetParent(transform);
        currentKnife.transform.localPosition = Vector2.zero;

        currentKnife.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (canTouch == true)
        {
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                Rigidbody2D rigidbody2D = currentKnife.GetComponent<Rigidbody2D>();
                rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);

                currentKnife.GetComponent<Collider2D>().enabled = true;

                canTouch = false;
            }
        }

        if (currentKnife == null) return;

        float distance = Vector2.Distance(currentKnife.transform.position, desPoint);

        if (distance > 0 && distance <= 1f)
        {
            currentKnife.transform.position = desPoint;
            currentKnife.gameObject.SetActive(false);

            GameObject knife = KnifeManager.Instance.GetSpawnKnife();
            knife.transform.SetParent(objectHolder);
            knife.transform.position = currentKnife.transform.position;
            knife.transform.rotation = currentKnife.transform.rotation;

            Destroy(currentKnife);
            CreateNewKnife();
            canTouch = true;
        }
    }

    private IEnumerator TurnOnCanTouch()
    {
        yield return new WaitForSeconds(touchCoolDown);
        canTouch = true;
    }
}
