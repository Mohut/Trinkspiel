using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class NameSelectScreen : MonoBehaviour
{
    [SerializeField] private UIDocument nameSelectScreen;
    [SerializeField] private StyleSheet styleSheet;
    private VisualElement scrollView;
    private TextField textField;
    private Button startButton;
    private List<Button> nameButtonList = new List<Button>();
    private List<string> nameList = new List<string>();

    void Start()
    {
        nameSelectScreen.rootVisualElement.Q<Button>("Add").clicked += AddName;
        scrollView = nameSelectScreen.rootVisualElement.Q("unity-content-container");
        textField = nameSelectScreen.rootVisualElement.Q<TextField>("Name");
        startButton = nameSelectScreen.rootVisualElement.Q<Button>("Start");
        startButton.clicked += StartGame;
        startButton.SetEnabled(false);
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
        AddNamesToGameManager();
        ChangeScene();
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
            startButton.SetEnabled(true);
        }
        else
        {
            startButton.SetEnabled(false);
        }
    }
}
