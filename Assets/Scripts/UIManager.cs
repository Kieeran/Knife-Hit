using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Transform knifeAmountBar;
    [SerializeField] private Transform gameOverPopUp;
    [SerializeField] private GameObject knifeIconPrefab;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        gameOverPopUp.Find("RestartButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            GameManager.Instance.RestartGame();
            gameOverPopUp.gameObject.SetActive(false);
        });
    }

    public void OpenGameOverPopUp()
    {
        gameOverPopUp.gameObject.SetActive(true);
    }

    public void SetupKnifeAmountBar(int amount)
    {
        foreach (Transform child in knifeAmountBar)
        {
            if (child.gameObject.activeSelf == true)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < amount; i++)
        {
            GameObject obj = Instantiate(knifeIconPrefab, knifeAmountBar);
            obj.SetActive(true);
        }
    }

    public void RemoveKnife()
    {
        foreach (Transform child in knifeAmountBar)
        {
            if (child.gameObject.activeSelf == false) continue;

            Image childImage = child.GetComponent<Image>();
            if (childImage != null && childImage.color != Color.black)
            {
                childImage.color = Color.black;
                return;
            }
        }
    }
}
