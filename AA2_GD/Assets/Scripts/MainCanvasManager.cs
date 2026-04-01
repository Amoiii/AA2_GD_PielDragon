using Unity.Cinemachine;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    #region Variables
    
    public GameManager gameManager;

    public GameObject lorePanel;
    public GameObject inGamePanel;

    public CinemachineCamera mainVCam;
    public CinemachineCamera introVCam;
    public CinemachineCamera inGameVCam;
    
    public Animator mainMenuAnimator;

    #endregion

    public void StartGame()
    {
        lorePanel.SetActive(false);
        inGamePanel.SetActive(true);
        
        introVCam.Priority = -1;
        inGameVCam.Priority = 10;
        
        gameManager.InitializeGame();
    }
    
    #region Main Menu Methods

    public void InitGame()
    {
        mainMenuAnimator.SetTrigger("IsTriggered");
        mainVCam.Priority = -1;
        introVCam.Priority = 10;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

    #endregion
}
