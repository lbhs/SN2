using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssignMedalScript : MonoBehaviour  //attached to the GameOverTextBox 
{
    public Sprite[] ArrayOfMedalsToDisplay;
    public Image MedalEarned;

    public int MedalIndexValue;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignMedal(int score)
    {
        MedalIndexValue = score / 75;
        if(MedalIndexValue > 4)
        {
            MedalIndexValue = 4;
        }

        MedalEarned.sprite = ArrayOfMedalsToDisplay[MedalIndexValue];   //Copper Medal = 0, Silver = 1 (>75 pts), Gold = 2 (>150 pts), Platinum = 4 (>225 pts)
        


    }
}
