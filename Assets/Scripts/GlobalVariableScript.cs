using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariableScript : MonoBehaviour  //attached to the MoleculeInstantiationManager GameObject
{
    //THESE ARE VARIABLES THAT SET THE DIFFICULTY FOR THE ROUND IN QUESTION
    public float AngleToleranceValue;
    public float TimeLimitForFreezingRotation;
    public float ActivationEnergyThresholdForThisScene;
    public int DifficultyMultiplierForScoring;  //40 is standard, 80 is for advanced level

    public bool UseMovingCubes;
    public int NumberOfCubes;
    public int CubeSpeed;

    public int MaxTorque; //higher torque results in fast rotation of the electrophile

    public int DifficultyLevel;  //1 = easy game, 2 = Challenging Game

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
