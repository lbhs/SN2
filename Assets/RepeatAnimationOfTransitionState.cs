using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatAnimationOfTransitionState : MonoBehaviour
{
    public GameObject AnimatedTransitionState;
    public float WaitTimeForRespawn;
    public Vector3 InstantiationLocation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRespawnCountdown()
    {
        StartCoroutine(RespawnTransitionStateModel());
    }

    public IEnumerator RespawnTransitionStateModel()
    {
        yield return new WaitForSeconds(WaitTimeForRespawn);
        Instantiate(AnimatedTransitionState, InstantiationLocation, Quaternion.Euler(new Vector3(0, 10, 0)));
        
    }

}
