using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneChooserScript : MonoBehaviour
{
    public TMP_Dropdown SceneChooserDropdown;

    // Start is called before the first frame update
    void Start()
    {
        print(SceneChooserDropdown.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChooseSceneFromDropdown()
    {
        Time.timeScale = 1;
        ScoringScript.Score = 0;
        SceneManager.LoadScene(SceneChooserDropdown.value);
    }

}
