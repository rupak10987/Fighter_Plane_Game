using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public  float delay_btn_shots=0.02f;
    private float timer;
    private Vector3 cam_offset=Vector3.one;
    public Camera cam;
    private GameObject gun_point_l;
    private GameObject gun_point_r;
    public float gun_damage;
    public AudioSource gun_audio;

    //mags
    public int max_mag_size=30;
    private int mag_size;
    public int max_bullets_storage_size=60;
    private int bullets_storage_size;
    public float reload_time=2;
    private float reload_timer=0;
    void Start()
    {
        gun_audio = GetComponent<AudioSource>();
        if(GameObject.Find("airplane_gun_L")!=null)
        gun_point_l = GameObject.Find("airplane_gun_L");
        if(GameObject.Find("airplane_gun_R")!=null)
        gun_point_r = GameObject.Find("airplane_gun_R");
    }
    private void Awake()
    {
        bullets_storage_size = max_bullets_storage_size;
        mag_size = max_mag_size;
        reload_timer = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        bool firing=false;
        if (magazine_calculation())
        {
            
                firing = Input.GetButton("Fire1");
           
        }
        else
        {
            reload_timer -= Time.fixedDeltaTime;
            if(reload_timer<=0)
            {
                if(bullets_storage_size>max_mag_size)
                {
                    mag_size = max_mag_size;
                    bullets_storage_size -= max_mag_size;
                }
                else if(bullets_storage_size>0)
                {
                    mag_size = bullets_storage_size;
                    bullets_storage_size -= bullets_storage_size;
                }
                reload_timer = 0;
            }
        }
        if (firing)
        {
          
            if (timer>delay_btn_shots)
            {
                //bullets_storage_size--;
                mag_size--;
                play_Audio(gun_audio);
                timer = 0f;
                cam_shake();
                fire_bullet(gun_point_l);//change
                fire_bullet(gun_point_r);
                
            }
            timer += Time.fixedDeltaTime;
        }
        timer += Time.fixedDeltaTime;
    }
    void fire_bullet(GameObject gunpoint)
    {
        GameObject finding = GameObject.Find("bullets");
        GameObject capsule = GameObject.Instantiate(finding);
        capsule.transform.position =gunpoint.transform.position-20*gunpoint.transform.up;//change
        capsule.transform.forward = calculate_dir(gunpoint) - 0.05f*(transform.up)+new Vector3(Random.Range(-20f,20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
        capsule.AddComponent<bullet>();
        capsule.AddComponent<Damager>();
        capsule.GetComponent<bullet>().mother = this.gameObject;
        capsule.GetComponent<Damager>().damage = gun_damage;
        bullet rb= capsule.GetComponent<bullet>();
        rb.speed = 1000;
        rb.xnot = capsule.transform.position.x;
        rb.ynot = capsule.transform.position.y;
        rb.znot = capsule.transform.position.z;
        Destroy(capsule,3f);
    }
    void cam_shake()
    {
        Vector3 vel = Vector3.zero;
        cam_offset = Camera.main.transform.position+5f*Camera.main.transform.forward;
        Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, cam_offset, ref vel, 0.09f);
       ///cam_offset = Vector3.SmoothDamp( cam_offset,Vector3.one, ref vel, 0.1f);
    }
    void play_Audio(AudioSource maudio)
    {
        maudio.Play();
      
    }
    private bool magazine_calculation()
    {
       
        if(bullets_storage_size>0)
        {
            if(mag_size>0)
            {
                return true;
            }
            else
            {
             if(reload_timer<=0)   
             reload_timer = reload_time;
             return false;
            }
                
        }
        else
        {
            if(mag_size > 0)
            {
                return true;
            }
            return false;
        }
       

    }
    public void refill()
    {
        bullets_storage_size = max_bullets_storage_size;
        mag_size= max_mag_size;
    }
    public int get_current_maz_size()
    {
        return mag_size;
    }
    public int get_current_storage()
    {
        return bullets_storage_size;
    }
    private bool manual_reload()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            reload_timer = reload_time;
            return true;
        }
        return false;
    }
    private Vector3 calculate_dir(GameObject gunpoint)
    {
        RaycastHit hit;
       

       if (Physics.Raycast(cam.transform.position,cam.transform.forward,out hit,1000,6))
        {
            Vector3 dir = hit.point - gunpoint.transform.position;//change
            return dir;
        }
        
        {
            Vector3 idir = cam.transform.position + cam.transform.forward * 1000;
            return idir- gunpoint.transform.position;//change
        }
    }
}
