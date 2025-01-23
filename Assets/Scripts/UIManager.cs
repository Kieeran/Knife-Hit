using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] private Transform knifeAmountBar;
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

    public void SetupKnifeAmountBar(int amount)
    {
        foreach (Transform child in knifeAmountBar)
        {
            if (child.gameObject.activeSelf == true)
            {
                Destroy(child);
            }
        }

        for (int i = 0; i < amount; i++)
        {
            Instantiate(knifeIconPrefab, knifeAmountBar);
        }
    }

    public void RemoveKnife()
    {
        for (int i = knifeAmountBar.childCount - 1; i >= 0; i--)
        {
            Transform child = knifeAmountBar.GetChild(i);

            Image childImage = child.GetComponent<Image>();

            if (childImage != null && childImage.color != Color.black)
            {
                childImage.color = Color.black;
                return;
            }
        }
    }
}
