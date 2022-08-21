using System.Collections.Generic;
using DM.DrinkCard;
using UnityEngine;
using Random = System.Random;


public class DrinkCardLists : MonoBehaviour
{
    [SerializeField] private List<DrinkCard> standard;
    [SerializeField] private List<DrinkCard> movement;
    [SerializeField] private List<DrinkCard> hot;
    [SerializeField] private List<DrinkCard> niceVibes;
    [SerializeField] private List<DrinkCard> childish;
    private bool standardActive = true;
    private bool movementActive = true;
    private bool hotActive = true;
    private bool niceVibesActive = true;
    private bool childishActive = true;
    private List<DrinkCard> drinkCards = new List<DrinkCard>();
    private static Random random = new Random();
    public void SummarizeCards()
    {
        drinkCards.Clear();
        
        if(standardActive)
            drinkCards.AddRange(standard);
        
        if(movementActive)
            drinkCards.AddRange(movement);
        
        if(hotActive)
            drinkCards.AddRange(hot);
        
        if(niceVibesActive)
            drinkCards.AddRange(niceVibes);
        
        if(childishActive)
            drinkCards.AddRange(childish);
    }
    public DrinkCard GetRandomCard()
    {
        return drinkCards[random.Next(drinkCards.Count)];
    }

    public void EnableDisableStandard()
    {
        standardActive = !standardActive;
    }
    
    public void EnableDisableMovement()
    {
        movementActive = !movementActive;
    }
    
    public void EnableDisableHot()
    {
        hotActive = !hotActive;
    }
    
    public void EnableDisableNiceVibes()
    {
        niceVibesActive = !niceVibesActive;
    }
    
    public void EnableDisableChildish()
    {
        childishActive = !childishActive;
    }
}
