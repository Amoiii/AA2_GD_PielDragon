using UnityEngine;

public class MainCanvasManager : MonoBehaviour
{
    #region Variables

    public Animator mainMenuAnimator;

    #endregion
    

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
