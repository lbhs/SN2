using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovementScript : MonoBehaviour  //attached to moving Cubes 
{
    public GameObject MoleculeInstantiationManager;
    private int MaxCubeSpeed;  //retrieved from GlobalVariableScript attached to MoleculeInstationManager GameObject
    private float InitialCubeDirection;
    public string InitialMovementDirection;

    // Start is called before the first frame update
    void Start()
    {
        MaxCubeSpeed = MoleculeInstantiationManager.GetComponent<GlobalVariableScript>().CubeSpeed;

        if(InitialMovementDirection == "Right")
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > 11)
        {
            MoveLeft();
        }

        else if (transform.position.x < -11)
        {
            MoveRight();
        }

    }

    void MoveRight()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(Random.Range(MaxCubeSpeed/2, MaxCubeSpeed), 0);
    }

    void MoveLeft()
    {
        GetComponent<Rigidbody>().velocity = new Vector2(-Random.Range(MaxCubeSpeed / 2, MaxCubeSpeed), 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Cube collision");
        GameObject.FindGameObjectWithTag("ElectrophileMolecule").GetComponent<RotatingElectrophileScript>().RestartRotation();
    }
}
