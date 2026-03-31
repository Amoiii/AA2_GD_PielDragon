using UnityEngine;

public class MenuAnimatorController : MonoBehaviour
{
    public GameObject lorePanel;
    
    public void TriggerInGame()
    {
        gameObject.SetActive(false);
        lorePanel.SetActive(true);
    }
}
