using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransientScoreDisplayScript : MonoBehaviour
{
    public TMP_Text TransientScoreDisplayForThisCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayTransientScore(int ScoreForThisCollision)
    {
        //print("Transient score = " + ScoreForThisCollision);
        TransientScoreDisplayForThisCollision.text = ScoreForThisCollision.ToString() + " pts";
        StartCoroutine(ClearTransientScoreDisplay());
    }
    
    
    public IEnumerator ClearTransientScoreDisplay()
    {
        
        yield return new WaitForSeconds(2);
        //print("timer expired, clear display now");
        TransientScoreDisplayForThisCollision.text = "";
    }

    
}
