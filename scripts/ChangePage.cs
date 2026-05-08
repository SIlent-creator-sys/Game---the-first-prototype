using System;
using UnityEngine;
using UnityEngine.UI;

public class ChangePage : MonoBehaviour
{
    public GameObject count;
    public Count showinfo;
    public movement data;
  
    public void Increase()
    {
        Text text = count.GetComponent<Text>();
        int counter = int.Parse(text.text);
        if(counter < data.data.listOfPapers.Count-1)
        counter++;
        
        text.text = Convert.ToString(counter);
        showinfo.ShowInfo();
    }
    public void Decrease()
    {
        Text text = count.GetComponent<Text>();
        
        int counter = int.Parse(text.text);
        if(counter > 0)
        counter--;
       
        text.text = Convert.ToString(counter);
        showinfo.ShowInfo();
    }
}
