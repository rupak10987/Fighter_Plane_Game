using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefab_loader_at_start : MonoBehaviour
{
    public GameObject[] amar_planes;
    
    void Awake()
    {
        //..instancing and making parent
        GameObject pp = GameObject.Instantiate(amar_planes[PlayerPrefs.GetInt("active_plane_index",0)], this.transform);

        //..setting the textures and main plane
        pp.GetComponent<texture_setting_sc>().my_plane = this.gameObject;

        //..propeller
        GameObject rr = GetChildWithName(pp, "propeller_");
        rr.GetComponent<rotor>().obj = this.gameObject;

        GameObject rr1 = GetChildWithName(pp, "propeller_1");
        if(rr1!=null)
        {
            rr1.GetComponent<rotor>().obj = this.gameObject;
        }
        //..setting the smoke
        ParticleSystem smoke = GetChildWithName(this.gameObject, "smoke_damage").GetComponent<ParticleSystem>();
        if (smoke != null)
            pp.GetComponent<texture_setting_sc>().my_smoke = smoke;
        else
            Debug.Log("amar putki te dhoa nai");

        //roll pitch and yaw;
        this.gameObject.AddComponent<RollPitchYaw>();
        this.gameObject.GetComponent<RollPitchYaw>().spring_loose = 0.1f;
        this.gameObject.GetComponent<RollPitchYaw>().roll_L = GetChildWithName(pp, "roll_l");
        this.gameObject.GetComponent<RollPitchYaw>().roll_R = GetChildWithName(pp, "roll_r");
        this.gameObject.GetComponent<RollPitchYaw>().pitch_L = GetChildWithName(pp, "pitch_l");
        this.gameObject.GetComponent<RollPitchYaw>().pitch_R = GetChildWithName(pp, "pitch_r");
        this.gameObject.GetComponent<RollPitchYaw>().Yaw = GetChildWithName(pp, "yaw");


        // trail effects for the wings
        GameObject trail_L_S = GetChildWithName(pp, "trail_L");
        GameObject trail_R_S = GetChildWithName(pp, "trail_R");
        trail_L_S.GetComponent<trail_script>().o = this.gameObject;
        trail_R_S.GetComponent<trail_script>().o = this.gameObject;


        //hehe
       // this.gameObject.AddComponent<Json_reader_plane>();
        Json_reader_plane plane_list_mofo = this.gameObject.GetComponent<Json_reader_plane>();
        Json_reader_plane.plane_list nigge = plane_list_mofo.myplanes;
        foreach (var plane_ in nigge.planes)
        {
            if(plane_.id== PlayerPrefs.GetInt("active_plane_index", 0))
            {
                this.gameObject.GetComponent<shoot>().delay_btn_shots = plane_.fire_rate;
                this.gameObject.GetComponent<shoot>().gun_damage = plane_.Damage;
                this.gameObject.transform.localScale = new Vector3(plane_.size, plane_.size, plane_.size);
                this.gameObject.GetComponent<Damageable>().max_health = plane_.max_health;
                break;
            }
        }
        //hehe
    }
    // Update is called once per frame
    void Update()
    {
        
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
