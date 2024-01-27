using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_shoot : MonoBehaviour
{
    public Vector3 shot_dir;
    public float delay_btn_shots = 0.02f;
    private float timer;
    public int mag_size = 20;
    public bool firing=false;
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
        GameObject finding = GameObject.Find("bullets");
        GameObject capsule = GameObject.Instantiate(finding);
        capsule.transform.forward = shot_dir+ new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f));
        capsule.transform.position = transform.position;
        capsule.AddComponent<aa_bullets>();//aa
        capsule.GetComponent<aa_bullets>().mother = this.gameObject;//aa
        capsule.AddComponent<Damager>();
        aa_bullets rb = capsule.GetComponent<aa_bullets>();//aa
        rb.speed = 800;
        rb.xnot = capsule.transform.position.x;
        rb.ynot = capsule.transform.position.y;
        rb.znot = capsule.transform.position.z;
        Destroy(capsule, 3f);
    }
}
