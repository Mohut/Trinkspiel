using DM.DrinkCard;
using UnityEngine;

[CreateAssetMenu(menuName = "Event")]
public class Event : ScriptableObject
{
    [SerializeField] private Category category;
    [SerializeField] private string description;

    public Category Category { get => category; }
    public string Description { get => description; }
}
