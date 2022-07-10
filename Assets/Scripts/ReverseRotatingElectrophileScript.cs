using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseRotatingElectrophileScript : MonoBehaviour
{
    public int RandomTorque;
    public float X_Velocity;
    public GameObject MoleculeInstantiationManager;
    public Vector2 InstantiationPosition;
    public Vector3 AngularVelocityVector;
    public Vector3 LinearVelocityVector;

    public bool Freezable;


    // Start is called before the first frame update
    void Start()
    {
        MoleculeInstantiationManager = GameObject.Find("MoleculeInstantiationManager");
        RandomTorque = Random.Range(100, 250);  //makes the molecule tumble in a clockwise direction--the values were increased 10x for methyl bromide
        X_Velocity = Random.Range(-8f, -3f);
        GetComponent<Rigidbody>().AddTorque(0, 0, RandomTorque, ForceMode.Impulse);
        GetComponent<Rigidbody>().velocity = new Vector2(X_Velocity, 0);  //molecule moves across the screen at a random constant x-velocity
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -12 || transform.position.y > 8)
        {
            MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateReverseElectrophileMolecule();
            //Instantiate(PrefabToSpawn, InstantiationPosition, Quaternion.identity);  //generates clone of clone of clone new clone of the rotating electrophile molecule
            Destroy(gameObject);
        }
        
        if (Freezable && Input.GetKeyDown("1"))  //used to invoke BOTW "Stasis", where motion of the electrophile molecule is frozen
        {
            //print("1");
            if (GetComponent<Rigidbody>().angularVelocity != Vector3.zero)  //this means that the electrophile is in motion and should be stopped
            {
                StopRotation();
            }

            else
            {
                RestartRotation();
            }
        }


    }

    public void StopRotation()
    {
        AngularVelocityVector = GetComponent<Rigidbody>().angularVelocity;  //"record" the current rotation of the ElectrophileMolecule
        LinearVelocityVector = GetComponent<Rigidbody>().velocity;  //"record" the current linear velocity of the ElectrophileMolecule 


        GetComponent<Rigidbody>().velocity = Vector2.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void RestartRotation()
    {
        GetComponent<Rigidbody>().angularVelocity = AngularVelocityVector;  //restores the prior angular velocity!
        GetComponent<Rigidbody>().velocity = LinearVelocityVector;  //restores the prior linear velocity

        //GetComponent<Rigidbody>().velocity = new Vector2(2.5f, 0);
    }
}
