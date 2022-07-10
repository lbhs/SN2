using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InvertCarbonScript : MonoBehaviour  //attached to the parent electrophile molecule (which is physically the central carbon atom)
{
    //THIS SCRIPT HANDLES THE ANIMATED COMPLEXES FOR BOTH A PRODUCTIVE AND A NON-PRODUCTIVE COLLISION BETWEEN NUCLEOPHILE AND ELECTROPHILE

    public Animator sp3_CarbonAnimator;
    private int Countdown;
    public AudioSource NucleophileCollisionSound;
    public AudioSource BadRotationSound;  //plays if the electrophile molecule's rotation is offline when collision occurs with nucleophile

    public GameObject AnimatedTransitionState;  //this animated complex is displayed if collision meets rxn requirements (rotation angle and nucleophile energy)
    public GameObject AbortiveReactionTransitionState;  //this animated complex is displayed if collision does not meet requirements for a productive reaction

    public Vector2 InstantiationLocation;  //should be the location of the electrophile molecule. . .
    public Quaternion InstantiationRotation;
    public AudioSource SuccessSound;
    public AudioSource DeniedSound;

    public float ActivationEnergyThreshold;  //this value is stored in the GlobalVariableScript attached to the MoleculeInstantiationManager
    public float NucleophileEnergy;  //this value is passed from the ChlorideMovementControlScript when the nucleophile is fired

    public int DifficultyMultiplierForRotationalAccuracy;  //standard value is 40,  In advanced game, make this 80

    public GameObject ScoreForThisCollisionDisplay;  //allows for display of the score of each collision (each successful reaction)

    // Start is called before the first frame update
    void Start()
    {
        ActivationEnergyThreshold = GameObject.Find("MoleculeInstantiationManager").GetComponent<GlobalVariableScript>().ActivationEnergyThresholdForThisScene;
        SuccessSound = GameObject.Find("SuccessSound").GetComponent<AudioSource>();
        BadRotationSound = GameObject.Find("UhOhSound (bad rotation lock)").GetComponent<AudioSource>();
        ScoreForThisCollisionDisplay = GameObject.Find("ScoreForThisCollision");
        DifficultyMultiplierForRotationalAccuracy = GameObject.Find("MoleculeInstantiationManager").GetComponent<GlobalVariableScript>().DifficultyMultiplierForScoring;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other + " triggered the capsule's collider");
        print("nucleophile energy = " + NucleophileEnergy);
        if(other.tag == "Nucleophile")  //If nucleophile has previously collided with an atom, its tag becomes "Inactive"
                                        // && Mathf.Abs(gameObject.transform.rotation.z) < .09)  No longer needed because fire nucleophile button is only activated if rotation is correct


        {
            if (NucleophileEnergy < ActivationEnergyThreshold)
            {
                print(NucleophileEnergy + " is less than " + ActivationEnergyThreshold);
                AbortiveReaction(other.gameObject);  //other.gameObject is the nucleophile that collided with electrophile

                //START ABORTIVE ANIMATION
            }

            else  //THIS IS THE CODE THAT RUNS IF A SUCCESSFUL COLLISION HAS OCCURRED BETWEEN NUCLEOPHILE AND THE ELECTROPHILE'S CAPSULE TARGET
            {
                print("successful collision");
                //print(Mathf.Abs(gameObject.transform.rotation.z));
                InvertTheCarbon(other.gameObject, transform.rotation.z);
                
            }
            
        }

        //ROTATION CHECK IS NOW DONE IN RotatingElectrophileScript.  If rotation is within tolerance, it will activate the button/key used to energize the nucleophile
        //else if(gameObject.transform.rotation.z >= 0.09)
        //{
        //    print("denied because rotation = " + gameObject.transform.rotation.z);
        //    AbortiveReaction(other.gameObject);  //other.gameObject is the nucleophile that collided with electrophile
        //    BadRotationSound.Play();
        //}

    }

    public void InvertTheCarbon(GameObject Nucleophile, float ActualMoleculeRotation)
    {
        print("started InvertTheCarbon subroutine");
        InstantiationLocation = new Vector2(transform.position.x, transform.position.y);  //this gets the current location of the electrophile molecule
        InstantiationRotation = gameObject.transform.rotation;
        Destroy(gameObject);  //this is the electrophile molecule
        Destroy(Nucleophile);
        SuccessSound.Play();
        int ScoreForThisCollision = 25 - Mathf.RoundToInt(Mathf.Abs(ActualMoleculeRotation * DifficultyMultiplierForRotationalAccuracy));  //higher score results if rotation is near zero!
        ScoringScript.Score += ScoreForThisCollision;
        print("score for this reaction = " + ScoreForThisCollision);
        ScoreForThisCollisionDisplay.GetComponent<TransientScoreDisplayScript>().DisplayTransientScore(ScoreForThisCollision);


        Instantiate(AnimatedTransitionState, InstantiationLocation, InstantiationRotation);  //this replaces the game objects with the animated complex that results in products
        //AnimatedTransitionState goes through carbon inversion animation, then triggers the DestroyThisMolecule Script that will trigger instantiation of a new electrophile
        
    }


    public void AbortiveReaction(GameObject Nucleophile)
    {
        print("started AbortiveReaction subroutine");
        InstantiationLocation = new Vector2(transform.position.x, transform.position.y);  //this gets the current location of the electrophile molecule
        InstantiationRotation = gameObject.transform.rotation;
        Destroy(gameObject);  //this is the electrophile molecule
        Destroy(Nucleophile);
        Instantiate(AbortiveReactionTransitionState, InstantiationLocation, InstantiationRotation);  //this replaces the game objects with the Abortive animated complex


    }
    
    
}
