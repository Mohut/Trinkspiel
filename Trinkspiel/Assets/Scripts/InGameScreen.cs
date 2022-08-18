using System;
using DM.DrinkCard;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class InGameScreen : MonoBehaviour
{
    [SerializeField] private UIDocument inGameDocument;
    [SerializeField] private UIDocument cardDocument;
    private VisualElement inGameDocumentRoot;
    private VisualElement cardDocumentRoot;
    
    private Button button1;
    private Button button2;
    private Button button3;

    private Label categoryTopText;
    private Label categoryBottomText;
    private Label descriptionText;
    private Label sipText;

    private Label stack;
    private Label playerName;

    private void Start()
    {
        GameManager.INSTANCE.ChooseCards();
        GameManager.INSTANCE.ChooseRandomName();
        
        inGameDocumentRoot = inGameDocument.rootVisualElement;
        cardDocumentRoot = cardDocument.rootVisualElement;
        
        button1 = inGameDocumentRoot.Q<Button>("1Sip");
        button2 = inGameDocumentRoot.Q<Button>("2Sips");
        button3 = inGameDocumentRoot.Q<Button>("3Sips");
        
        button1.clicked += ShowCard(GameManager.INSTANCE.DrinkCard1);
        button2.clicked += ShowCard(GameManager.INSTANCE.DrinkCard2);
        button3.clicked += ShowCard(GameManager.INSTANCE.DrinkCard3);

        button1.text = GameManager.INSTANCE.DrinkCard1.Sips.ToString();
        button2.text = GameManager.INSTANCE.DrinkCard2.Sips.ToString();
        button3.text = GameManager.INSTANCE.DrinkCard3.Sips.ToString();

        stack = inGameDocumentRoot.Q<Label>("Stack");
        stack.text = GameManager.INSTANCE.Stack.ToString();
        playerName = inGameDocumentRoot.Q<Label>("Name");
        playerName.text = GameManager.INSTANCE.CurrentName;

        categoryTopText = cardDocumentRoot.Q<Label>("CategoryTop");
        categoryBottomText = cardDocumentRoot.Q<Label>("CategoryBottom");
        descriptionText = cardDocumentRoot.Q<Label>("Description");
        sipText = cardDocumentRoot.Q<Label>("SipText");
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
        cardDocumentRoot.style.display = DisplayStyle.Flex;
        inGameDocumentRoot.style.display = DisplayStyle.None;
    }
    
    public void FillCard(DrinkCard drinkCard)
    {
        categoryTopText.text = drinkCard.Categorie.ToString();
        categoryBottomText.text = drinkCard.Categorie.ToString();
        descriptionText.text = drinkCard.Description;
        sipText.text = drinkCard.Sips.ToString();
    }
}
