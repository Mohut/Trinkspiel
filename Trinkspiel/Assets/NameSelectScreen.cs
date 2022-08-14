using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;

public class NameSelectScreen : MonoBehaviour
{
    [SerializeField] private UIDocument nameSelectScreen;
    private VisualElement scrollView;
    private TextField textField;
    private List<Button> nameButtonList = new List<Button>();
    private List<string> nameList;
    private Event Action showCard;

    void Start()
    {
        nameSelectScreen.rootVisualElement.Q<Button>("Add").clicked += AddName;
        nameSelectScreen.rootVisualElement.Q<Button>("Start").clicked += StartGame;
        scrollView = nameSelectScreen.rootVisualElement.Q("unity-content-container");
        textField = nameSelectScreen.rootVisualElement.Q<TextField>("Name");
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

    public void StartGame()
    {
        foreach (Button nameButton in nameButtonList)
        {
            nameList.Add(nameButton.text);
        }
        
        GameManager.INSTANCE.SetNames(nameList);
        GameManager.INSTANCE.StartGame();
    }
}
