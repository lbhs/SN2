using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoringScript : MonoBehaviour
{
    public TMP_Text ScoreText;
    public static int Score;   //this variable is altered in the InvertCarbonScript upon successful reaction
       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text = Score.ToString() + " pts";   
    }

    public void GameOver()  //handled in CountdownTimerScript
    {
        //GameObject.Find("GameOverTextBox").GetComponent<AssignMedalScript>().AssignMedal(Score);
    }

    

}
