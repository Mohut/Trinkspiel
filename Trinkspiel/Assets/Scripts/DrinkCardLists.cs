using System.Collections.Generic;
using DM.DrinkCard;
using UnityEngine;
using Random = System.Random;


public class DrinkCardLists : MonoBehaviour
{
    [Header("Drink Cards")]
    [SerializeField] private List<DrinkCard> standard;
    [SerializeField] private List<DrinkCard> movement;
    [SerializeField] private List<DrinkCard> hot;
    [SerializeField] private List<DrinkCard> niceVibes;
    [SerializeField] private List<DrinkCard> childish;
    [Header("Events")]
    [SerializeField] private List<Event> standardEvents;
    [SerializeField] private List<Event> movementEvents;
    [SerializeField] private List<Event> hotEvents;
    [SerializeField] private List<Event> niceVibesEvents;
    [SerializeField] private List<Event> childishEvents;
    private bool standardActive = true;
    private bool movementActive = true;
    private bool hotActive = true;
    private bool niceVibesActive = true;
    private bool childishActive = true;
    private List<DrinkCard> drinkCards = new List<DrinkCard>();
    private List<Event> eventCards = new List<Event>();
    private static Random random = new Random();

    public List<DrinkCard> Standard { get => standard; }
    public List<DrinkCard> Movement { get => movement; }
    public List<DrinkCard> Hot { get => hot; }
    public List<DrinkCard> NiceVibes { get => niceVibes; }
    public List<DrinkCard> Childish { get => childish; }

    public void SummarizeCards()
    {
        drinkCards.Clear();

        if (standardActive)
        {
            drinkCards.AddRange(standard);
            eventCards.AddRange(standardEvents);
        }

        if (movementActive)
        {
            drinkCards.AddRange(movement);
            eventCards.AddRange(movementEvents);
        }
        
        if (hotActive)
        { 
            drinkCards.AddRange(hot);
            eventCards.AddRange(hotEvents);
        }
        
        if (niceVibesActive)
        {
            drinkCards.AddRange(niceVibes);
            eventCards.AddRange(niceVibesEvents);
        }

        if (childishActive)
        {
            drinkCards.AddRange(childish);
            eventCards.AddRange(childishEvents);
        }
    }
    public DrinkCard GetRandomCard()
    {
        return drinkCards[random.Next(drinkCards.Count)];
    }

    public Event GetRandomEvent()
    {
        Debug.Log(eventCards.Count);
        return eventCards[random.Next(eventCards.Count)];
    }

    public void EnableDisableStandard()
    {
        standardActive = !standardActive;
        
        if (standardActive)
        {
            GameManager.INSTANCE.CardAmount += standard.Count;
        }
        else
        {
            GameManager.INSTANCE.CardAmount -= standard.Count;
        }
    }
    
    public void EnableDisableMovement()
    {
        movementActive = !movementActive;

        if (movementActive)
        {
            GameManager.INSTANCE.CardAmount += movement.Count;
        }
        else
        {
            GameManager.INSTANCE.CardAmount -= movement.Count;
        }
    }
    
    public void EnableDisableHot()
    {
        hotActive = !hotActive;

        if (hotActive)
        {
            GameManager.INSTANCE.CardAmount += hot.Count;
        }
        else
        {
            GameManager.INSTANCE.CardAmount -= hot.Count;
        }
    }
    
    public void EnableDisableNiceVibes()
    {
        niceVibesActive = !niceVibesActive;

        if (niceVibesActive)
        {
            GameManager.INSTANCE.CardAmount += niceVibes.Count;
        }
        else
        {
            GameManager.INSTANCE.CardAmount -= niceVibes.Count;
        }
    }
    
    public void EnableDisableChildish()
    {
        childishActive = !childishActive;

        if (childishActive)
        {
            GameManager.INSTANCE.CardAmount += childish.Count;
        }
        else
        {
            GameManager.INSTANCE.CardAmount -= childish.Count;
        }
    }

    public bool CheckForDecks()
    {
        bool result = false;

        if (standardActive) 
            result = true;
        
        if (movementActive)
            result = true;
        
        if (hotActive)
            result = true;
        
        if (niceVibesActive)
            result = true;
        
        if (childishActive)
            result = true;
        
        return result;
    } 
}
