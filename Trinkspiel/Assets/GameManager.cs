using System;
using System.Collections.Generic;
using DM.DrinkCard;
using UnityEngine;
using Random = System.Random;


public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    [SerializeField] private DrinkCardLists drinkCardLists;
    [SerializeField] private int maxStack = 10;
    private List<DrinkCard> drinkCards = new List<DrinkCard>();
    private List<Event> eventCards;
    private List<string> names = new List<string>();
    private int currentNameIndex = 10000;
    private string currentName = "";
    private string otherName = "";
    private int stack = 1;
    private bool eventRound = false;
    private Event currentEvent;
    private DrinkCard drinkCard1;
    private DrinkCard drinkCard2;
    private DrinkCard drinkCard3;
    private DrinkCard currentDrinkCard;
    private Random random = new Random();
    private int cardAmount = 0;
    public List<string> Names { set => names = value; }
    public DrinkCard DrinkCard1 { get => drinkCard1; }
    public DrinkCard DrinkCard2 { get => drinkCard2; }
    public DrinkCard DrinkCard3 { get => drinkCard3; }
    public string CurrentName { get => currentName; }
    public string OtherName { get => otherName; }
    public int Stack { get => stack; }
    public int MaxStack { get => maxStack; }
    public DrinkCard CurrentDrinkCard { get => currentDrinkCard; set => currentDrinkCard = value; }
    public bool EventRound { get { return eventRound; }}
    public Event CurrentEvent { get { return currentEvent; } }
    public List<DrinkCard> DrinkCards { get { return drinkCards; } }
    public int CardAmount { get { return cardAmount; } set { cardAmount = value; } }

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
        
        cardAmount += drinkCardLists.Standard.Count;
        cardAmount += drinkCardLists.Movement.Count;
        cardAmount += drinkCardLists.Hot.Count;
        cardAmount += drinkCardLists.NiceVibes.Count;
        cardAmount += drinkCardLists.Childish.Count;
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

    public void ChooseEvent()
    {
        currentEvent = drinkCardLists.GetRandomEvent();
    }

    public void AddToStack(int sips)
    {
        stack += sips;
    }

    public void ResetStack()
    {
        stack = 1;
    }

    public void SummarizeCards()
    {
        drinkCardLists.SummarizeCards();
    }

    public void CheckForEventRound()
    {
        eventRound = random.Next(5) == 1 ? true : false;
    }

    public Action EnableDisableDecks(int index)
    {
        return delegate { EnableDisable(index); };
    }

    private void EnableDisable(int index)
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
