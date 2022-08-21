using UnityEngine;

namespace DM.DrinkCard
{ 
    public enum Category
    {
        Standard,
        Bewegung,
        Heiß,
        NiceVibes,
        Kindisch
    }
    
    [CreateAssetMenu(menuName = "Drink Card")]
   public class DrinkCard : ScriptableObject
   {
       [SerializeField] private Category categorie;
       [SerializeField] private int sips;
       [SerializeField] private string description;

       public Category Categorie { get => categorie; }
       public int Sips { get => sips; }
       public string Description { get => description; }
   }
}

