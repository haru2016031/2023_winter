using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EventSystemCtl : MonoBehaviour
{
    public GameObject pauseUIPrefab;
    private GameObject pauseUIInstance;

    private void Start()
    {
       pauseUIInstance = Instantiate(pauseUIPrefab);

        EventSystem eventSystem = FindObjectOfType<EventSystem>();

        Button first = pauseUIInstance.GetComponentInChildren<Button>();

        eventSystem.firstSelectedGameObject = first.gameObject;
    }
}
