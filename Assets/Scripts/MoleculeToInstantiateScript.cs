using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleculeToInstantiateScript : MonoBehaviour  //attached to the 
{
    public GameObject ElectrophileMoleculePrefab;    
    public GameObject ReverseElectrophilePrefab;
    public GameObject NucleophilePrefab;

    public GameObject TutorialElectrophileMolecule;  //only used in Tutorial Scene
    public GameObject NonRotatingElectrophilePrefab;  //only used in Tutorial Scene

    public Vector2 InstantiationPosition;
    public Vector2 ReverseInstantiationPosition;

    public Button EnergizeButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateNewElectrophileMolecule()  //called from RotatingElectrophileScript if molecule goes off screen (line 30) AND from DestroyThisMolecule script, which is attached to the AnimatedRxnComplex
    {
        Instantiate(ElectrophileMoleculePrefab, InstantiationPosition, Quaternion.identity);
        EnergizeButton.interactable = false;

    }

    public void InstantiateReverseElectrophileMolecule()  //called from RotatingElectrophileScript if molecule goes off screen (line 30) AND from DestroyThisMolecule script
    {
        Instantiate(ReverseElectrophilePrefab, ReverseInstantiationPosition, Quaternion.identity);
    }

    public void InstantiateNucleophile()
    {
        if (GameObject.FindGameObjectWithTag("Nucleophile"))
        {
            print("nucleophile already present");
        }

        else
        {
            Instantiate(NucleophilePrefab);
        }
        
    }

    public void InstantiateNonRotatingElectrophile(float Z_Rotation)
    {

        Instantiate(NonRotatingElectrophilePrefab, new Vector2 (2,5), Quaternion.Euler(new Vector3 (0,0,Z_Rotation)));
    }
    
}
