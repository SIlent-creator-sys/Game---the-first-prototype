using UnityEngine;

using System.IO;

using Newtonsoft.Json;


namespace Save.Ssytemic{
public static class SaveSystem 
{
    public static string path;
    
   
   public static void StartJson()
    {
        path = Path.Combine(Application.persistentDataPath, "save.json");
        Debug.Log("Differnce" + path);
       
    }
   public  static void SaveDataOfPlayer(PlayerData data)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(path, json);
       

    }
    public static void UnPacking(ref PlayerData data)
        {
            string json = File.ReadAllText(path);
           
            data = JsonConvert.DeserializeObject<PlayerData>(json);
            Debug.Log("Current Des" + JsonConvert.SerializeObject(data));
        }
  
}
}
