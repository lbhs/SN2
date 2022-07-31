using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownToRebirth : MonoBehaviour
{
    public GameObject RespawningManager;

    // Start is called before the first frame update
    void Start()
    {
        RespawningManager = GameObject.Find("RespawnTranstitionStateEmptyGameObject");
        RespawningManager.GetComponent<RepeatAnimationOfTransitionState>().StartRespawnCountdown();
        StartCoroutine (Countdown());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Countdown()
    {
        yield return new WaitForSeconds(8);
        Destroy(gameObject);
    }
}
