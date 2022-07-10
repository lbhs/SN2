using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementButtonsScript : MonoBehaviour  //attached to the MoveRight and MoveLeft onscreen buttons
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeftButtonPressed()
    {
        if(GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().NucleophileInFlight == false)
        {
            GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().MoveLeft();
        }
        
    }

    public void RightButtonPressed()
    {
        if (GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().NucleophileInFlight == false)
        {
            GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().MoveRight();
        }
    }

    public void StopMovement()
    {
        if (GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().NucleophileInFlight == false)
        {
            GameObject.FindGameObjectWithTag("Nucleophile").GetComponent<ChlorideMovementControlScript>().StopMovementOfNucleophile();
        }
    }

}
