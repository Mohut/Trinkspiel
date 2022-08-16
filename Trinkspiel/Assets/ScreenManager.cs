using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject nameSelectScreen;
    [SerializeField] private GameObject inGameScreen;

    private void OnEnable()
    {
        gameManager.startGameEvent += ShowInGameScreen;
    }

    private void OnDisable()
    {
        gameManager.startGameEvent += ShowInGameScreen;
    }

    public void ShowInGameScreen()
    {
        nameSelectScreen.SetActive(false);
        inGameScreen.SetActive(true);
    }
}
