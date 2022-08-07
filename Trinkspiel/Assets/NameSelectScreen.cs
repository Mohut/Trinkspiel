using UnityEngine;
using UnityEngine.UIElements;

public class NameSelectScreen : MonoBehaviour
{
    [SerializeField] private UIDocument nameSelectScreen;
    private VisualElement scrollView;
    void Start()
    {
        nameSelectScreen.rootVisualElement.Q<Button>("Add").clicked += AddName;
        nameSelectScreen.rootVisualElement.Q<Button>("Start").clicked += StartGame;
        scrollView = nameSelectScreen.rootVisualElement.Q("unity-content-container");
    }

    public void AddName()
    {
        scrollView.Add(new TextField("yes"));
    }

    public void StartGame()
    {
        
    }
}
