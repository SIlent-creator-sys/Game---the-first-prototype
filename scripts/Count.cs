using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Count : MonoBehaviour
{
    public movement InfoFromPlayerData;
    public GameObject info_panel;

    public Text Goal;
    int counter;
    void Start()
    {
      ShowInfo();
      ShowGoal();
    }
    public void ShowInfo()
    {

       counter =  int.Parse(gameObject.GetComponent<Text>().text);
       Debug.Log($"{counter}");
       if(InfoFromPlayerData.data.listOfPapers.Count > 0){
       info_panel.GetComponent<Text>().text = InfoFromPlayerData.data.listOfPapers[counter];
       Debug.Log("Added");
       }
       else
       {
        info_panel.GetComponent<Text>().text = "НЕТ НАГРАД";
       }
       
        
       Debug.Log("IsWorkingPanel?");
    }
    public void ShowGoal()
    {
        List<string> goals = InfoFromPlayerData.data.listOfGoals;
        Goal.text = goals[goals.Count-1];
    }
    
}
