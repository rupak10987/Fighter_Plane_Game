using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    plane_controll updater;
    private float rot_speed;
    public float r_speed_m=1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updater = obj.GetComponent<plane_controll>();
        rot_speed = updater.f_speed;
        if (rot_speed < 20f)
            rot_speed = 20f;
        rot_speed*=Time.deltaTime*r_speed_m;
        transform.RotateAround(transform.position, transform.up, rot_speed);

    }
}
