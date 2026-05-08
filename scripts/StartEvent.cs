using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartEvent : MonoBehaviour
{
    public GameObject anim;
    public AudioSource backgroundMusic;
    public void StartGame()
    {
        backgroundMusic.Stop();
        GetComponent<AudioSource>().Play();
        anim.SetActive(true);
        anim.GetComponent<Animator>().enabled = true;
        StartCoroutine(Delay());
        
    }
    public void Leave()
    {
        Application.Quit();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        
        SceneManager.LoadScene(1);
    }
}
