using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCanvasManager : MonoBehaviour
{
    #region Variables
    
    public GameManager gameManager;

    public GameObject lorePanel;
    public GameObject inGamePanel;
    public GameObject losePanel;

    public CinemachineCamera mainVCam;
    public CinemachineCamera introVCam;
    public CinemachineCamera inGameVCam;
    
    public Animator mainMenuAnimator;

    #endregion

    private void OnEnable()
    {
        PlayerHitbox.OnPlayerDeath += ShowLosePanel;
    }

    private void OnDisable()
    {
        PlayerHitbox.OnPlayerDeath -= ShowLosePanel;
    }

    public void StartGame()
    {
        lorePanel.SetActive(false);
        inGamePanel.SetActive(true);
        
        introVCam.Priority = -1;
        inGameVCam.Priority = 10;
        
        gameManager.InitializeGame(); }
    
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

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
        inGamePanel.SetActive(false);
    }

    public void RetryGameButton()
    {
        SceneManager.LoadScene(0);
    }
}
