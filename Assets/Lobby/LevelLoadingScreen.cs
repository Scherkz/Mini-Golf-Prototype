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

    private void ShowLoadingScreen(MapNode map)
    {
        ToggleCildren(true);

        selectionText.text = $"{map.mapName} has been selected!";
        mapIcon.sprite = map.mapIcon;
    }

    private void ToggleCildren(bool enable)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(enable);
        }
    }
}
