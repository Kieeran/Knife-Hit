using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThrowKnife : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private float force;
    [SerializeField] private float touchCoolDown;
    private bool canTouch = true;

    private GameObject currentKnife;
    private void Start()
    {
        CreateNewKnife();
    }

    private void CreateNewKnife()
    {
        currentKnife = Instantiate(knifePrefab, transform);

        currentKnife.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (canTouch == false) return;

        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Touch the screen");

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
