using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    #region Variables

    public GameObject lorePanel;
    public GameObject inGamePanel;

    public Animator mainMenuAnimator;

    #endregion

    public void StartGame()
    {
        lorePanel.SetActive(false);
        inGamePanel.SetActive(true);
        // Logica de inicial del juego
    }
    
    #region Main Menu Methods

    public void InitGame()
    {
        mainMenuAnimator.SetTrigger("IsTriggered");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
