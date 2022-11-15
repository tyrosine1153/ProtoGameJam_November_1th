using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMovAnim : MonoBehaviour
{
    bool drive = false;
    Vector2 Apoint = new Vector2(0, -122);
    Vector2 Bpoint = new Vector2(0, -124);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Ride", 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Ride(float DelayTime)
    {
        if (drive == false) { transform.position = Bpoint; drive = true; }
        else { transform.position = Apoint; drive = false; }
        Debug.Log(transform.position);
            yield return new WaitForSeconds(DelayTime);
        StartCoroutine("Ride", DelayTime);
    }
}
