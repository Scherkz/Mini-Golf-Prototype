using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoadingScreen : MonoBehaviour
{
    private Image mapIcon;
    private TMP_Text selectionText;

    private void Awake()
    {
        mapIcon = transform.Find("MapIcon").GetComponent<Image>();
        selectionText = transform.Find("SelectionText").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        ToggleCildren(false);
    }

    private void OnEnable()
    {
        EventBus.Instance.OnMapSelected += ShowLoadingScreen;
    }

    private void OnDisable()
    {
        EventBus.Instance.OnMapSelected -= ShowLoadingScreen;
    }

    private void ShowLoadingScreen(string mapName)
    {
        ToggleCildren(true);

        selectionText.text = $"{mapName} has been selected!";
        mapIcon.color = Color.red;
    }

    private void ToggleCildren(bool enable)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(enable);
        }
    }
}
