using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Button AdvanceTutorialButton;
    public GameObject TutorialSpeechBubble;  //move this offscreen when appropriate
    public TMP_Text TutorialTextBox;

    public GameObject MoleculeInstantiationManager;

    public GameObject TutorialArrow;  //move this around to point at various features--can change color and size of the arrow
   
    public int MessageNumber;

    public GameObject Nucleophile;
    public GameObject NucleophileIDText;  //Activated in Step 1, Deactivated in Step 3
    public GameObject LeftButton;
    public GameObject RightButton;

    public GameObject ElectrophileMolecule;  //starts hidden
    public GameObject ElectrophileMoleculeIDText;  //this gives the electrophile molecule a title

    public GameObject NonRotatingElectrophileMolecule;  //this is a prefab that lacks the RotatingElectrophileScript

    public GameObject EnergizeButton;  //start with this hidden, then have it appear in step 5
    public GameObject LockRotationButton;  //start with this hidden, then have it appear in Step 11
    public bool LockButtonHasBeenPressed;  //need user to press Lock Button before allowing tutorial to progress

    public GameObject ReturnToMainMenuButton; 

    public List<string> TutorialMessages = new List<string>();  //this is where all the messages given to the user are stored!



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdvanceTutorial()
    {
        //print("Advance Tutorial");
        MessageNumber++;
        TutorialSpeechBubbleTextDisplay();
    }

    public void TutorialSpeechBubbleTextDisplay()
    {
        TutorialTextBox.text = TutorialMessages[MessageNumber];

        //Message 1 = The green sphere represents a nucleophile!
        if(MessageNumber == 1)
        {
            NucleophileIDText.SetActive(true);
        }

        //Message 2 = Specifically, this nucleophile is a chloride ion

        //Message 3 = The L and R buttons onscreen can move the nucleophile
        if(MessageNumber == 3)
        {
            NucleophileIDText.SetActive(false);
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
        }

        //Message 4 = Right and Left arrows on your keyboard can also move the nucleophile

        //Message 5 = Clicking on this button will energize the nucleophile
        if(MessageNumber == 5)
        {
            EnergizeButton.SetActive(true);
            EnergizeButton.GetComponent<Button>().interactable = true;
            TutorialArrow.transform.position = new Vector2(4.8f, -6);
            TutorialArrow.GetComponent<Image>().color = Color.green;
            AdvanceTutorialButton.interactable = false;

        }

        //Message 6 = UP ARROW can also energize the nucleophile
        

        //Message 7 = The nucleophile's energy determines its speed

        //Message 8 = You will shoot the nucleophile at a target molecule
        if (MessageNumber == 8)
        {
            Nucleophile = GameObject.FindGameObjectWithTag("Nucleophile");  //hide the nucleophile and its associated buttons so that the user will focus on electrophile molecule
            Nucleophile.SetActive(false);
            print("hiding nucleophile now");

            LeftButton.SetActive(false);
            RightButton.SetActive(false);

            EnergizeButton.SetActive(false);
            EnergizeButton.GetComponent<Button>().interactable = false;
            ElectrophileMolecule.SetActive(true);
            ElectrophileMoleculeIDText.SetActive(true);
            //ElectrophileMolecule.GetComponent<RotatingElectrophileScript>().StopRotation();  //if Tutorial is active, rotation is not initiated in the Start Function of RotatingElectrophileScript
        }


        //Message 9 = The target molecule is called an Electrophile.  

        //Message 10 = The electrophile molecule will move automatically across the screen
        if (MessageNumber == 10)
        {
           ElectrophileMolecule.GetComponent<TutorialElectrophileScript>().RestartRotation();
        }

        //Message 11 = You can freeze the electrophile molecule's motion by clicking the Lock button
        if (MessageNumber == 11)
        {
            LockRotationButton.SetActive(true);
            AdvanceTutorialButton.interactable = false;
        }

        //ONCE LOCKED, ENABLE MESSAGE 12



        //Message 12 = You can also use the SPACE BAR on your keyboard to lock the electrophile molecule's motion
        //RESTART A NEW ELECTROPHILE
        if(MessageNumber == 12)
        {
            LockRotationButton.SetActive(false);
            AdvanceTutorialButton.interactable = true;
        }


        //Message 13 = In order to initiate a successful reaction the nucleophile needs to hit the red spot on the electrophile
        //instantiate NonRotatingElectrophileMolecule, Activate the nucleophile so that the user can shoot at the stationary target
        if(MessageNumber == 13)
        {
            Destroy(GameObject.FindGameObjectWithTag("TutorialElectrophileMolecule"));
            MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateNonRotatingElectrophile(12);
            Nucleophile.SetActive(true);
            NucleophileIDText.SetActive(true);
            TutorialArrow.transform.position = new Vector2(2.1f, 2.5f);
            TutorialArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
            TutorialArrow.GetComponent<Image>().color = Color.red;
        }



        //Message 14 = Fire the nucleophile at the electrophile!
        if (MessageNumber == 14)
        {
            //TutorialSpeechBubble.transform.position = new Vector2(-10, 0);
            NucleophileIDText.SetActive(false);
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            EnergizeButton.SetActive(true);
            EnergizeButton.GetComponent<Button>().interactable = true;
            AdvanceTutorialButton.interactable = false;

        }


        //Message 15 = You get maximum points if the Electrophile is frozen so that the red spot is Straight Down

        if(MessageNumber == 15)
        {
            ElectrophileMoleculeIDText.SetActive(false);
            MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateNonRotatingElectrophile(0);            
            LeftButton.SetActive(false);
            RightButton.SetActive(false);
            EnergizeButton.SetActive(false);
            AdvanceTutorialButton.interactable = true;
            TutorialArrow.transform.position = new Vector2(2.1f, 2.5f);
            TutorialArrow.transform.rotation = Quaternion.Euler(0, 0, -90);
        }



        //Message 16 = try again to fire the nucleophile at the electrophile!
        if (MessageNumber == 16)
        {
            TutorialArrow.transform.position = new Vector2(200, 2.5f);
            AdvanceTutorialButton.interactable = false;
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            EnergizeButton.SetActive(true);
            MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateNucleophile();
        }

        //Message 17 = 25 pts is maximum score per collision
        if(MessageNumber == 17)
        {
            AdvanceTutorialButton.interactable = true;
        }

        //Message 18 = Timer will countdown
        if(MessageNumber == 18)
        {
            TutorialArrow.transform.position = new Vector2(-10.8f, 5.3f);
            TutorialArrow.transform.rotation = Quaternion.Euler(0, 0, 90);
            TutorialArrow.GetComponent<Image>().color = Color.cyan;
            
            LeftButton.SetActive(false);
            RightButton.SetActive(false);
            EnergizeButton.SetActive(false);
        }

        //Message 19 = Try to lock rotation of the Electrophile when the red spot is facing straight down
        if (MessageNumber == 19)
        {
            TutorialArrow.transform.position = new Vector2(200, 3.3f);

            LockRotationButton.SetActive(true);
            AdvanceTutorialButton.interactable = false;
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            EnergizeButton.SetActive(true);

            MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateNucleophile();
            MoleculeInstantiationManager.GetComponent<MoleculeToInstantiateScript>().InstantiateNewElectrophileMolecule();

        }

        //Message 20 = Tutorial Complete, press Return To Main Menu Button
        if (MessageNumber == 20)
        {
            
            LockRotationButton.SetActive(false);
            AdvanceTutorialButton.interactable = false;
            LeftButton.SetActive(false);
            RightButton.SetActive(false);
            EnergizeButton.SetActive(false);

            ReturnToMainMenuButton.SetActive(true);

        }

    }

    public void ActivateAdvanceTutorialButton()//called by the LockRotationScript 
    {
        AdvanceTutorialButton.interactable = true;
    }

    public void SendGoodAngleMessage()
    {
        TutorialTextBox.text = "The Red Spot is pointing Down!  Fire your nucleophile!!";
        StartCoroutine (CountdownToEraseMessage(5));
    }

    public void SendBadAngleMessage()
    {
        TutorialTextBox.text = "The red spot isn't pointing down--unlock rotation and try again.";
        StartCoroutine(CountdownToEraseMessage(3));
    }

    public IEnumerator CountdownToEraseMessage(int WaitTime)
    {
        yield return new WaitForSeconds(4);
        TutorialTextBox.text = null;
    }
}
