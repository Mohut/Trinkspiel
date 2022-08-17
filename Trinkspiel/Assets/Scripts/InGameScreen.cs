using System;
using DM.DrinkCard;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class InGameScreen : MonoBehaviour
{
    [SerializeField] private UIDocument inGameDocument;
    [SerializeField] private UIDocument cardDocument;
    private Button button1;
    private Button button2;
    private Button button3;
    private VisualTreeAsset cardTree;
    private VisualElement card;
    private Label categoryTopText;
    private Label categoryBottomText;
    private Label descriptionText;
    private Label sipText;

    private void Start()
    {
        GameManager.INSTANCE.ChooseCards();
        
        button1 = inGameDocument.rootVisualElement.Q<Button>("1Sip");
        button2 = inGameDocument.rootVisualElement.Q<Button>("2Sips");
        button3 = inGameDocument.rootVisualElement.Q<Button>("3Sips");

        categoryTopText = cardDocument.rootVisualElement.Q<Label>("CategoryTop");
        categoryBottomText = cardDocument.rootVisualElement.Q<Label>("CategoryBottom");
        descriptionText = cardDocument.rootVisualElement.Q<Label>("Description");
        sipText = cardDocument.rootVisualElement.Q<Label>("SipText");
        
        button1.clicked += ShowCard(GameManager.INSTANCE.DrinkCard1);
        button2.clicked += ShowCard(GameManager.INSTANCE.DrinkCard2);
        button3.clicked += ShowCard(GameManager.INSTANCE.DrinkCard3);
    }

    private void OnDestroy()
    {
        button1.clicked -= ShowCard(GameManager.INSTANCE.DrinkCard1);
        button2.clicked -= ShowCard(GameManager.INSTANCE.DrinkCard2);
        button3.clicked -= ShowCard(GameManager.INSTANCE.DrinkCard3);
    }
    
    public Action ShowCard(DrinkCard drinkCard)
    {
        return delegate { EnableCard(); FillCard(drinkCard); };
    }
    
    public void EnableCard()
    {
        cardDocument.enabled = true;
        inGameDocument.enabled = false;
    }
    
    public void FillCard(DrinkCard drinkCard)
    {
        categoryTopText.text = drinkCard.Categorie.ToString();
        categoryBottomText.text = drinkCard.Categorie.ToString();
        descriptionText.text = drinkCard.Description;
        sipText.text = drinkCard.Sips.ToString();
    }
}
