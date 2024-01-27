using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class always_rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float rot_speed;
   public enum axis
    {forward, up, right }
    public axis rotate_axis_;
    private Vector3 m_axis;
    private void Awake()
    {
        if (rotate_axis_==axis.forward)
        {
            m_axis = transform.forward;
        }
        else if(rotate_axis_==axis.up)
        {
            m_axis= transform.up;
        }
        else if(rotate_axis_!=axis.right)
        {
            m_axis=-transform.right;
        }
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.RotateAround(m_axis, rot_speed*Time.deltaTime);
    }
}
