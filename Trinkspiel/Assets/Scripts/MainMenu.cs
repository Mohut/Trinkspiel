using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument mainMenuDocument;
    private VisualElement mainMenuRoot;

    private void Start()
    {
        mainMenuRoot = mainMenuDocument.rootVisualElement;
        mainMenuRoot.Q<Button>("Play").clicked += StartGame;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
