using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergizeSoundScript : MonoBehaviour
{
    public GameObject EnergizeSoundMonoFrequency;
    public AudioSource EnergizeSound1;
    public AudioSource EnergizeSound2_variablePitch;
    public GameObject EnergizeSoundGameObject;

    public bool EnergizeSoundIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Nucleophile"))  //when nucleophile has been fired and is regenerating, there is no Nucleophile in scene
        {
            //ChlorideY_Velocity starts at 4 and goes to 10
            EnergizeSound2_variablePitch.pitch = 0.2f + GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().ChlorideY_Velocity / 40;
        }
        
        

    }

    public void PlayTheEnergizeSoundFX()  //this function is called only from ChlorideMovementControlScript
    {
        //EnergizeSound1.Play();  //only used if two sounds will be superimposed
        //EnergizeSoundGameObject.SetActive(true);  //

        EnergizeSound2_variablePitch.Play();
        //print(EnergizeSound2_variablePitch.pitch);
        EnergizeSoundIsPlaying = true;

    }

    public void StopEnergizeSounds()
    {
        EnergizeSound2_variablePitch.Stop();  //need to stop the first sound
        EnergizeSoundIsPlaying = false;
        //EnergizeSoundGameObject.SetActive(false);  //this will allow the sound to start again (plays sound when gameObject is SetActive
    }
}
