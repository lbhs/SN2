using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using SimpleJSON;


//public class Player
//{
//    public string Name;
//    public int Score;
//}

public class ShowLeaderboardScript : MonoBehaviour
{
   
    public List<Player> players = new List<Player>();
    public GameObject[] textBoxes;

    private void OnEnable()
    {
        clear();
        StartCoroutine(GetString());
        //"163XTG4mOdzoAZv-HHcKGoRtDum-XvTOsajDYoiO97VA", "Leaderboard")
    }

    IEnumerator GetString()
    {
        print("GetString initiated");
        //string url = "https://sheets.googleapis.com/v4/spreadsheets/163XTG4mOdzoAZv-HHcKGoRtDum-XvTOsajDYoiO97VA/values/Leaderboard!" + "B2" + ":" + "C1001" + "?key=" + SecretKey.GSkey;
        string url = "https://sheets.googleapis.com/v4/spreadsheets/1zbJyB3-Hj_7Qcjj1p8HkNCgQbSBLMBaHfhJiUu1DYgM/values/Leaderboard!" + "B2" + ":" + "C1001" + "?key=" + SecretKey.GSkey;
        WWW www = new WWW(url);
        
        yield return www;
        string RecivedJSON;
        RecivedJSON = www.text;
        var J = JSON.Parse(RecivedJSON);
        for (int i = 2; i < 1001; i++)
        {
            Player p = new Player();
            string n = J["values"][i][0].Value;
            p.Name = n;
            string s = J["values"][i][1].Value;
            int a;
            if (int.TryParse(s, out a))
            {
                p.Score = int.Parse(s);
                players.Add(p);
            }
            print (p.Name);
        }
        players = players.OrderByDescending(i => i.Score).ToList();
        applyUpdates();

        
    }

    void applyUpdates()
    {
        print("applyUpdates initiated");
        //players.Sort((s1, s2) => s1.Score.CompareTo(s2.Score));
        players = players.OrderByDescending(i => i.Score).ToList();
        int num = 0;
        foreach (var item in textBoxes)
        {
            item.transform.GetChild(0).GetComponent<Text>().text = num + 1 + ":";
            item.transform.GetChild(1).GetComponent<Text>().text = players[num].Name;
            item.transform.GetChild(2).GetComponent<Text>().text = players[num].Score.ToString();
            num++;
        }
        //Debug.Log(players[players.Count-1].Score);
    }

    void clear()
    {
        print("clear initiated");
        players.Clear();
        int i = 1;
        foreach (var item in textBoxes)
        {
            item.transform.GetChild(1).GetComponent<Text>().text = "Loading";
            item.transform.GetChild(2).GetComponent<Text>().text = "...";
            i++;
        }
    }
}
