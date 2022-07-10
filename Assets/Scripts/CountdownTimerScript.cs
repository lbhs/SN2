using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownTimerScript : MonoBehaviour  //Attached to CountdownTimerImage 
{
    private int CountdownTime;
    private int CyclesPerSecond;
    public TMP_Text CountdownTimerDisplay;
    public AudioSource TimesUp;
    public TMP_Text GameOverText;
    public GameObject GameOverTextBox;  //this is the actual gameObject, not the TMP_Text component
    public GameObject ButtonToLoadGameOverScene;
    public GameObject MoleculeInstantiationManager;
    public GameObject Cube1;
    public GameObject Cube2;

    public int TimerStartValue;
    public bool StartTimer;

    // Start is called before the first frame update
    void Start()
    {
        CountdownTime = TimerStartValue;
        MoleculeInstantiationManager = GameObject.Find("MoleculeInstantiationManager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (StartTimer)  //in Tutorial Scene, timer won't start until the user has been informed of its purpose
        {
            
            CyclesPerSecond++;
            if (CyclesPerSecond > 99)
            {
                CyclesPerSecond = 0;
                CountdownTime--;

                CountdownTimerDisplay.text = CountdownTime.ToString();

                if (CountdownTime < 1)  //This means the end of the game!
                {
                    Time.timeScale = 0;  //this should freeze the game
                    TimesUp.Play();
                    GameOverText.text = "Game Over";
                    ButtonToLoadGameOverScene.SetActive(true);
                    if(Cube1 != null)
                    {
                        Cube1.SetActive(false);
                    }
                    if (Cube2 != null)
                    {
                        Cube2.SetActive(false);
                    }

                    if (MoleculeInstantiationManager.GetComponent<GlobalVariableScript>().DifficultyLevel == 1) //means this is the easy game
                    {
                        GameOverTextBox.GetComponent<AssignMedalScript>().AssignMedal(ScoringScript.Score);
                    } 

                    


                    //GameObject.Find("Score").GetComponent<ScoringScript>().GameOver();

                }
            }
        }
        

        
    }
}
