using UnityEngine;

public class InsideMenu : MonoBehaviour
{
    public GameObject PanelForSettings;
   
    public void Exit()
    {
        gameObject.SetActive(false);
        
        PanelForSettings.SetActive(true);
        
    }
}
