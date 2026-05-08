


using System;
using System.Collections;
using NUnit.Framework;
using Unity.Multiplayer.PlayMode;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using System.Text.Json;
using System.IO;
using static Save.Ssytemic.SaveSystem;

using UnityEditor;
using Newtonsoft.Json;
using System.Collections.Generic;
using JetBrains.Annotations;




public class PlayerData
{
    public float x =0;
    public float y = 3;
    public float z = 0;
    public System.Collections.Generic.List<string> listOfPapers = new System.Collections.Generic.List<string>();
    public System.Collections.Generic.List<string> listOfGoals = new System.Collections.Generic.List<string>();

    public int TheNumberOfScene = 0;

    public Dictionary<string , bool> dataOfobjects = new Dictionary<string, bool>();

    public float[] position = new float[3];

    public void AddNameOfObjectToDict(string name)
    {
        if(dataOfobjects.TryGetValue(name, out bool result))
        Debug.Log("Exists");
        else
        dataOfobjects.Add(name, false);
    }
    public void ChangeObjectToTrue(string name)
    {
        if(dataOfobjects.TryGetValue(name, out bool result))
        {
            if (!result)
            {
                dataOfobjects[name] = true;
            }
        }
    }
    public void ChekingDataOfObjects()
    {
        
        foreach( var pair in dataOfobjects)
        {
            GameObject obj = GameObject.Find(pair.Key);
            Debug.Log("Work! dataCheck");
            if(pair.Value){
            obj.GetComponent<DoorAction>().OpenDoorByData();
            MonoBehaviour.Destroy(obj);
            }

        }
        
    }
    public  void SetPositionForPlayer(float x , float y  , float z , ref Vector3 playerPosition)
    {
        playerPosition.x = x;
        playerPosition.y = y;
        playerPosition.z = z;
    }
    public void Packaging()
    {
         position[0] = x;
         position[1] = y;
         position[2] = z;
    }
    public void SetValues(float x , float y  , float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}


public class movement : MonoBehaviour
{
    public PlayerData data;
    public CharacterController controller;
    public Camera playerCamera;


    
    public float walkSpeed = 10f;
    public float sprintSpeed = 12f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float mouseSensitivity = 2f;
    Quaternion angle;
    Quaternion track = Quaternion.Euler(0 , 0, 90);

    bool IsSound = false;
    public AudioClip clips;
    public AudioSource source;
    
    private UnityEngine.Vector3 velocity = UnityEngine.Vector3.zero;
    private float xRotation = 0f;
    public GameObject panel;
    private bool IsBackUp = true;
    Vector3 startpost;

    

    
    
    void Start() {
         data = new PlayerData();
         StartJson();
        if(File.Exists(path)){
          
            SetupPositionForPLayer(true);
            data.ChekingDataOfObjects();
            Debug.Log(File.ReadAllText(path));
            Debug.Log("Now" + JsonConvert.SerializeObject(data));
            Debug.Log( $"Position {gameObject.transform.position}");

        }
        // Check controller
        StartCoroutine(Delay());
        angle = playerCamera.transform.localRotation;
       
        source = GetComponent<AudioSource>();
        source.clip = clips;
      
        if (controller == null) {
            Debug.LogError("ASSIGN CHARACTER CONTROLLER IN INSPECTOR! XD");
            enabled = false;
            return;
        }
        startpost = playerCamera.transform.localPosition;
        
        Debug.Log("FPS Controller");
        
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
         
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(panel);

    }

    void SetupPositionForPLayer(bool IsWorking)
    {
        if(IsWorking){
            UnPacking(ref data);
            data.SetValues(data.position[0], data.position[1], data.position[2]);
            Vector3 record = Vector3.zero;
            data.SetPositionForPlayer( data.x, data.y , data.z,  ref record);
            gameObject.transform.position = record;
        }
    }
    
    void Update() {
        // Setting Position for player
        if(IsBackUp){
        StartCoroutine(DelayOfBackUp());
        
        IsBackUp = false;
        }
        // ===== MOVEMENT =====
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
       
        
        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && controller.isGrounded) ? sprintSpeed : walkSpeed;
        UnityEngine.Vector3 move = (transform.right * x + transform.forward * z) * currentSpeed;
        SaveDataOfPlayer(data);
        // ===== GRAVITY & JUMPING =====
        Gravity(currentSpeed , x ,z);
        // ===== APPLY MOVEMENT =====
        ApplyMovement(move);
       //Disabling
        DisablingController();
        DisablingPlayer();
        // ===== MOUSE LOOK =====
        MouseLook();


      
    }
    IEnumerator DelayOfBackUp()
    {
        yield return new WaitForSeconds(1);
        data.SetValues(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        data.Packaging();
        foreach(var pair in data.dataOfobjects)
        {
             Debug.Log($"Key {pair.Key} and value {pair.Value}");
        }
        
      
        SaveDataOfPlayer(data);
        IsBackUp = true;

    }
    void RealPhysicMovement(float currentSpeed , float x , float z)
    {
            float number;
            if(currentSpeed == sprintSpeed)
            {
                number = Mathf.Sin(Time.time*18f)*0.1f;
            }
            else
            {
                number =  Mathf.Sin(Time.time*12f)*0.1f;
            }
           
            if(Mathf.Abs(x)>0|| Mathf.Abs(z)>0){
                
            playerCamera.transform.localPosition = startpost + new Vector3(0 , number , 0);
                    if(currentSpeed == sprintSpeed && !IsSound){
                    source.loop = true;
                    source.Play();
                    IsSound = true;
                    }
                    else if(currentSpeed == walkSpeed)
                    {
                        source.Stop();
                        IsSound = false;
                    }

            }
            else
            {
                source.Stop();
                IsSound = false;
            }
        } 

    void Gravity(float currentSpeed , float x , float z)
    {
         if (controller.isGrounded && controller.enabled) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            
            RealPhysicMovement(currentSpeed , x, z);
        } 
        else {
            source.Stop();
            IsSound = false;
            velocity.y += gravity * Time.deltaTime;
        }
    }

    void ApplyMovement(Vector3 move)
    {
        move.y = velocity.y;
        if(controller.enabled)
        controller.Move(move * Time.deltaTime);
    }

    void DisablingController()
    {
         if (Input.GetKey(KeyCode.H))
        {
            controller.enabled = false;
             Debug.Log("ON!" + controller.enabled);
            Rigidbody rb = GetComponent<Rigidbody>();
            if(rb == null)
            {
                rb = gameObject.AddComponent<Rigidbody>();
            }
        }
    }
    void DisablingPlayer()
    {
        if (!controller.enabled)
        {
            playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition , Vector3.zero , 4*Time.deltaTime);
            if(playerCamera.transform.localEulerAngles.z <=90){
            playerCamera.transform.Rotate(new Vector3(0 , 0 ,1)*85f*Time.deltaTime);
            }
        }
    }
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        if(controller.enabled){
        transform.Rotate(UnityEngine.Vector3.up * mouseX);
        playerCamera.transform.localRotation = UnityEngine.Quaternion.Euler(xRotation, 0f, 0f);
        }
    }
    
   
}
