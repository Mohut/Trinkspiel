using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class NameSelectScreen : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIDocument nameSelectScreen;
    private VisualElement scrollView;
    private TextField textField;
    private List<Button> nameButtonList = new List<Button>();
    private List<string> nameList;

    void Start()
    {
        nameSelectScreen.rootVisualElement.Q<Button>("Add").clicked += AddName;
        nameSelectScreen.rootVisualElement.Q<Button>("Start").clicked += gameManager.StartGame;
        scrollView = nameSelectScreen.rootVisualElement.Q("unity-content-container");
        textField = nameSelectScreen.rootVisualElement.Q<TextField>("Name");
    }

    private void OnEnable()
    {
        gameManager.startGameEvent += AddNamesToGameManager;
    }

    private void OnDisable()
    {
        gameManager.startGameEvent -= AddNamesToGameManager;
    }

    public void AddName()
    {
        Button newName = new Button();
        newName.text = textField.value;
        newName.clicked += delegate { DeleteName(newName); };
        scrollView.Add(newName);
    }

    public void DeleteName(Button button)
    {
        scrollView.Remove(button);
    }

    public void AddNamesToGameManager()
    {
        foreach (Button nameButton in nameButtonList)
        {
            nameList.Add(nameButton.text);
        }
        gameManager.Names = nameList;
    }
}
