using System;
using DM.DrinkCard;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DrinkCardLists drinkCardLists;
    [SerializeField] private UIDocument UIDocument;
    private VisualTreeAsset cardTree;
    private VisualElement card;
    private TextField categorieTopText;
    private TextField categorieBottomText;
    private TextField descriptionText;
    private TextField sipText;

    private void Start()
    {
        cardTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/Card.uxml");
        Debug.Log(cardTree.name);
    }

    public void ShowCard(int sips)
    {
        UIDocument.rootVisualElement.Q<VisualElement>("Cards").SetEnabled(false);
        UIDocument.rootVisualElement.Q<VisualElement>("Card").SetEnabled(true);
        categorieTopText = card.Q<TextField>("CategorieTop");
        categorieBottomText = card.Q<TextField>("CategorieBottom");
        descriptionText = card.Q<TextField>("DescriptionText");
        sipText = card.Q<TextField>("SipText");
    }

    public void FillCard(DrinkCard drinkCard)
    {
        card = cardTree.CloneTree();
        UIDocument.rootVisualElement.Q("Card").Add(card);
        categorieTopText.SetValueWithoutNotify(drinkCard.Categorie.ToString());
        categorieBottomText.SetValueWithoutNotify(drinkCard.Categorie.ToString());
        descriptionText.SetValueWithoutNotify(drinkCard.Description);
        sipText.SetValueWithoutNotify(drinkCard.Sips.ToString());
    }
}
