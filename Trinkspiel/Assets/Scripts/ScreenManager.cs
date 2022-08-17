using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    public void ShowInGameScreen()
    {
        SceneManager.LoadScene(1);
    }
}
