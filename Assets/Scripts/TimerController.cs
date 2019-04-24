using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    //for the time for the level
    public static float timer;

    //timer text
    public Text timerText;
    public static float counter;

    // Start is called before the first frame update
    void Start()
    {
        timer = 15;
        counter = 15;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        counter -= Time.deltaTime;

        timerText.text = counter.ToString();

        if(timer <= 0)
        {
            SceneManager.LoadScene("Game Over!", LoadSceneMode.Single);
        }
      
    }
}
