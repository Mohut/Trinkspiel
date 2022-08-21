using System;
using DM.DrinkCard;
using UnityEngine;
using UnityEngine.UIElements;

public class InGameScreen : MonoBehaviour
{
    [SerializeField] private UIDocument inGameDocument;
    [SerializeField] private UIDocument cardDocument;
    [SerializeField] private UIDocument drinkInfoDocument;
    [SerializeField] private UIDocument eventInfo;
    private VisualElement inGameDocumentRoot;
    private VisualElement cardDocumentRoot;
    private VisualElement drinkInfoDocumentRoot;
    private VisualElement eventInfoRoot;
    
    private Button button1;
    private Button button2;
    private Button button3;

    private Label categoryTopText;
    private Label categoryBottomText;
    private Label descriptionText;
    private Label sipText;
    private Label cardStack;
    private Slider slider;

    private Label stack;
    private Label playerName;

    private Label drinkInfoText;
    private Button continueButton;

    private Button eventContinueButton;
    private Label eventText;

    private void Start()
    {
        GameManager.INSTANCE.SummarizeCards();
        GameManager.INSTANCE.ChooseCards();
        GameManager.INSTANCE.ChooseRandomName();

        inGameDocumentRoot = inGameDocument.rootVisualElement;
        cardDocumentRoot = cardDocument.rootVisualElement;
        drinkInfoDocumentRoot = drinkInfoDocument.rootVisualElement;
        eventInfoRoot = eventInfo.rootVisualElement;

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
        cardStack = cardDocumentRoot.Q<Label>("Stack");
        slider = cardDocumentRoot.Q<Slider>("DoneSlider");
        drinkInfoText = drinkInfoDocumentRoot.Q<Label>("InfoText");
        continueButton = drinkInfoDocumentRoot.Q<Button>("Continue");
        eventText = eventInfoRoot.Q<Label>("EventText");
        continueButton.clicked += NewRound;

        eventContinueButton = eventInfoRoot.Q<Button>("Continue");
        eventContinueButton.clicked += NewRound;
        
        eventInfoRoot.style.display = DisplayStyle.None;
        cardDocumentRoot.style.display = DisplayStyle.None;
        drinkInfoDocumentRoot.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (slider.value <= 0)
        {
            slider.value = 50;
            NotCompletedTask();
        }

        if (slider.value >= 100)
        {
            slider.value = 50;
            CompletedTask();
        }
    }

    private void ResetButtons()
    {
        button1.clicked -= ShowCard(GameManager.INSTANCE.DrinkCard1);
        button2.clicked -= ShowCard(GameManager.INSTANCE.DrinkCard2);
        button3.clicked -= ShowCard(GameManager.INSTANCE.DrinkCard3);
    }
    
    public Action ShowCard(DrinkCard drinkCard)
    {
        return delegate { EnableCard(); FillCard(drinkCard); SetCurrentDrinkCard(drinkCard); };
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
        descriptionText.text = drinkCard.Description.Replace("***", GameManager.INSTANCE.OtherName);
        sipText.text = drinkCard.Sips.ToString();
        cardStack.text = GameManager.INSTANCE.Stack.ToString();
    }

    public void SetCurrentDrinkCard(DrinkCard drinkCard)
    {
        GameManager.INSTANCE.CurrentDrinkCard = drinkCard;
    }

    public void CompletedTask()
    {
        GameManager.INSTANCE.AddToStack(GameManager.INSTANCE.CurrentDrinkCard.Sips);
        
        if (GameManager.INSTANCE.Stack >= GameManager.INSTANCE.MaxStack)
        {
            cardDocumentRoot.style.display = DisplayStyle.None;
            drinkInfoDocumentRoot.style.display = DisplayStyle.Flex;
            drinkInfoText.text = GameManager.INSTANCE.CurrentName + " verteilt " + GameManager.INSTANCE.Stack + " Schlücke";
            GameManager.INSTANCE.ResetStack();
        }
        else
        {
            NewRound();
        }
    }

    public void NotCompletedTask()
    {
        cardDocumentRoot.style.display = DisplayStyle.None;
        drinkInfoDocumentRoot.style.display = DisplayStyle.Flex;
        drinkInfoText.text = GameManager.INSTANCE.CurrentName + " trinkt " + GameManager.INSTANCE.Stack + " Schlücke";
        GameManager.INSTANCE.ResetStack();
    }

    public void NewRound()
    {
        GameManager.INSTANCE.CheckForEventRound();
        if (GameManager.INSTANCE.EventRound)
        {
            GameManager.INSTANCE.ChooseEvent();
            eventText.text = GameManager.INSTANCE.CurrentEvent.Description;
            drinkInfoDocumentRoot.style.display = DisplayStyle.None;
            cardDocumentRoot.style.display = DisplayStyle.None;
            eventInfoRoot.style.display = DisplayStyle.Flex;
            inGameDocumentRoot.style.display = DisplayStyle.None;
            return;
        }
        
        GameManager.INSTANCE.ChooseCards();
        GameManager.INSTANCE.ChooseRandomName();

        playerName.text = GameManager.INSTANCE.CurrentName;
        stack.text = GameManager.INSTANCE.Stack.ToString();
        
        ResetButtons();

        button1.clicked += ShowCard(GameManager.INSTANCE.DrinkCard1);
        button2.clicked += ShowCard(GameManager.INSTANCE.DrinkCard2);
        button3.clicked += ShowCard(GameManager.INSTANCE.DrinkCard3);
        
        button1.text = GameManager.INSTANCE.DrinkCard1.Sips.ToString();
        button2.text = GameManager.INSTANCE.DrinkCard2.Sips.ToString();
        button3.text = GameManager.INSTANCE.DrinkCard3.Sips.ToString();
        
        drinkInfoDocumentRoot.style.display = DisplayStyle.None;
        cardDocumentRoot.style.display = DisplayStyle.None;
        eventInfoRoot.style.display = DisplayStyle.None;
        inGameDocumentRoot.style.display = DisplayStyle.Flex;
    }
}
