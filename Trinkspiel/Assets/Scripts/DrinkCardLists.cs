using System.Collections.Generic;
using DM.DrinkCard;
using UnityEngine;
using Random = System.Random;


public class DrinkCardLists : MonoBehaviour
{
    [SerializeField] private List<DrinkCard> drinkCards;
    private static Random random = new Random();

    public DrinkCard GetRandomNormalCard()
    {
        return drinkCards[random.Next(drinkCards.Count)];
    }
}
