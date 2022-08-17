using System.Collections.Generic;
using System.Linq;
using DM.DrinkCard;
using UnityEngine;
using Random = System.Random;


public class GameManager : MonoBehaviour
{
    public static GameManager INSTANCE;
    [SerializeField] private DrinkCardLists drinkCardLists;
    
    private List<string> names = new List<string>();
    private int stack = 0;
    private DrinkCard drinkCard1;
    private DrinkCard drinkCard2;
    private DrinkCard drinkCard3;
    private Random random = new Random();
    public List<string> Names { set => names = value; }
    public DrinkCard DrinkCard1 { get => drinkCard1; }
    public DrinkCard DrinkCard2 { get => drinkCard2; }
    public DrinkCard DrinkCard3 { get => drinkCard3; }

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

    public void RandomizeNameList()
    {
        List<string> shuffled = names.OrderBy(_ => random.Next()).ToList();
        names = shuffled;
    }

    public void ChooseCards()
    {
        drinkCard1 = drinkCardLists.GetRandomNormal1Card();
        drinkCard2 = drinkCardLists.GetRandomNormal2Card();
        drinkCard3 = drinkCardLists.GetRandomNormal3Card();
    }
}
