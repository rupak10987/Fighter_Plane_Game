using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class texture_setting_sc : MonoBehaviour
{
    public GameObject my_plane;
    public Texture m_MainTexture;
    public Texture d_MainTexture;
    public Texture ed_MainTexture;
    public Texture transparent;
    public ParticleSystem my_smoke;
    Renderer m_Renderer;
    private float prev_health;
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        m_Renderer.material.SetTexture("_BaseMap", m_MainTexture);
        // my_smoke.enableEmission = false;
        var emission = my_smoke.emission;
        emission.enabled = false;
        prev_health =my_plane.GetComponent<Damageable>().get_current_health();
    }

    // Update is called once per frame
    void Update()
    {
            float cur_hel= my_plane.GetComponent<Damageable>().get_current_health();
            if(prev_health!=cur_hel)
            {
                texture_set();
            }
        prev_health= my_plane.GetComponent<Damageable>().get_current_health();

    }

    private void texture_set()
    {
        if (my_plane.GetComponent<Damageable>().get_current_health() <= my_plane.GetComponent<Damageable>().max_health &&
                    my_plane.GetComponent<Damageable>().get_current_health() >= my_plane.GetComponent<Damageable>().max_health * 0.66f)
        {
            m_Renderer.material.SetTexture("_BaseMap", m_MainTexture);
            //my_smoke.enableEmission = false;
            // my_smoke.emission.enabled(ture);
            var emission = my_smoke.emission;
            emission.enabled = false;   
        }
        if (my_plane.GetComponent<Damageable>().get_current_health() < my_plane.GetComponent<Damageable>().max_health * 0.66f &&
            my_plane.GetComponent<Damageable>().get_current_health() >= my_plane.GetComponent<Damageable>().max_health * 0.33f)
        {
            m_Renderer.material.SetTexture("_BaseMap", d_MainTexture);
            //my_smoke.enableEmission = true;
            var emission = my_smoke.emission;
            emission.enabled = true;
        }
        if (my_plane.GetComponent<Damageable>().get_current_health() < my_plane.GetComponent<Damageable>().max_health * 0.33f &&
            my_plane.GetComponent<Damageable>().get_current_health() >= my_plane.GetComponent<Damageable>().max_health * 0f)
        {
            m_Renderer.material.SetTexture("_BaseMap", ed_MainTexture);
            //my_smoke.enableEmission = true;
            var emission = my_smoke.emission;
            emission.enabled = true;
        }
    }

}

