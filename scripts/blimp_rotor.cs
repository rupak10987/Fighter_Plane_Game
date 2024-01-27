using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blimp_rotor : MonoBehaviour
{
    // Start is called before the first frame update

    private float rot_speed=1000;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  rot_speed += Time.deltaTime * r_speed_m;
        transform.RotateAround(transform.position, transform.up, rot_speed*Time.deltaTime);
    }
}
