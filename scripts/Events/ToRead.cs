using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;
using Newtonsoft.Json;

public class ToRead : MonoBehaviour
{
    public bool IsEventGo = true;
    public AudioClip clip;
    public AudioSource source;
    public GameObject Player;
    public GameObject Paper;
    private movement scriptMovement;
    public GameObject obj;

// DOOR
    public GameObject Ruchka;
    public GameObject Text;
    public AudioSource SoundLockedDoor;
    

//HASH
    
    public string TAG;
    private List<string> hash = new List<string>();
    string GetText;
    public GameObject ReadableText;
//For GasRoom
    public GameObject DoorOfGasRoom;
    public GameObject KeyForGasRoom;

//For Ulugbek Story
   public GameObject UlugbekPaper;
   public GameObject DoorOfUlugbek;
   public VideoPlayer videoOfulugbek;
   public GameObject TextureOfVideo;
   public GameObject NewDoorForNewScene;
   public AudioSource Tank;
    
    bool AfterEvent = false;
    void Start()
    {
        scriptMovement = Player.GetComponent<movement>();
        
        source.clip = clip;
       

        
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && hash.Find(x => x == TAG)?.Length == null && IsEventGo){

            if(TAG == "Paper")
            {
                DisablingMovement();
                ReadPaper(true , ref ReadableText);
                AfterEvent = true;
            }
            else if(TAG == "DOOR")
            {
                Ruchka.GetComponent<DoorAction>().OpenDoor();
                scriptMovement.data.ChangeObjectToTrue(Ruchka.name);
                Destroy(Ruchka);
                Text.SetActive(false);
            }
            else if(TAG == "UNDOOR")
            {
                SoundLockedDoor.Play();
            }
            else if(TAG == "KeyForGasRoom")
            {
                DoorOfGasRoom.tag = "DOOR";
                source.Play();
                Destroy(KeyForGasRoom);
                Text.SetActive(false);
            }
            else if(TAG == "PaperAboutUlugbek")
            {
                DisablingMovement();
                ReadPaper(true , ref UlugbekPaper);
                AfterEvent = true;
            }
              else if(TAG == "Watchable")
            {
                DisablingMovement();
                Tank.Stop();
                TextureOfVideo.SetActive(true);
                videoOfulugbek.enabled = true;
                StartCoroutine(Delay());
                
            }
        }

        if (Input.GetKeyDown(KeyCode.G) && AfterEvent)
        {
            if(TAG == "Paper")
            {
                ReadPaper(false , ref ReadableText);
                scriptMovement.data.listOfPapers.Add(GetText);
                scriptMovement.data.listOfGoals.Add("Найти дверь в морг");
                Debug.Log("Now" + JsonConvert.SerializeObject(scriptMovement.data));
                hash.Add(TAG);
                source.Play();
                AfterEvent = false;
                Paper.SetActive(false);
                obj.GetComponent<gates>().enabled = true;
                obj.GetComponent<AudioSource>().enabled = true;
                 EnablingMovement();
               
            }
            else if(TAG == "PaperAboutUlugbek")
            {

                hash.Add(TAG);
                source.Play();
                DoorOfUlugbek.tag = "DOOR";
                ReadPaper(false , ref UlugbekPaper);
                AfterEvent = false;
                scriptMovement.data.listOfPapers.Add(GetText);
                 scriptMovement.data.listOfGoals.Add("Зайти в комнату Улугбека и найти кое-что интересное...");
              
                Debug.Log("Now" + JsonConvert.SerializeObject(scriptMovement.data));
               
                EnablingMovement();
            }
          
        }
        
        
    }

   

    
    void ReadPaper(bool function , ref GameObject Readable)
    {
        if(function)
         Readable.SetActive(true);
         
        else{
         Readable.SetActive(false);
         GetText = Readable.GetComponentInChildren<Text>().text;
        }
    }
    void DisablingMovement()
    {
        scriptMovement.enabled = false;
    }
    void EnablingMovement()
    {
            scriptMovement.enabled = true;
    }

    System.Collections.IEnumerator Delay()
    {
        yield return new WaitForSeconds(35);
        videoOfulugbek.enabled = false;
        TextureOfVideo.SetActive(false);
        EnablingMovement();
        source.Play();
        NewDoorForNewScene.tag = "DOOR";
        
    }

    

}
