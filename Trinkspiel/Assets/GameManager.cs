using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    [SerializeField] private GameObject nameSelectScreenObject;
    [SerializeField] private GameObject inGameScreenObject;
    [SerializeField] private InGameScreen inGameScreen;
    [SerializeField] private NameSelectScreen nameSelectScreen;
    
    private List<string> names = new List<string>();
    private int stack = 0;

    private void Start()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnEnable()
    {
        nameSelectScreen.car
    }

    public void StartGame()
    {
        nameSelectScreenObject.SetActive(false);
        inGameScreenObject.SetActive(true);
    }

    public void SetNames(List<string> names)
    {
        this.names = names;
    }

    public void ShowCard()
    {
        inGameScreen.ShowCard();
    }
}
