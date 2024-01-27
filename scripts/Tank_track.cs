using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_track : MonoBehaviour
{

    public GameObject plane;
    private Tank_shoot track;
    public float rot_speed = 20f;
    private Vector3 target;
    public Vector3 rot_target;
    private float rot_amount;
    // Start is called before the first frame update
    void Start()
    {
        track = this.GetComponent<Tank_shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        rot_target = plane.transform.position - transform.position;
        rot_target = new Vector3(rot_target.x, 0, rot_target.z);
        rot_amount = Vector3.SignedAngle(transform.up, rot_target, transform.forward);
        transform.RotateAround(transform.forward, Time.deltaTime * rot_amount * rot_speed);
        //
        target = plane.transform.position - transform.position;
        float dist = Vector3.Magnitude(target);
        if (dist <= 400)
        {
            track.firing = true;
            track.shot_dir = target.normalized * (dist * 1f) +
                             plane.transform.forward.normalized * plane.GetComponent<plane_controll>().f_speed * (dist / 1000f);//offset dist=v*t + vp*t
        }
        else
        {
            track.firing = false;
        }
    }
}
