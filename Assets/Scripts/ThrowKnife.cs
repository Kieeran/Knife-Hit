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
    private bool canTouch;

    private Vector2 desPoint;
    private GameObject currentKnife;
    private int knifeAmount = 0;

    public void SetKnifeAmount(int amount)
    {
        knifeAmount = amount;

        ResetThrowKnife();

        UIManager.Instance.SetupKnifeAmountBar(knifeAmount);
    }
    private void Start()
    {
        ResetThrowKnife();
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
    }

    private void CreateNewKnife()
    {
        currentKnife = KnifeManager.Instance.GetThrowKnifeByID(GameManager.Instance.GetCurrentKnifeID());
        currentKnife.transform.SetParent(transform);
        currentKnife.transform.localPosition = Vector2.zero;

        currentKnife.GetComponent<Collider2D>().enabled = false;
    }

    public void ResetThrowKnife()
    {
        canTouch = true;
        if (currentKnife == null)
        {
            CreateNewKnife();
        }

        desPoint = Vector2.zero;
        FindDesPoint();
    }

    private void Update()
    {
        if (knifeAmount <= 0) return;

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

            knife.GetComponent<SpriteRenderer>().sprite = KnifeManager.Instance.GetKnifeConfigs()[GameManager.Instance.GetCurrentKnifeID()].knifeSkin;

            KnifeManager.Instance.ReturnThrowKnife(currentKnife);
            CreateNewKnife();
            canTouch = true;

            knifeAmount--;

            UIManager.Instance.RemoveKnife();
            GameManager.Instance.UpdateScore();

            if (knifeAmount == 0)
            {
                GameManager.Instance.Win();
            }
        }
    }
}
