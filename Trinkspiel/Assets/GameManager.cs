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
    private List<string> names = new List<string>();
    private int currentNameIndex = 10000;
    private string currentName = "";
    private int stack = 0;
    private DrinkCard drinkCard1;
    private DrinkCard drinkCard2;
    private DrinkCard drinkCard3;
    private Random random = new Random();
    public List<string> Names { set => names = value; }
    public DrinkCard DrinkCard1 { get => drinkCard1; }
    public DrinkCard DrinkCard2 { get => drinkCard2; }
    public DrinkCard DrinkCard3 { get => drinkCard3; }
    public string CurrentName { get => currentName; }
    public int Stack { get => stack; }
    public int MaxStack { get => maxStack; }

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
        while (randomNumber == currentNameIndex)
        {
            randomNumber = random.Next(names.Count);
        }

        currentNameIndex = randomNumber;
        currentName = names[randomNumber];
    }

    public void ChooseCards()
    {
        drinkCard1 = drinkCardLists.GetRandomNormalCard();
        drinkCard2 = drinkCardLists.GetRandomNormalCard();
        drinkCard3 = drinkCardLists.GetRandomNormalCard();
    }

    public void ResetStack()
    {
        stack = 0;
    }
}
