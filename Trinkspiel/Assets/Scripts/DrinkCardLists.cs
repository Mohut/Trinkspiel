using System.Collections.Generic;
using DM.DrinkCard;
using UnityEngine;
using Random = System.Random;


public class DrinkCardLists : MonoBehaviour
{
    [SerializeField] private List<DrinkCard> drinkCards;
    private static Random random = new Random();

    public DrinkCard GetRandomNormal1Card()
    {
        return drinkCards[random.Next(drinkCards.Count)];
    }
    public DrinkCard GetRandomNormal2Card()
    {
        return drinkCards[random.Next(drinkCards.Count)];
    }
    public DrinkCard GetRandomNormal3Card()
    {
        return drinkCards[random.Next(drinkCards.Count)];
    }
}
