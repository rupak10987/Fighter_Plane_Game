using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA_track_plane : MonoBehaviour
{
    
    public GameObject plane;
    public GameObject self;
    public enemy_shoot track;
    public float rot_speed=20f;
    private Vector3 target;
    public Vector3 rot_target;
    private float rot_amount;
    // Start is called before the first frame update
    void Start()
    {
        track = self.GetComponent<enemy_shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        rot_target = plane.transform.position-transform.position;
        rot_target=new Vector3(rot_target.x, 0, rot_target.z);
        rot_amount = Vector3.SignedAngle(transform.up,rot_target, transform.forward);
        transform.RotateAround(transform.forward, Time.deltaTime * rot_amount* rot_speed);
        //
        target = (plane.transform.position)-transform.position;
        float dist=Vector3.Magnitude(target);
        if(dist<=800)
        {
            track.firing = true;
            track.shot_dir = target.normalized*(dist*1f)+
                             plane.transform.forward.normalized* plane.GetComponent<plane_controll>().f_speed*(dist / 800f);//offset dist=v*t + vp*t 600 speed of bullet
        }
        else
        {
            track.firing = false;
        }
    }
}
