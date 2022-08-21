using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class NameSelectScreen : MonoBehaviour
{
    [SerializeField] private DrinkCardLists drinkCardLists;
    [SerializeField] private UIDocument nameSelectScreen;
    [SerializeField] private StyleSheet styleSheet;
    [SerializeField] private UIDocument deckSelectScreen;
    private VisualElement scrollView;
    private TextField textField;
    private Button continueButton;
    private Button startButton;
    private List<Button> nameButtonList = new List<Button>();
    private List<string> nameList = new List<string>();
    private Button standardButton;
    private Button movementButton;
    private Button hotButton;
    private Button niceVibesButton;
    private Button childishButton;

    void Start()
    {
        nameSelectScreen.rootVisualElement.Q<Button>("Add").clicked += AddName;
        scrollView = nameSelectScreen.rootVisualElement.Q("unity-content-container");
        textField = nameSelectScreen.rootVisualElement.Q<TextField>("Name");
        continueButton = nameSelectScreen.rootVisualElement.Q<Button>("Start");
        startButton = deckSelectScreen.rootVisualElement.Q<Button>("Start");
        startButton.clicked += StartGame;
        continueButton.clicked += ShowDeckSelectScreen;
        continueButton.SetEnabled(false);
        deckSelectScreen.rootVisualElement.style.display = DisplayStyle.None;
        
        standardButton = deckSelectScreen.rootVisualElement.Q<Button>("Standard");
        movementButton = deckSelectScreen.rootVisualElement.Q<Button>("Movement");
        hotButton = deckSelectScreen.rootVisualElement.Q<Button>("Hot");
        niceVibesButton = deckSelectScreen.rootVisualElement.Q<Button>("NiceVibes");
        childishButton = deckSelectScreen.rootVisualElement.Q<Button>("Childish");
        
        standardButton.clicked += GameManager.INSTANCE.EnableDisableDecks(1);
        movementButton.clicked += GameManager.INSTANCE.EnableDisableDecks(2);
        hotButton.clicked += GameManager.INSTANCE.EnableDisableDecks(3);
        niceVibesButton.clicked += GameManager.INSTANCE.EnableDisableDecks(4);
        childishButton.clicked += GameManager.INSTANCE.EnableDisableDecks(5);
        
        standardButton.clicked += ChangeButton(standardButton);
        movementButton.clicked += ChangeButton(movementButton);
        hotButton.clicked += ChangeButton(hotButton);
        niceVibesButton.clicked += ChangeButton(niceVibesButton);
        childishButton.clicked += ChangeButton(childishButton);
    }

    public void AddName()
    {
        Button newName = new Button();
        if (textField.value.Length >= 11)
        {
            textField.value = textField.value.Substring(0, 10);
        }
        newName.text = textField.value;
        newName.styleSheets.Add(styleSheet);
        newName.clicked += delegate { DeleteName(newName); };
        scrollView.Add(newName);
        nameButtonList.Add(newName);
        textField.value = "";
        CheckIfStartPossible();
    }

    public void DeleteName(Button button)
    {
        scrollView.Remove(button);
        nameButtonList.Remove(button);
        CheckIfStartPossible();
    }

    public void StartGame()
    {
        if (!drinkCardLists.CheckForDecks())
            return;
        
        GameManager.INSTANCE.SummarizeCards();
        AddNamesToGameManager();
        ChangeScene();
    }

    public void ShowDeckSelectScreen()
    {
        nameSelectScreen.rootVisualElement.style.display = DisplayStyle.None;
        deckSelectScreen.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void AddNamesToGameManager()
    {
        foreach (Button nameButton in nameButtonList)
        {
            nameList.Add(nameButton.text);
        }
        GameManager.INSTANCE.Names = nameList;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void CheckIfStartPossible()
    {
        if (nameButtonList.Count >= 2)
        {
            continueButton.SetEnabled(true);
        }
        else
        {
            continueButton.SetEnabled(false);
        }
    }

    public Action ChangeButton(Button button)
    {
        return delegate { ChangeButtonColor(button); CheckIfDeckIsActive(); };
    }

    public void ChangeButtonColor(Button button)
    {
        if (button.style.backgroundColor == Color.red)
        {
            button.style.backgroundColor = Color.green;
        }
        else
        {
            button.style.backgroundColor = Color.red;
        }
    }

    public void CheckIfDeckIsActive()
    {
        if (!drinkCardLists.CheckForDecks())
        {
            startButton.SetEnabled(false);
        }
        else
        {
            startButton.SetEnabled(true);
        }
    }
}
