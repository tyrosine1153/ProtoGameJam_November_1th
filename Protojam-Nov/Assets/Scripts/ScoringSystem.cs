using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringSystem : MonoBehaviour
{
    GameManager gm = null;
    TextMeshProUGUI maxScore = null;
    TextMeshProUGUI success = null;
    TextMeshProUGUI fail = null;
    // Start is called before the first frame update
    void Start()
    {
        maxScore = GameObject.Find("MaxScore").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("GameManager") == null)
        {            
            maxScore.text = "" + 0;
        }
        else
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            maxScore.text = ""+gm.CurrentGameData.OrderCount;
            if (GameObject.Find("Success") != null)
            {
                success = GameObject.Find("Success").GetComponent<TextMeshProUGUI>();
                fail = GameObject.Find("Fail").GetComponent<TextMeshProUGUI>();
                success.text = "" + gm.CurrentGameData.SuccessCount;
                fail.text = "" + gm.CurrentGameData.FailCount;
            }
        }
    }

}
