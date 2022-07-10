using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThisMolecule : MonoBehaviour  //Attached to the AnimatedComplex GameObject.  Called from the animation attached to this object
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyMolecule()  //called from the animation attached to the animated complex
    {
        if (GameObject.Find("TutorialSpeechBubble"))
        {
            Destroy(gameObject);
            GameObject.Find("TutorialSpeechBubble").GetComponent<TutorialScript>().AdvanceTutorial();             
            
        }

        else
        {
            GameObject.Find("MoleculeInstantiationManager").GetComponent<MoleculeToInstantiateScript>().InstantiateNewElectrophileMolecule();
            GameObject.Find("MoleculeInstantiationManager").GetComponent<MoleculeToInstantiateScript>().InstantiateNucleophile();
            Destroy(gameObject);
        }
        

    }
}
