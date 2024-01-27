using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class camera_movement : MonoBehaviour
{
    public GameObject mplane;
    private float angles;
    public float rot_speed;

    public Vector3 delta = Vector3.zero;
 private Vector3 lastPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        angles = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
        angles = mouse_bullshits().x;
        if(angles>1f)
        {
            angles-=10f*Time.deltaTime;
        }
        if (angles < -1f)
        {
            angles += 10f * Time.deltaTime;
        }
        gameObject.transform.RotateAround(mplane.transform.position, Vector3.up, angles* Time.deltaTime * rot_speed);
        gameObject.transform.LookAt(mplane.transform.position);
    }
    Vector2 mouse_bullshits()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            delta = Input.mousePosition - lastPos;
            lastPos = Input.mousePosition;
        }
        return new Vector2(delta.x, delta.y);
    }
}
