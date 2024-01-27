using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class release_bomb : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay_btn_bombs = 2f;
    private float timer;
    public int mag_size = 4;
    public GameObject[] boms;
    private Hashtable mhashtable_for_boms;
    public enum bom_type
    {
        fat_bom,
        lomba_bom,
        glider_bom
    }
    public bom_type m_bom_type = bom_type.fat_bom;
    void Start()
    {
        mhashtable_for_boms = new Hashtable();
        mhashtable_for_boms.Add(bom_type.fat_bom, boms[0]);
        mhashtable_for_boms.Add(bom_type.lomba_bom, boms[0]);
        mhashtable_for_boms.Add(bom_type.glider_bom, boms[0]);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool bombing = Input.GetKey(KeyCode.E);
        if (bombing)
        {
            if (timer > delay_btn_bombs)
            {
                timer = 0f;
                relez_mother_f_bomb();
            }
            timer += Time.fixedDeltaTime;
        }
        timer += Time.fixedDeltaTime;
    }
    void relez_mother_f_bomb()
    {
       // GameObject finding = GameObject.Find("torpedo");
        GameObject littleboy = GameObject.Instantiate(boms[0]);
        littleboy.transform.forward = transform.forward.normalized;
        littleboy.transform.position = transform.position-transform.up*5f;
        littleboy.transform.localScale=new Vector3(200, 200, 200);
       
        littleboy.AddComponent<bullet>();
        littleboy.AddComponent<Damager>();
        littleboy.GetComponent<Damager>().damage = 5f;
        littleboy.GetComponent<bullet>().mother = this.gameObject;
        bullet rb = littleboy.GetComponent<bullet>();
        //getting the speed of the plane
        plane_controll plane_speed = GetComponent<plane_controll>();
        rb.speed = plane_speed.f_speed;
        rb.gravity = 70;
        rb.xnot = littleboy.transform.position.x;
        rb.ynot = littleboy.transform.position.y;
        rb.znot = littleboy.transform.position.z;
        Destroy(littleboy, 20f);
    }
}
