using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane_controll : MonoBehaviour
{
    public Rigidbody rb;
    public float mousesensitivity = 300f;
    private float rot_speed = 1.5f;
    public float max_rotation_speed;
    public float f_speed = 60.0f;
    public float accelaration=20.0f;
    public float cam_smooth_time = 0.09f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 velocity2 = Vector3.zero;
    Vector3 transition= Vector3.up;
    [SerializeField] private float cinematic_amount;
    [SerializeField] private float Camera_offset_f;
    [SerializeField] private float Camera_offset_up;
    private float time_going_down=0f;
    private float time_going_up = 0f;
    private float roll_grav_up_time=0f;
    private float roll_grav_down_time=0f;
    private float controll_acess_at_start_timer;
    #region mouse
    private Vector3 last_mouse_Pos;
    Vector2 delta;
    private bool is_aim_down_sight=false;
    #endregion

    private int optional_view_point;
    private float view_timer;
    public float gravity=0.01f;

    public Vector3 optional_view1;
    public Vector3 optional_view2;
    void Start()
    {
        
       controll_acess_at_start_timer = 0f;
        last_mouse_Pos = Input.mousePosition;
        optional_view_point = 1;
        view_timer = 0;
    }
    private void Awake()
    {
        
    }

    private void coll_detect()
    {
        if(Physics.CheckBox(transform.position+transform.forward*5, transform.localScale / 2f,transform.rotation,3))
        {
            gameObject.GetComponent<Damager>().KILL_Damage(gameObject.GetComponent<Damageable>());
        }
    }
    void FixedUpdate()
    {
        Vector3 temp_forward_vec = transform.forward;//
        controll_acess_at_start_timer += Time.deltaTime;
        coll_detect();
        Gravity_sim();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rot_speed += Time.fixedDeltaTime*2f;
            if (rot_speed > max_rotation_speed)
                rot_speed = max_rotation_speed;
        }
        else
        { if (rot_speed > max_rotation_speed)//cng
            {
                rot_speed -= Time.fixedDeltaTime * 5f;
            }
        }
        
        if(controll_acess_at_start_timer >=2f)
        {
            controll_acess_at_start_timer = 2f;
            Vector2 temp_yaws = yaw_calculate();
            float mouse_cntrl = temp_yaws.x;
            mouse_cntrl =Mathf.Clamp(mouse_cntrl, -0.15f, 0.15f);
            mouse_cntrl*= Time.fixedDeltaTime * mousesensitivity;
            float mouse_cntrl_vertical = temp_yaws.y;
            mouse_cntrl_vertical = Mathf.Clamp(mouse_cntrl_vertical, -0.1f, 0.1f);
            mouse_cntrl_vertical *= Time.fixedDeltaTime * mousesensitivity;
            if (!mouse_yaw_clapmp())
            {
                float theta = Vector3.Angle(Vector3.up, transform.forward);
                if (theta > 90f)
                { theta = 180 - theta; }
                mouse_cntrl *= theta / 10f;//max angle where yaw cant perform
            }
            float pitch_ctrl = Input.GetAxis("Vertical") * rot_speed;
           
            float roll_ctrl = -Input.GetAxis("Horizontal") * rot_speed;
            transform.Rotate(pitch_ctrl-mouse_cntrl_vertical, mouse_cntrl, roll_ctrl);
            transform.position = transform.position + (transform.forward + Camera.main.transform.forward * 0.5f).normalized * Time.fixedDeltaTime * f_speed;
        }
        else
        {
            transform.position = transform.position + (transform.forward).normalized * Time.fixedDeltaTime * f_speed;//gonna change here
        }
        camera_update();
        f_speed -= transform.forward.y * Time.fixedDeltaTime * accelaration;
        speed_on_cmd();
        speed_tune_up();
        Vector3 rott = new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z);

    }
   
    void camera_update()
    {
        //camera
        if(Input.GetKey(KeyCode.C))
        {
            Vector3 cameratarget1 = transform.position + (30 * transform.forward) + 10*transform.up;//
            Camera.main.transform.position = cameratarget1;
            Camera.main.transform.LookAt(transform.position,transform.up);
        }
        if (Input.GetMouseButton(1))//fps views
        {
            is_aim_down_sight = true;
            view_timer-=Time.deltaTime;
            
            if(view_timer<=0)
            {
                view_timer = 0;
                if (Input.GetKey(KeyCode.Q))
                {
                    view_timer += 0.25f;
                    optional_view_point++;
                    if (optional_view_point > 2)
                        optional_view_point = 1;
                }
            }
           if(optional_view_point==2)//wheel_view
            {
                Vector3 cameratarget1 = transform.position + (-3 * transform.forward) - 3 * transform.up;//+transform.right*5;
                Camera.main.transform.position = cameratarget1;
                Camera.main.transform.LookAt(transform.position + transform.forward * 1000f, transform.up);
            }
           if(optional_view_point==1)//side_propview
            {
                Vector3 cameratarget1 = transform.position + (-3 * transform.forward) + 4 * transform.up+transform.right*5;
                Camera.main.transform.position = cameratarget1;
                Camera.main.transform.LookAt(transform.position + transform.forward * 1000f, transform.up);
            }
           
        }
        else
        {
            is_aim_down_sight = false;
        }
        if(!Input.GetKey(KeyCode.C) && !Input.GetMouseButton(1))
        {
            Vector3 cameratarget = transform.position - (Camera_offset_f * transform.forward) + Vector3.up * Camera_offset_up;//cng//Vector3.up
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cameratarget, ref velocity, cam_smooth_time);
            transition = Vector3.SmoothDamp(transition, transform.up, ref velocity2, cinematic_amount);//cng
            Camera.main.transform.LookAt(transform.position + transform.forward * 30f, transition);
            // i need to store the current transiton value and delver the value for next frame
        }
    }

    void speed_on_cmd()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(mouse_yaw_clapmp())//
            f_speed -= Time.fixedDeltaTime * accelaration;
        }
        if(Input.GetKey(KeyCode.LeftShift) && transform.forward.y>0f)
        {
            f_speed += Time.fixedDeltaTime* accelaration;
        }
    }
    private Vector2 yaw_calculate()
    {
        Vector2 new_delta;
        float vel = 0;
        float vel2 = 0;
        new_delta.x = Input.GetAxis("Mouse X");
        new_delta.y = Input.GetAxis("Mouse Y");
        delta.x = Mathf.SmoothDamp(delta.x, new_delta.x,ref vel, 0.2f);
        delta.y = Mathf.SmoothDamp(delta.y, new_delta.y, ref vel2, 0.2f);
        return new Vector2(delta.x, delta.y);
    }
    bool mouse_yaw_clapmp()
    {
        float Theta = Vector3.Angle(Vector3.up, transform.forward);
        if(Theta>90f)
        {
            Theta = 180 - Theta;
        }
        if(Theta<=10)//max angle where yaw cant perform
        {
            return false;
        }
        else
            return true;
    }
   bool rotation_and_grav()
    {
        float threshold_ang=10f;//
        float ang=Vector3.Angle(Vector3.up, transform.right);
        if (ang > 90+threshold_ang || ang <90-threshold_ang)
        {
            return true; 
        }
        else
            return false;
    }
    void speed_tune_up()
    {
        if (f_speed <120.0f)//30
            f_speed = 120.0f;
        if (f_speed > 250.0f)//80
            f_speed = 250.0f;
    }
    void Gravity_sim()
    {
        float displace = 0;
        if (rotation_and_grav())
        {
            roll_grav_down_time += Time.deltaTime*5;
           /* if (roll_grav_down_time > 20)
                roll_grav_down_time = 20;*/
            float factor = 200/f_speed;
            transform.position += (-Vector3.up)*roll_grav_down_time* factor * gravity;
           // transform.Rotate(2*Time.deltaTime, 0, 0);//jkasjkasdkj

        }
        else
        {
            if(roll_grav_down_time>30)
                roll_grav_down_time = 30;
            if(roll_grav_down_time>0)
            {
                roll_grav_down_time -= Time.deltaTime * 80;
                float factor = 200 / f_speed;
                transform.position += (-Vector3.up) * roll_grav_down_time * factor * gravity;
            }
               // roll_grav_down_time=0;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (time_going_down <= 0)
            {
                time_going_up += Time.fixedDeltaTime;
                // making changes
                if (time_going_up > 0.2f)
                    time_going_up = 0.2f;
                //done
                displace = gravity * time_going_up;
                transform.position = new Vector3(transform.position.x, (transform.position.y + displace), transform.position.z);
                
            }
            else
            {
                if (time_going_down > 3f)
                    time_going_down = 3f;
                time_going_down -= Time.fixedDeltaTime;
                displace = gravity * time_going_down;
                transform.position = new Vector3(transform.position.x, (transform.position.y - displace), transform.position.z);
               
            }

        }
        else
        {
            if (time_going_up <= 0)
            {
                // gona do some changes
                time_going_down += Time.fixedDeltaTime*2;
                displace = gravity * time_going_down;
                transform.position = new Vector3(transform.position.x, (transform.position.y - displace), transform.position.z);
               
            }
            else
            {
                time_going_up -= Time.fixedDeltaTime*2;//
                displace = gravity * time_going_up;
                transform.position = new Vector3(transform.position.x, (transform.position.y + displace), transform.position.z);
               
            }
        }
    }
   

}
