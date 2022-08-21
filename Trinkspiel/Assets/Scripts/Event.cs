using System.Collections;
using System.Collections.Generic;
using DM.DrinkCard;
using UnityEngine;

public class Event : ScriptableObject
{
    [SerializeField] private Category category;
    [SerializeField] private string description;

    public Category Category { get => category; }
    public string Description { get => description; }
}
