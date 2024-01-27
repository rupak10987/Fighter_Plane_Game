using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    public float max_health;
    private float current_health;
    private float plane_d_timer;
    public  bool ami_morte_jassi;
    public bool ami_more_gesi;
    private bool death_by_projectile;
    private float time_to_die = 20f;
    public bool take_damage=true;
    [SerializeField] private int death_sprite_number;
    [SerializeField] private int death_xp_amount;
    [SerializeField] private string death_text;
   /* private void OnEnable()
    {
        current_health = max_health;
        take_damage = true;
}*/
    private void Awake()
    {
        current_health = max_health;
        take_damage = true;
    }
    void Start()
    {
        current_health = max_health;
        take_damage = true;
        plane_d_timer = time_to_die;
        ami_morte_jassi = false;
        ami_more_gesi = false;
       
    }
    public void Damage(float dam)
    {
        if(take_damage)
        {
            if (gameObject.GetComponent<camera_shake>() != null)
            {
                gameObject.GetComponent<camera_shake>().do_shake();
            }
            current_health -= dam;
            if (current_health <= 0)
            {
                take_damage = false;
                die();
            }
        }  
    }
    public void repair()
    {
        current_health += 0.2f * max_health;
        if (current_health >= max_health)
        {
            current_health=max_health;
        }
    }
    private void die()
    {
        
        
        if(gameObject.name!="plane main_Main")
        {
            //scoring
             Debug.Log("death");
             GameObject canvass = GetChildWithName(GameObject.Find("plane main_Main"), "Canvas");
             canvass.GetComponent<scorehandler>().call_once_for_score(death_sprite_number,death_xp_amount,death_text);
            
            Debug.Log("somossa ase");
            //end scoring
            GameObject find = GameObject.Find("bomb_impact_particle");
            GameObject effect = GameObject.Instantiate(find);
            effect.transform.position = transform.position;
            effect.transform.localScale = new Vector3(1f, 1f, 1f);
            effect.transform.localScale = Vector3.one * 3f;
            effect.transform.forward = Vector3.up;
            Destroy(effect, 2f);
            Destroy(gameObject);
        }
        else
        {
            GameObject find = GameObject.Find("bomb_impact_particle");
            GameObject effect = GameObject.Instantiate(find);
            effect.transform.position = transform.position;
            effect.transform.localScale = new Vector3(1f, 1f, 1f);
            effect.transform.localScale = Vector3.one * 1f;
            effect.transform.forward = Vector3.up;
            Destroy(effect, 0.5f);

            if(death_by_projectile)
            {
                time_to_die = 50;
                plane_d_timer = time_to_die;
                plane_d_timer -= 1;
                gameObject.GetComponent<plane_crash>().crushing = true;
                gameObject.GetComponent<plane_crash>().set_speed(gameObject.GetComponent<plane_controll>().f_speed);
            }
            else
            {
                gameObject.GetComponent<plane_crash>().crushing = false;
                time_to_die = 20;
                plane_d_timer=time_to_die;
                plane_d_timer -= 1;
            }
            gameObject.GetComponent<plane_controll>().enabled = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
       if(plane_d_timer<time_to_die)
        {
            plane_d_timer -= 10 * Time.deltaTime;
            if(plane_d_timer<0)
            ami_morte_jassi = true;
        }
       if(ami_more_gesi==true)
        {
            Destroy(gameObject);
        }
    }
    public void set_death_type(bool projectile_death)
    {
        death_by_projectile = projectile_death;
    }
    public float get_current_health()
    {
        return current_health;
    }
    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

}
