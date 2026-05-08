using Unity.VisualScripting;
using UnityEngine;

public class Menu : MonoBehaviour
{
    
    public GameObject panelMenu;
     public Count ToCount;
   

    public void UnlockMenuPanel()
    {
        panelMenu.SetActive(true);
        ToCount.ShowInfo();
        ToCount.ShowGoal();

        gameObject.SetActive(false);
    }
}
