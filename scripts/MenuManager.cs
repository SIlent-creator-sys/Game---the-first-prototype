using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private movement playerMovement;
    public GameObject PanelForSettings;
    public GameObject Player;
    public Text Selectable;

    bool PushedButton = false;
    void Start()
    {
        playerMovement = Player.GetComponent<movement>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !PushedButton)
        {
            PushedButton = true;
            SwitchingPlayer(false);
        }
        if(Input.GetKeyDown(KeyCode.L) && PushedButton)
        {
             PushedButton = false;
             SwitchingPlayer(true);
        }
        
    }
    void SwitchingPlayer(bool IsTrue)
    {
        playerMovement.enabled = IsTrue;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = !IsTrue;
        PanelForSettings.SetActive(!IsTrue);

    }
}
