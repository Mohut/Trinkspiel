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
    [SerializeField] private Sprite standardSprite;
    [SerializeField] private Sprite movementSprite;
    [SerializeField] private Sprite hotSprite;
    [SerializeField] private Sprite niceVibesSprite;
    [SerializeField] private Sprite childishSprite;
    private VisualElement inGameDocumentRoot;
    private VisualElement cardDocumentRoot;
    private VisualElement drinkInfoDocumentRoot;
    private VisualElement eventInfoRoot;
    
    private Button button1;
    private Button button2;
    private Button button3;
    private Label button1Category;
    private Label button2Category;
    private Label button3Category;
    
    private Label descriptionText;
    private Label sipText;
    private Label cardStack;
    private Slider slider;
    private IMGUIContainer cardBackground;

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

        button1Category = inGameDocumentRoot.Q<Label>("Category1");
        button2Category = inGameDocumentRoot.Q<Label>("Category2");
        button3Category = inGameDocumentRoot.Q<Label>("Category3");

        SubscribeNewCards();

        stack = inGameDocumentRoot.Q<Label>("Stack");
        stack.text = GameManager.INSTANCE.Stack.ToString();
        playerName = inGameDocumentRoot.Q<Label>("Name");
        playerName.text = GameManager.INSTANCE.CurrentName;
        
        descriptionText = cardDocumentRoot.Q<Label>("Description");
        sipText = cardDocumentRoot.Q<Label>("SipText");
        cardStack = cardDocumentRoot.Q<Label>("Stack");
        slider = cardDocumentRoot.Q<Slider>("DoneSlider");
        cardBackground = cardDocumentRoot.Q<IMGUIContainer>("Background");
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
        cardBackground.style.backgroundImage = ChooseSprite(drinkCard.Categorie);
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

        SubscribeNewCards();
        
        drinkInfoDocumentRoot.style.display = DisplayStyle.None;
        cardDocumentRoot.style.display = DisplayStyle.None;
        eventInfoRoot.style.display = DisplayStyle.None;
        inGameDocumentRoot.style.display = DisplayStyle.Flex;
    }

    public StyleBackground ChooseSprite(Category category)
    {
        switch (category)
        {
            case Category.Standard:
                return new StyleBackground(standardSprite);
            case Category.Bewegung:
                return new StyleBackground(movementSprite);
            case Category.Heiß:
                return new StyleBackground(hotSprite);
            case Category.NiceVibes:
                return new StyleBackground(niceVibesSprite);
            case Category.Kindisch:
                return new StyleBackground(childishSprite);
        }

        return new StyleBackground(standardSprite);
    }

    public void SubscribeNewCards()
    {
        button1.clicked += ShowCard(GameManager.INSTANCE.DrinkCard1);
        button2.clicked += ShowCard(GameManager.INSTANCE.DrinkCard2);
        button3.clicked += ShowCard(GameManager.INSTANCE.DrinkCard3);

        button1.text = GameManager.INSTANCE.DrinkCard1.Sips.ToString();
        button2.text = GameManager.INSTANCE.DrinkCard2.Sips.ToString();
        button3.text = GameManager.INSTANCE.DrinkCard3.Sips.ToString();

        if (GameManager.INSTANCE.DrinkCard1.Categorie == Category.NiceVibes)
        {
            button1Category.text = "Nice Vibes";
        }
        else
        {
            button1Category.text = GameManager.INSTANCE.DrinkCard1.Categorie.ToString();
        }
        
        if (GameManager.INSTANCE.DrinkCard2.Categorie == Category.NiceVibes)
        {
            button2Category.text = "Nice Vibes";
        }
        else
        {
            button2Category.text = GameManager.INSTANCE.DrinkCard2.Categorie.ToString();
        }
        
        if (GameManager.INSTANCE.DrinkCard3.Categorie == Category.NiceVibes)
        {
            button3Category.text = "Nice Vibes";
        }
        else
        {
            button3Category.text = GameManager.INSTANCE.DrinkCard3.Categorie.ToString();
        }
        
        button1.style.backgroundImage = ChooseSprite(GameManager.INSTANCE.DrinkCard1.Categorie);
        button2.style.backgroundImage = ChooseSprite(GameManager.INSTANCE.DrinkCard2.Categorie);
        button3.style.backgroundImage = ChooseSprite(GameManager.INSTANCE.DrinkCard3.Categorie);
    }
}
