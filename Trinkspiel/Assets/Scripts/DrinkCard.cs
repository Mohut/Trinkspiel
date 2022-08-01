using UnityEngine;

namespace DM.DrinkCard
{ 
    public enum Categorie
    {
        Normal,
        Notnormal
    }
    
    [CreateAssetMenu(menuName = "Drink Card")]
   public class DrinkCard : ScriptableObject
   {
       [SerializeField] private Categorie categorie;
       [SerializeField] private int sips;
       [SerializeField] private string description;
   }
}

