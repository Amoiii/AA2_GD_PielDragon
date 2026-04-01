using Unity.Cinemachine;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    #region Variables

    public GameObject lorePanel;
    public GameObject inGamePanel;

    public CinemachineCamera mainVCam;
    public CinemachineCamera introVCam;
    
    public Animator mainMenuAnimator;

    #endregion

    public void StartGame()
    {
        lorePanel.SetActive(false);
        inGamePanel.SetActive(true);
        
        introVCam.Priority = -1;
        mainVCam.Priority = 10;
        // Logica de inicial del juego
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
