using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument mainMenuDocument;
    [SerializeField] private UIDocument tutorialDocument;
    private VisualElement mainMenuRoot;
    private VisualElement tutorialRoot;
    private void Start()
    {
        mainMenuRoot = mainMenuDocument.rootVisualElement;
        tutorialRoot = tutorialDocument.rootVisualElement;
        
        mainMenuRoot.Q<Button>("Play").clicked += StartGame;
        mainMenuRoot.Q<Button>("Help").clicked += openTutorial;
        tutorialRoot.Q<Button>("BackButton").clicked += openMainMenu;
        
        tutorialRoot.style.display = DisplayStyle.None;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void openTutorial()
    {
        mainMenuRoot.style.display = DisplayStyle.None;
        tutorialRoot.style.display = DisplayStyle.Flex;
    }

    public void openMainMenu()
    {
        tutorialRoot.style.display = DisplayStyle.None;
        mainMenuRoot.style.display = DisplayStyle.Flex;
    }
}
