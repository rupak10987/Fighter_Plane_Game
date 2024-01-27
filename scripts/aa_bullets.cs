using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aa_bullets : bullet
{
    public override void do_ray(Vector3 point2)
    {
        base.do_ray(point2);
        float random_height = Random.Range(20f, 60f);
        GameObject plane = GameObject.Find("plane main_Main");
        if(going_up())
        {
            if(transform.position.y-plane.transform.position.y>random_height)
            {
                //nstance the bull shit smoke effect
                GameObject find = GameObject.Find("tank_bull_impact");
                GameObject effect = GameObject.Instantiate(find);
                effect.transform.position=transform.position;
                Destroy(effect, 1.5f);
                Destroy(this.gameObject);
                
            }
        }
        else
        {
            if (transform.position.y - plane.transform.position.y < random_height)
            {
                //nstance the bull shit smoke effect
                GameObject find = GameObject.Find("tank_bull_impact");
                GameObject effect = GameObject.Instantiate(find);
                effect.transform.position = transform.position;
                Destroy(effect, 1.5f);
                Destroy(this.gameObject);
            }
        }

    }
    private bool going_up()
    {
        float Theta = Vector3.Angle(-Vector3.up, transform.forward);

        if (Theta >90 )//max angle where yaw cant perform
        {
            return true;
        }
        else
            return false;
    }
}
