using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChlorideMovementControlScript : MonoBehaviour  //Attached to the Chloride Nucleophile GameObject
{
    public float ChlorideY_Velocity;  //used to launch the nucleophile at its target
    public bool NucleophileInFlight;  //Disable left and right commands if Nucleophile has already been launched
    public Vector2 ForceApplied;

    public GameObject MoleculeInstantiationManager;

    public AudioSource DeniedSound;
    public AudioSource FireNucleophileSound;
    //public AudioSource MovementSound;

    public Button EnergizeButton;  //set to interactable if rotation has been frozen at a proper angle (RotatingElectrophileScript).  Also set to true in advanced game with no rotation lock
    //public GameObject EnergizeSoundGameObject;



    

    // Start is called before the first frame update
    void Start()
    {
        MoleculeInstantiationManager = GameObject.Find("MoleculeInstantiationManager");
        DeniedSound = GameObject.Find("DeniedSound").GetComponent<AudioSource>();
        FireNucleophileSound = GameObject.Find("FireSound").GetComponent<AudioSource>();
        //MovementSound = GameObject.Find("MoveNucleophileSound").GetComponent<AudioSource>();

        if (GameObject.Find("EnergizeButton"))  //tutorial scene may not have an active EnergizeButton 
        {
            EnergizeButton = GameObject.Find("EnergizeButton").GetComponent<Button>();
        }       

        if (GameObject.Find("TutorialSpeechBubble"))  
        {
            if (GameObject.Find("TutorialSpeechBubble").GetComponent<TutorialScript>().MessageNumber < 4)
            {
                EnergizeButton.gameObject.SetActive(false);  //the energize button starts hidden in the Tutorial Scene
            }
                
        }

        ChlorideY_Velocity = 4;
        //EnergizeSound = GameObject.Find("EnergizeSoundBuzz").GetComponent<AudioSource>();  
        //EnergizeSoundGameObject = MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>().EnergizeSoundMonoFrequency;  //this allows a reference to the sound holder GameObject using a permanent GameObject in scene
    }

    // Update is called once per frame
    void Update()
    {
        if (!NucleophileInFlight)  //controls only work if nucleophile has not yet been launched
        {
            if (Input.GetKey("left"))
            {
                MoveLeft();               
            }

            else if (Input.GetKey("right"))
            {
                MoveRight();
                
            }

            else if (Input.GetKeyUp("right") || Input.GetKeyUp("left"))
            {
                StopMovementOfNucleophile();
                
            }


            //if (Input.GetKey("up"))  MOVED TO FIXED UPDATE!!!!
            //{
            //    //CHECK HERE TO SEE WHETHER ENERGIZING IS ALLOWED--will be disallowed if rotation is frozen at a bad angle 
            //    if (EnergizeButton != null && EnergizeButton.interactable)
            //    {
            //        EnergizeNucleophile();
            //    }

            //    else
            //    {
            //        //print("can't energize bc angle is bad");
            //    }

            //    //Sounds are handled by MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>
                

            //}

                       
            if (Input.GetKeyUp("up"))  //&& NucleophileInFlight == false)   //GetKeyUp is triggered when button is released
            {
                if (EnergizeButton != null && EnergizeButton.interactable)
                {
                    FireNucleophile();
                }
            }
        }
        
    }



    public void MoveLeft()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(-3, 0);
    }

    public void MoveRight()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(3, 0);
    }

    public void StopMovementOfNucleophile()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(0, 0);
    }

    public void EnergizeNucleophile()
    {
        if (MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>().EnergizeSoundIsPlaying == false)
        {
            MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>().PlayTheEnergizeSoundFX();
        }


        if (ChlorideY_Velocity < 10)
        {
            ChlorideY_Velocity += 0.08f;
            //print(ChlorideY_Velocity);
        }
        
       


    }

    public void FireNucleophile()
    {
        if (!NucleophileInFlight)  //if nucleophile is already in flight, don't want to fire it again!
        {
            MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>().StopEnergizeSounds();
            FireNucleophileSound.Play();
            NucleophileInFlight = true;   //this bool disables movement left and right until a new nucleophile is instantiated
            //EnergizeButton.interactable = false;

            if (GameObject.FindGameObjectWithTag("ElectrophileMolecule"))  //in Tutorial scene, the electrophile may not yet be active
            {
                GameObject.FindGameObjectWithTag("ElectrophileMolecule").GetComponent<InvertCarbonScript>().NucleophileEnergy = ChlorideY_Velocity;
            }

            gameObject.GetComponent<Rigidbody>().velocity = new Vector2(0, ChlorideY_Velocity);
            //ForceApplied = new Vector2(0, ChlorideY_Velocity);
            ////print("force applied " + ForceApplied);
            //GetComponent<Rigidbody>().AddForce(ForceApplied, ForceMode.VelocityChange);

            if (GameObject.Find("TutorialArrow"))
            {
                GameObject.Find("TutorialArrow").transform.position = new Vector2(200, 0);
            }
        }
        
    }



    private void FixedUpdate()  //Make a New Nucleophile if the original goes off screen
    {
        //print("nucleophile velocity = " + GetComponent<Rigidbody>().velocity);

        if(transform.position.y > 8)  //this means that the nucleophile missed the target and went off the top of the screen!
        {
            //print("chloride y-pos = " + transform.position.y);
            gameObject.tag = "Inactive";
            StartCoroutine(CountdownToDestruction(0.3f));

            if (GameObject.FindGameObjectWithTag("ElectrophileMolecule"))
            {
                GameObject.FindGameObjectWithTag("ElectrophileMolecule").GetComponent<RotatingElectrophileScript>().RestartRotation();
            }
            //else if (GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule"))
            //{
            //    GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule").GetComponent<TutorialElectrophileScript>().RestartRotation();
            //}
        }

        if (Input.GetKey("up"))
        {
            //CHECK HERE TO SEE WHETHER ENERGIZING IS ALLOWED--will be disallowed if rotation is frozen at a bad angle 
            if (EnergizeButton != null && EnergizeButton.interactable && !NucleophileInFlight)
            {
                EnergizeNucleophile();
            }

            else
            {
                //print("can't energize bc angle is bad");
            }

            //Sounds are handled by MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>
        }

    }


    private void OnCollisionEnter(Collision other)
    {
        //Any collision should spawn a new nucleophile after 1.3 second delay
        DeniedSound.Play();
        GetComponent<Rigidbody>().AddForce(new Vector2 (0,0.8f*-ChlorideY_Velocity), ForceMode.VelocityChange);


        gameObject.tag = "Inactive";   //this deactivates the nucleophile, as the InvertCarbonScript looks for the "Nucleophile" tag when capsule is triggered
        print("nucleophile collided with " + other.gameObject);

        StartCoroutine(CountdownToDestruction(1.3f));  //a collision with electrophile atom (or cube) should trigger the respawning of the nucleophile with 1.3 sec delay
        //got some duplicate events, in which the nucleophile hit both the capsule trigger and a hydrogen atom on the electrophile (ricochet trigger)  

    }

    public IEnumerator CountdownToDestruction (float delayTime)
    {
        //print("Countdown to Destruction of nucleophile");
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);  //this is the nucleophile atom

        MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>().StopEnergizeSounds();
        MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateNucleophile();
    }
}
