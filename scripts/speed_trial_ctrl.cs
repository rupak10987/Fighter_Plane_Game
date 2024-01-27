using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speed_trial_ctrl : MonoBehaviour
{
    public GameObject plane;
    public Camera cam_to_follow;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<ParticleSystem>().enableEmission = false;
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cam_to_follow.transform.position+cam_to_follow.transform.forward*.2f;
        transform.forward = -cam_to_follow.transform.forward;
       
        if (plane.GetComponent<plane_controll>().f_speed >=120)
        {
            
            gameObject.GetComponent<ParticleSystem>().enableEmission = true;
            
        }
        else
            gameObject.GetComponent<ParticleSystem>().enableEmission = false;
    
    }
}
