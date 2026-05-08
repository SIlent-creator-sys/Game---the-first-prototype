
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Rayc : MonoBehaviour
{
      Collider currentObject;
      public GameObject text;
      Color currentColor;
      LayerMask Mask;

      public movement DataforObjects;

      public bool IsRotateGo = false;
      public GameObject door;
      public GameObject Tank;
      public GameObject textUlugbek;
      private bool IsDelay = true;
      void Start()
    {
        Mask = LayerMask.GetMask("Touchment");
    }
      void Update()
    {
        
        Ray ray = new Ray(transform.position , transform.forward);
        Debug.DrawRay(transform.position , transform.forward*7  , Color.yellow);
        RaycastHit hit;
        if(Physics.Raycast(ray , out hit , 7f , Mask ))
        {
            MeshRenderer rend = hit.collider.GetComponent<MeshRenderer>();
            if(rend){
            if(currentObject != hit.collider)
            {
                string tag = hit.collider.tag;
             
                if(currentObject)
                currentObject.GetComponent<MeshRenderer>().material.color = currentColor;
                if(tag !="TableAboutUlugbek")
                text.SetActive(true);

                if(tag == "DOOR")
                    {
                       text.GetComponent<ToRead>().Ruchka = hit.collider.gameObject;
                       text.GetComponent<ToRead>().Text = text;
                       DataforObjects.data.AddNameOfObjectToDict(hit.collider.name);


                    }
                // if(tag == "UNDOOR")
                //     {
                        
                //     }
                else if(tag == "KeyForGasRoom")
                   text.GetComponent<ToRead>().KeyForGasRoom = hit.collider.gameObject;

                else if(tag == "TableAboutUlugbek")
                    {
                        textUlugbek.SetActive(true);
                    }
                text.GetComponent<Text>().enabled = true;
                text.GetComponent<ToRead>().TAG = tag;
                
                
                
                currentColor = rend.material.color;
                rend.material.color = Color.yellow;
                currentObject = hit.collider;
            }

            }
        }
        else
        {
            if(currentObject && currentObject.GetComponent<MeshRenderer>()){
             
            text.SetActive(false);
            textUlugbek.SetActive(false);
                
             currentObject.GetComponent<MeshRenderer>().material.color = currentColor;
             currentObject = null;
            }

        }
        if (IsRotateGo)
        {
            if(door.name == "Dooor")
            {
                Tank.GetComponent<AudioSource>().enabled = true;
                text.GetComponent<ToRead>().Tank =  Tank.GetComponent<AudioSource>();
               

            }
            Quaternion target = Quaternion.Euler(0 , -90 , 0);
            door.transform.localRotation = Quaternion.RotateTowards(door.transform.localRotation , target , 35*Time.deltaTime);
            
            Debug.Log("Still Work!");
            if (IsDelay)
            {
                text.GetComponent<ToRead>().IsEventGo = false;
                door.GetComponent<AudioSource>().enabled = true;
                StartCoroutine(Delay());
                IsDelay = false;
            }
        }
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.5f);
        text.GetComponent<ToRead>().IsEventGo = true;
        IsRotateGo = false;
        IsDelay = true;
    }
    
}
