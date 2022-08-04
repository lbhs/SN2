using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HideLeaderboardScript : MonoBehaviour
{
    public TMP_Dropdown SceneChooserDropdown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideLeaderboard()
    {
        SceneChooserDropdown.value = 0;
        gameObject.SetActive(false);
    }
}
