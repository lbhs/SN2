using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLockRotationScript : MonoBehaviour
{
    public GameObject TutorialSpeechBubble;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TutorialLockRotation()  //this function is only active in tutorial scene
    {
        //Two cases--Case one is early tutorial, where the user isn't trying to lock electrophile at a proper angle.  Case two = Game Simulation.
        if(TutorialSpeechBubble.GetComponent<TutorialScript>().MessageNumber < 18)
        {
            if (GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<Rigidbody>().angularVelocity != Vector3.zero)
            {
                GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<TutorialElectrophileScript>().StopRotationCaseOne();
                TutorialSpeechBubble.GetComponent<TutorialScript>().LockButtonHasBeenPressed = true;  //used as a prerequisite to advance tutorial
                TutorialSpeechBubble.GetComponent<TutorialScript>().ActivateAdvanceTutorialButton();
            }

            else  //on second click, rotation is resumed
            {
                GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<TutorialElectrophileScript>().RestartRotation();
            }
        }

        else  //if MessageNumber >17, case two applies--using normal RotationLock function plus messaging functions
        {
            if (GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<Rigidbody>().angularVelocity != Vector3.zero)
            {
                GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<TutorialElectrophileScript>().StopRotationCaseTwo();
                TutorialSpeechBubble.GetComponent<TutorialScript>().LockButtonHasBeenPressed = true;  //used as a prerequisite to advance tutorial
                TutorialSpeechBubble.GetComponent<TutorialScript>().ActivateAdvanceTutorialButton();
            }

            else  //on second click, rotation is resumed
            {
                GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<TutorialElectrophileScript>().RestartRotation();
            }
        }

       
    }
}
