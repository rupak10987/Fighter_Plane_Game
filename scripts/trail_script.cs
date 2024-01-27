using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trail_script : MonoBehaviour
{
    public GameObject o;
    TrailRenderer balsal;
    plane_controll updater;
    float e_time;
// Start is called before the first frame update
void Start()
    {
        balsal = GetComponent<TrailRenderer>();
        balsal.emitting = false;
        updater = o.GetComponent<plane_controll>();
    }

    // Update is called once per frame
    void Update()
    {
        e_time += Time.deltaTime;
        if (e_time >= 100f)
            e_time = 0f;
        float speed =updater.f_speed;
        if (speed>60 && balsal.emitting==false)
        {
            balsal.emitting = true;
        }
        else if (balsal.emitting && speed<=60)
        {

            balsal.emitting = false;
        }
    }
}
