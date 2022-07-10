using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialElectrophileScript : MonoBehaviour  //attached to the TutorialElectrophileMolecule
{
    public int RandomTorque;
    public float X_Velocity;

    public GameObject TutorialSpeechBubble;
    public GameObject MoleculeInstantiationManager;
    public Vector2 InstantiationPosition;
    public Vector3 AngularVelocityVector;
    public Vector3 LinearVelocityVector;

    public float RotationAngleTolerance;  //this is used to control difficulty level of the game--for challenging game, set to 0.09, for tolerant game, set to 0.15
    public float ActualMoleculeRotation;  //z-value of the ElectrophileMolecule's rotation vector--used to determine the score for a successful reaction

    //retrieved from the GlobalVariablesScript attached to the InstantiationManager GameObject
    private int MaxTorque;  //making MaxTorque high will increase rotation rate of the electrophile
    private float FreezeTimeLimit;  //setting this to a small value makes the game more difficult--in beginner game, the FreezeTimeLimit = 30 seconds

    public AudioSource GoodRotationLock;
    public AudioSource BadRotationLock;



    public Button EnergizeButton;



    // Start is called before the first frame update
    void Start()
    {
        MoleculeInstantiationManager = GameObject.Find("MoleculeInstantiationManager");
        TutorialSpeechBubble = GameObject.Find("TutorialSpeechBubble");

        FreezeTimeLimit = MoleculeInstantiationManager.GetComponent<GlobalVariableScript>().TimeLimitForFreezingRotation;
        MaxTorque = MoleculeInstantiationManager.GetComponent<GlobalVariableScript>().MaxTorque;

        RandomTorque = Random.Range(-MaxTorque, -MaxTorque+1);  //makes the molecule tumble in a clockwise direction--the values were increased 10x for methyl bromide
        X_Velocity = Random.Range(1.5f, 2.5f);

        GetComponent<Rigidbody>().inertiaTensor = new Vector3(1, 1, 1);
        GetComponent<Rigidbody>().centerOfMass = new Vector3(0,0,0);  //this means that the molecule will rotate around the central carbon instead of around an offset point

        if (TutorialSpeechBubble.GetComponent<TutorialScript>().MessageNumber < 10)
        //in Tutorial Scene, the electrophile first appears with rotation paused.  Rotation is initiated by calling "RestartRotation()" at the right time
        {
            //do nothing--this only applies to MessageNumber 9, which intoduces a stationary electrophile molecule.  MessageNumber 10, starts normal electrophile motion
        }

        else  //the commands below initiate rotation of the electrophile.  
        {
            GetComponent<Rigidbody>().AddTorque(0, 0, RandomTorque, ForceMode.Impulse);
            GetComponent<Rigidbody>().velocity = new Vector2(X_Velocity, 0);  //molecule moves across the screen at a random constant x-velocity
        }



        RotationAngleTolerance = MoleculeInstantiationManager.GetComponent<GlobalVariableScript>().AngleToleranceValue; //using MoleculeInstantiationManager as the keeper of variables

        if (GameObject.Find("EnergizeButton"))
        {
            EnergizeButton = GameObject.Find("EnergizeButton").GetComponent<Button>();  //In tutorial scene, EnergizeButton may be invisible            
        }

        GoodRotationLock = GameObject.Find("GoodRotationLockSound").GetComponent<AudioSource>();
        BadRotationLock = GameObject.Find("UhOhSound (bad rotation lock)").GetComponent<AudioSource>();

        AngularVelocityVector = GetComponent<Rigidbody>().angularVelocity;  //"record" the current rotation of the ElectrophileMolecule
        LinearVelocityVector = new Vector2(X_Velocity, 0);  //"record" the current linear velocity of the ElectrophileMolecule 



    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.x > 8 || transform.position.y > 8)
        {
            MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateNewElectrophileMolecule();
            //Instantiate(PrefabToSpawn, InstantiationPosition, Quaternion.identity);  //generates clone of clone of clone new clone of the rotating electrophile molecule
            Destroy(gameObject);
        }

        if (Input.GetKeyDown("space"))  //used to invoke BOTW "Stasis", where motion of the electrophile molecule is frozen
        {
            //print("space");
            if(TutorialSpeechBubble.GetComponent<TutorialScript>().MessageNumber < 18)
            {
                if (GetComponent<Rigidbody>().angularVelocity != Vector3.zero)  //this means that the electrophile is in motion and should be stopped
                {
                    StopRotationCaseOne();
                }

                else
                {
                    RestartRotation();
                }
            }

            else  //means MessageNumber >17 and CaseTwo applies--angle of locked rotation matters!
            {
                if (GetComponent<Rigidbody>().angularVelocity != Vector3.zero)  //this means that the electrophile is in motion and should be stopped
                {
                    StopRotationCaseTwo();
                }

                else
                {
                    RestartRotation();
                }
            }
            
        }


    }

    public void StopRotationCaseOne()  //this function will simply stop rotation, no checking of angles.  Used in Tutorial Scene early stages
    {
        print("case one");
        AngularVelocityVector = GetComponent<Rigidbody>().angularVelocity;  //"record" the current rotation of the ElectrophileMolecule
        LinearVelocityVector = GetComponent<Rigidbody>().velocity;  //"record" the current linear velocity of the ElectrophileMolecule 


        GetComponent<Rigidbody>().velocity = Vector2.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        StartCoroutine(CountdownToRestartRotation(2.5f));
    }

    public void StopRotationCaseTwo()  //this function will check to see if the rotation has been frozen at an acceptable angle to allow reactions
    {
        print("case two");

        AngularVelocityVector = GetComponent<Rigidbody>().angularVelocity;  //"record" the current rotation of the ElectrophileMolecule
        LinearVelocityVector = GetComponent<Rigidbody>().velocity;  //"record" the current linear velocity of the ElectrophileMolecule 

        GetComponent<Rigidbody>().velocity = Vector2.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;      

        ActualMoleculeRotation = Mathf.Abs(transform.rotation.z);
        print(ActualMoleculeRotation);

        if (ActualMoleculeRotation < RotationAngleTolerance)
        {
            //CHECK HERE FOR THE ANGLE AT WHICH ROTATION HAS BEEN PAUSED--IF WITHIN THE ACCEPTABLE RANGE, ACTIVATE THE "ENERGIZE NUCLEOPHILE" BUTTON!
            GoodRotationLock.Play();
            print("activate the Energize Button");
            EnergizeButton.interactable = true;
            
            TutorialSpeechBubble.GetComponent<TutorialScript>().SendGoodAngleMessage();        
        }

        else
        {
            BadRotationLock.Play();
            StartCoroutine(CountdownToRestartRotation(2.5f));
            print("send bad angle message here");
            TutorialSpeechBubble.GetComponent<TutorialScript>().SendBadAngleMessage();

        }

        //AngularVelocityVector = GetComponent<Rigidbody>().angularVelocity;  //"record" the current rotation of the ElectrophileMolecule
        //LinearVelocityVector = GetComponent<Rigidbody>().velocity;  //"record" the current linear velocity of the ElectrophileMolecule 


        //GetComponent<Rigidbody>().velocity = Vector2.zero;
        //GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //StartCoroutine(CountdownToRestartRotation(2));
    }


    public void RestartRotation()
    {
        print("RestartRotation");
        GetComponent<Rigidbody>().AddTorque(0, 0, RandomTorque, ForceMode.Impulse);  //this should restore the original angular velocity
        GetComponent<Rigidbody>().velocity = LinearVelocityVector;  //restores the prior linear velocity

        if (GameObject.Find("EnergizeButton"))
        {
            EnergizeButton.interactable = false;
            MoleculeInstantiationManager.GetComponent<EnergizeSoundScript>().StopEnergizeSounds();
            GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().ChlorideY_Velocity = 4;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        RestartRotation();
    }


    public IEnumerator CountdownToRestartRotation(float FreezeTimeLimit)
    {
        yield return new WaitForSeconds(FreezeTimeLimit);
        RestartRotation();
    }

    

}
