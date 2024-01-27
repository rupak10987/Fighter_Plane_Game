using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dog : MonoBehaviour
{
    private float rotation_amount;
    public float re_calculate_timer = 1;
    public Vector3 chase_p;
    enum state
    {
        patrol,
        chased
    };
    state m_state;
    // Start is called before the first frame update
    void Start()
    {
        re_calculate_timer = 1;
        m_state = state.patrol;
        chase_p = transform.forward * 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_state == state.patrol)
        {
            re_calculate_timer-=Time.deltaTime;
            if (re_calculate_timer < 0)
            {
                chase_p = calc_chase_point();
                re_calculate_timer = 1f;
                rotation_amount = Vector3.SignedAngle(transform.forward, chase_p, Vector3.up);
                rotation_amount/=Mathf.Abs(rotation_amount);
            }
            //rotation_amount -= Time.deltaTime;
            transform.Rotate(0, rotation_amount*1.0f, rotation_amount);
            transform.position += transform.forward*Time.deltaTime*150;
        }
    }
    Vector3 calc_chase_point()
    {
        Vector3 fwd=transform.forward;
        fwd *= 50;
        fwd +=new Vector3 (Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f));
        return fwd;
    }
}
