using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    [SerializeField] private InGameScreen inGameScreen;
    [SerializeField] private NameSelectScreen nameSelectScreen;
    private List<string> names = new List<string>();
    private int stack = 0; 
    public event Action startGameEvent;
    public List<string> Names { set => names = value; }

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

    public void StartGame()
    {
        startGameEvent.Invoke();
    }
}
