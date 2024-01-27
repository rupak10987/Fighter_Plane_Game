using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;//initialized from  owner
    public float gravity=80.8f;
    public float elapsed_time = 0f;
    private float vZnot;
    private float vYnot;
    private float vXnot;
    public float ynot;//initialized from  owner
    public float znot;//initialized from  owner
    public float xnot;//initialized from  owner
    public bool done = false;
    public GameObject mother;
    // Start is called before the first frame update
    void Start()
    {
        vZnot = speed * transform.forward.z;
        vXnot = speed * transform.forward.x;
        vYnot = speed * transform.forward.y;
    }

    // Update is called once per frame
     void FixedUpdate()
    {
        if(!done)
        {
            elapsed_time += Time.fixedDeltaTime;
            transform.position = Calculate_point_on_parabola(elapsed_time);
            Vector3 next_point = Calculate_point_on_parabola(elapsed_time + Time.fixedDeltaTime);
            //test
            transform.forward = next_point - transform.position;
            do_ray(next_point);
        }
        else
        {
            Destroy(gameObject);//
        }
    }
    void ExplosionDamage(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            gameObject.GetComponent<Damager>().Damage(hitCollider.gameObject.GetComponent<Damageable>());
        }
    }
   public virtual void do_ray(Vector3 point2)
    {
        RaycastHit hit;
            if(mother.layer==6)
            if (Physics.Raycast(transform.position, (point2 - transform.position).normalized, out hit, Vector3.Magnitude(transform.position - point2)))
            {
                if (gameObject.name == "bullets(Clone)")

                {
                    //damaging bro
                    GameObject find = GameObject.Find("bullet_impact_particle");
                    GameObject effect = GameObject.Instantiate(find);


                    effect.transform.position = hit.point;
                    effect.transform.forward = hit.normal;
                    effect.transform.localScale = new Vector3(1f, 1f, 1f);
                    effect.transform.localScale = Vector3.one * 0.2f;
                    done = true;
                    Destroy(effect, 2f);
                    gameObject.GetComponent<Damager>().Damage(hit.collider.gameObject.GetComponent<Damageable>());
                }
                else //if (gameObject.name == "torpedo(Clone)")
                {
                    GameObject find = GameObject.Find("bomb_impact_particle");
                    GameObject effect = GameObject.Instantiate(find);
                    effect.transform.position = hit.point;
                    effect.transform.forward = hit.normal;
                    effect.transform.localScale = new Vector3(1f, 1f, 1f);
                    effect.transform.localScale = Vector3.one * 3f;
                    done = true;
                    Destroy(effect, 2f);
                     explosion();
                    //explosion_fix();
                    //gameObject.GetComponent<Damager>().Damage(hit.collider.gameObject.GetComponent<Damageable>());
                }

            }
            if(mother.layer==0)
            {
            if (Physics.Raycast(transform.position, (point2 - transform.position).normalized, out hit, Vector3.Magnitude(transform.position - point2)))
            {
                if (gameObject.name == "bullets(Clone)" )

                {
                    //damaging bro
                    GameObject find = GameObject.Find("bullet_impact_particle");
                    GameObject effect = GameObject.Instantiate(find);


                    effect.transform.position = hit.point;
                    effect.transform.forward = hit.normal;
                    effect.transform.localScale = new Vector3(1f, 1f, 1f);
                    effect.transform.localScale = Vector3.one * 0.2f;
                    done = true;
                    Destroy(effect, 2f);
                    if(hit.collider.gameObject.layer==6)
                    gameObject.GetComponent<Damager>().Damage(hit.collider.gameObject.GetComponent<Damageable>());
                }

                if(gameObject.name == "Tank_bullets(Clone)")
                {
                    GameObject find = GameObject.Find("tank_bull_impact");
                    GameObject effect = GameObject.Instantiate(find);


                    effect.transform.position = hit.point;
                    effect.transform.forward = hit.normal;
                   
                    effect.transform.localScale = Vector3.one *2f;
                    done = true;
                    Destroy(effect, 1f);
                    if (hit.collider.gameObject.layer == 6)
                        gameObject.GetComponent<Damager>().Damage(hit.collider.gameObject.GetComponent<Damageable>());
                }
            }
            }
            



    }
    private void explosion()
    {
        Collider[] hitColliders_=new Collider[100];
        hitColliders_.Initialize();
        hitColliders_ = Physics.OverlapSphere(transform.position, 50.51f,3, QueryTriggerInteraction.UseGlobal);
        /*RaycastHit[] hitColliders_;
        hitColliders_ = Physics.SphereCastAll(transform.position, 200,Vector3.up, Mathf.Infinity,3, QueryTriggerInteraction.UseGlobal);*/
      
        foreach (Collider hitCollider in hitColliders_)
        {
            gameObject.GetComponent<Damager>().Damage(hitCollider.gameObject.GetComponent<Damageable>());
        }
    }
    private void explosion_fix()
    {
        RaycastHit[] hits=new RaycastHit[100];
        hits = Physics.SphereCastAll(transform.position, 100, Vector3.up, Mathf.Infinity, 3, QueryTriggerInteraction.UseGlobal);
        foreach (RaycastHit hit in hits)
        {
            hit.collider.gameObject.GetComponent<Damager>().Damage(hit.collider.gameObject.GetComponent<Damageable>());
        }
        //hits.c
    }
    Vector3 Calculate_point_on_parabola(float e_time)
    {
        float y1 = ynot + (vYnot * e_time) - (0.5f *gravity* Mathf.Pow(e_time, 2f)); //new position
        float z1 = znot + (vZnot * e_time);
        float x1 = xnot + (vXnot * e_time);
        return new Vector3(x1, y1, z1);
    }
}

