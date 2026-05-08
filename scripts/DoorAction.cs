using System.Linq.Expressions;
using UnityEngine;

public class DoorAction : MonoBehaviour
{
    public Rayc SwitchRotate;
    
    
    public void OpenDoor()
    {
        Ray ray = new Ray(transform.position , transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray , out hit , 10))
        {
            GameObject door = hit.collider.gameObject;

            
            SwitchRotate.door = door;
            SwitchRotate.IsRotateGo = true;

        }
    } 
    public void OpenDoorByData()
    {
         Ray ray = new Ray(transform.position , transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray , out hit , 10))
        {
            GameObject door = hit.collider.gameObject;
            door.transform.localRotation = Quaternion.Euler(0, -90 ,0);
        }
    }
}
