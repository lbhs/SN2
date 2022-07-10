using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergizeAndFireNucleophileScript : MonoBehaviour  //attached to the Up Arrow Button in scene
{
    private bool EnergizeButtonIsPressed;

    public Image EnergyLevelBar;

    public AudioSource EnergizeSound1;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (EnergizeButtonIsPressed)
        //{
        //    //needed to move this function to Update for it to repeatedly perform the EnergizeNucleophile function that adds velocity to the Nucleophile
        //    if (GameObject.FindGameObjectWithTag("Nucleophile"))
        //    {
        //        GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().EnergizeNucleophile();
        //    }          
        //}

        if (GameObject.FindGameObjectWithTag("Nucleophile"))
        {
            EnergyLevelBar.fillAmount = GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().ChlorideY_Velocity / 10;
        }    
               
    }

    private void FixedUpdate()
    {
        if (EnergizeButtonIsPressed)
        {
            //needed to move this function to Update for it to repeatedly perform the EnergizeNucleophile function that adds velocity to the Nucleophile
            if (GameObject.FindGameObjectWithTag("Nucleophile"))
            {
                if (!GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().NucleophileInFlight)
                {
                    GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().EnergizeNucleophile();
                }                
            }
        }
    }

    public void EnergizeButtonPressed()  //Add energy to nucleophile while button is pressed
    {
        if (gameObject.GetComponent<Button>().interactable)  //need to check that the Button is interactable, otherwise can power up nucleophile at any time
        {
            EnergizeButtonIsPressed = true;

            if (GameObject.FindGameObjectWithTag("Nucleophile"))
            {
                if (!GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().NucleophileInFlight)
                {
                    GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().EnergizeNucleophile();
                }
            }
        }
        
    }

    public void EnergizeButtonReleased()  //Nucleophile is fired when button is released
    {
        EnergizeButtonIsPressed = false;
        if (GameObject.FindGameObjectWithTag("Nucleophile"))
        {
            GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().FireNucleophile();
        }
        
        
        if (GameObject.Find("TutorialArrow"))
        {
            GameObject.Find("TutorialArrow").transform.position = new Vector2(200, 0);
            GameObject.Find("TutorialSpeechBubble").GetComponent<TutorialScript>().AdvanceTutorialButton.interactable = true;
        }
            
    }



}
