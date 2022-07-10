using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class resetScene : MonoBehaviour   //Attached to die_Object
{
    public GameObject DisplayCanvas;
	public GameObject ScoreDisplay;
    public Font ken;
    public static int FinalScore;

    public void gameOver()
    {
        DontDestroyOnLoad(DisplayCanvas);
        FinalScore = ScoringScript.Score;
        StartCoroutine(LoadSceneTimer());  //Scene(1) is temporarily the GameOverScene (scene 0 is Advan scene)
        //SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);  //THIS WAS REALLY COOL TO SEE--DIDN'T ERASE THE PREVIOUS SCENE!!!!
        SceneManager.LoadScene(4);  //scene 4 is the GameOver Scene
        Debug.Log(FinalScore);
        Time.timeScale = 1;  //start time flowing again so that the rest of the script can play
        

    }

    public void reset()
    {
        SceneManager.LoadScene(0);

        //StartCoroutine(LoadSceneTimer());
        //SceneManager.LoadScene(2, LoadSceneMode.Additive);
        //SceneManager.UnloadSceneAsync(1);
    }


    //public Animator FadeAnimator;
    //public float FadeTime = 1;

    IEnumerator LoadSceneTimer()
    {
        //FadeAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(0.2f);
        Destroy(DisplayCanvas);

    }
}
