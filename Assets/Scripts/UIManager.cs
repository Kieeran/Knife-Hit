using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Transform gameOverPopUp;
    [SerializeField] private Transform homeScreen;
    [SerializeField] private Transform knifeContainer;

    [SerializeField] private Button playGame;

    [SerializeField] private Transform knifeAmountBar;
    [SerializeField] private GameObject knifeIconPrefab;

    [SerializeField] private TMP_Text appleCoinNumFront;
    [SerializeField] private TMP_Text appleCoinNumBack;
    [SerializeField] private TMP_Text appleCoinNumShop;

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

        gameOverPopUp.Find("ReturnHomeButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            homeScreen.gameObject.SetActive(true);
            gameOverPopUp.gameObject.SetActive(false);
        });

        playGame.onClick.AddListener(() =>
        {
            homeScreen.gameObject.SetActive(false);

            GameManager.Instance.RestartGame();
        });

        for (int i = 0; i < knifeContainer.childCount; i++)
        {
            int currentIndex = i;

            knifeContainer.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.Instance.SetCurrentKnifeID(KnifeManager.Instance.GetKnifeConfigs()[currentIndex].knifeID);
            });
        }
    }

    public void ShowAppleCoinUI(int amount)
    {
        appleCoinNumFront.text = amount.ToString();
        appleCoinNumBack.text = amount.ToString();
        appleCoinNumShop.text = amount.ToString();
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
