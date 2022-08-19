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
    private List<Button> nameButtonList = new List<Button>();
    private List<string> nameList = new List<string>();

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
        newName.styleSheets.Add(styleSheet);
        newName.clicked += delegate { DeleteName(newName); };
        scrollView.Add(newName);
        nameButtonList.Add(newName);
        textField.value = "";
    }

    public void DeleteName(Button button)
    {
        scrollView.Remove(button);
        nameButtonList.Remove(button);
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
}
