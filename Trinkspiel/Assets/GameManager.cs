using System;
using System.Collections.Generic;
using System.Linq;
using DM.DrinkCard;
using UnityEngine;
using Random = System.Random;


public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    [SerializeField] private DrinkCardLists drinkCardLists;
    [SerializeField] private int maxStack = 10;
    private List<DrinkCard> drinkCards;
    private List<Event> eventCards;
    private List<string> names = new List<string>();
    private int currentNameIndex = 10000;
    private string currentName = "";
    private string otherName = "";
    private int stack = 0;
    private bool eventRound;
    private DrinkCard drinkCard1;
    private DrinkCard drinkCard2;
    private DrinkCard drinkCard3;
    private DrinkCard currentDrinkCard;
    private Random random = new Random();
    public List<string> Names { set => names = value; }
    public DrinkCard DrinkCard1 { get => drinkCard1; }
    public DrinkCard DrinkCard2 { get => drinkCard2; }
    public DrinkCard DrinkCard3 { get => drinkCard3; }
    public string CurrentName { get => currentName; }
    public string OtherName { get => otherName; }
    public int Stack { get => stack; }
    public int MaxStack { get => maxStack; }
    public DrinkCard CurrentDrinkCard { get => currentDrinkCard; set => currentDrinkCard = value; }

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
        
        DontDestroyOnLoad(gameObject);
    }

    public void ChooseRandomName()
    {
        int randomNumber = random.Next(names.Count);
        int otherRandomNumber = random.Next(names.Count);
        
        while (randomNumber == currentNameIndex)
        {
            randomNumber = random.Next(names.Count);
        }

        while (randomNumber == otherRandomNumber)
        {
            otherRandomNumber = random.Next(names.Count);
        }

        currentNameIndex = randomNumber;
        currentName = names[randomNumber];
        otherName = names[otherRandomNumber];
    }

    public void ChooseCards()
    {
        drinkCard1 = drinkCardLists.GetRandomCard();
        drinkCard2 = drinkCardLists.GetRandomCard();
        drinkCard3 = drinkCardLists.GetRandomCard();
    }

    public void AddToStack(int sips)
    {
        stack += sips;
    }

    public void ResetStack()
    {
        stack = 0;
    }

    public void SummarizeCards()
    {
        drinkCardLists.SummarizeCards();
    }

    public void CheckForEventRound()
    {
        eventRound = random.Next(6) == 1 ? true : false;
    }

    public Action EnableDisableDecks(int index)
    {
        return delegate { EnableDisableDecks(index); };
    }

    public void EnableDisable(int index)
    {
        switch (index)
        {
            case 1:
                drinkCardLists.EnableDisableStandard();
                break;
            case 2:
                drinkCardLists.EnableDisableMovement();
                break;
            case 3:
                drinkCardLists.EnableDisableHot();
                break;
            case 4:
                drinkCardLists.EnableDisableNiceVibes();
                break;
            case 5:
                drinkCardLists.EnableDisableChildish();
                break;
        }
    }
}
