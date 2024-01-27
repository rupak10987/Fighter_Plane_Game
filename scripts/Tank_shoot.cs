using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_shoot : MonoBehaviour
{
    public Vector3 shot_dir;
    public float delay_btn_shots =1f;
    private float timer;
    public bool firing = false;
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        if (firing)
        {

            if (timer > delay_btn_shots)
            {
                timer = 0f;
                fire_bullet();

            }
            timer += Time.fixedDeltaTime;
        }
        timer += Time.fixedDeltaTime;
    }
    void fire_bullet()
    {
        GameObject dhuma = GameObject.Find("tank_nossel_fire");
        GameObject dhummaa = GameObject.Instantiate(dhuma);
        dhummaa.transform.position=transform.position;
        dhummaa.transform.forward = shot_dir;
        dhummaa.transform.localScale= Vector3.one*0.7f;
        Destroy(dhummaa,5f);

        GameObject finding = GameObject.Find("Tank_bullets");
        GameObject capsule = GameObject.Instantiate(finding);
        capsule.transform.forward = shot_dir;// + new Vector3(Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f), Random.Range(-0.02f, 0.02f));
        capsule.transform.position = transform.position;
        capsule.AddComponent<bullet>();
        capsule.GetComponent<bullet>().mother = this.gameObject;
        capsule.AddComponent<Damager>();
        capsule.GetComponent<Damager>().damage = 3f;
        bullet rb = capsule.GetComponent<bullet>();
        rb.speed = 1000;
        rb.xnot = capsule.transform.position.x;
        rb.ynot = capsule.transform.position.y;
        rb.znot = capsule.transform.position.z;
        Destroy(capsule, 3f);
    }
}
