using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_crash : MonoBehaviour
{
    private float last_speed;
    public bool crushing;
    private float time_going_down = 0f;
    private float time_going_up = 0f;

    public float cam_smooth_time = 0.09f;
    [SerializeField] private float cinematic_amount =0.1f;
    [SerializeField] private float Camera_offset_f = 1f;
    [SerializeField] private float Camera_offset_up = 5f;
    Vector3 transition = Vector3.up;
    private Vector3 velocity = Vector3.zero;
    private float rotation;
    private int rot_dir;
    // Start is called before the first frame update
    void Start()
    {
        rotation = 0;
        crushing = false;
        transition = transform.up;
    }
    public void set_speed(float speed)
    {
        last_speed = speed; 
    }
    // Update is called once per frame
    void Update()
    {
        if(crushing)
        {
            rotation+=2f*Time.deltaTime* rot_dir;
            if(rotation>=2)
            {
                rotation = 2;
            }
            float displace = 0;
            if (time_going_up <= 0)
            {
                time_going_down += Time.fixedDeltaTime;
                displace = 0.2f * time_going_down;//cng
                transform.position = new Vector3(transform.position.x, (transform.position.y - displace), transform.position.z);

            }
            else
            {
                time_going_up -= Time.fixedDeltaTime;
                displace = 0.2f * time_going_up;
                transform.position = new Vector3(transform.position.x, (transform.position.y + displace), transform.position.z);
            }
            transform.position = transform.position + transform.forward * Time.deltaTime * last_speed;
            transform.Rotate(0, 0, rotation);
            camera_move();
            coll_detect();
        }
        else
        {
            float a = Input.GetAxis("Horizontal");
           if(a <=0)
            {
               rot_dir = 1;
            }
           else
            {
                rot_dir = -1;
            }

        }
       
    }
    private void camera_move()
    {
        Vector3 cameratarget = transform.position - (Camera_offset_f * transform.forward) + Vector3.up * Camera_offset_up;//cng
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cameratarget, ref velocity, cam_smooth_time);
        transition = Vector3.SmoothDamp(transition, Vector3.up, ref velocity, cinematic_amount);//cng
        Camera.main.transform.LookAt(transform.position + transform.forward * 30f, transition);
    }
    private void coll_detect()
    {
        if (Physics.CheckBox(transform.position + transform.forward * 5, transform.localScale / 2f, transform.rotation, 3))
        {
            this.GetComponent<plane_crash>().enabled = false;
            GameObject find = GameObject.Find("bomb_impact_particle");
            GameObject effect = GameObject.Instantiate(find);
            effect.transform.position = transform.position;
            effect.transform.localScale = new Vector3(1f, 1f, 1f);
            effect.transform.localScale = Vector3.one * 3f;
            effect.transform.forward = Vector3.up;
            Destroy(effect, 2f);
        }
    }
}
