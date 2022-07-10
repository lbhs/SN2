using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotationScript : MonoBehaviour  //attached to the LockRotationButton
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockRotation()  //this function toggles the rotation state of the ElectrophileMolecule
    {
        if (GameObject.FindGameObjectWithTag("ElectrophileMolecule").GetComponent<Rigidbody>().angularVelocity != Vector3.zero)
        {
            GameObject.FindGameObjectWithTag("ElectrophileMolecule").GetComponent<RotatingElectrophileScript>().StopRotation();

        }

        else  //on second click, rotation is resumed
        {
            GameObject.FindGameObjectWithTag("ElectrophileMolecule").GetComponent<RotatingElectrophileScript>().RestartRotation();
        }
            
    }  

    //public void TutorialLockRotation()  //this function is only active in tutorial scene
    //{
    //    //Two cases--Case one is early tutorial, where the user isn't trying to lock electrophile at a proper angle.  Case two = Game Simulation.

    //    if (GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<Rigidbody>().angularVelocity != Vector3.zero)
    //    {
    //        GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<TutorialElectrophileScript>().StopRotation();
    //        GameObject.Find("TutorialSpeechBubble").GetComponent<TutorialScript>().LockButtonHasBeenPressed = true;  //used as a prerequisite to advance tutorial
    //        GameObject.Find("TutorialSpeechBubble").GetComponent<TutorialScript>().ActivateAdvanceTutorialButton();
    //    }        

    //    else  //on second click, rotation is resumed
    //    {
    //        GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<TutorialElectrophileScript>().RestartRotation();
    //    }
    //}



}
