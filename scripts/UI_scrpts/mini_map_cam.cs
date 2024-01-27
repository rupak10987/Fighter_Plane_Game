using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mini_map_cam : MonoBehaviour
{
    public GameObject m_plane;
    private float prev_rot=0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(m_plane.transform.position.x,100, m_plane.transform.position.z);
        prev_rot = Vector3.SignedAngle(Vector3.right, new Vector3(m_plane.transform.forward.x, 0, m_plane.transform.forward.z), Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_plane.transform.position.x,200, m_plane.transform.position.z);
        float rotation_amount = Vector3.SignedAngle(Vector3.right, new Vector3(m_plane.transform.forward.x, 0, m_plane.transform.forward.z),Vector3.up);
       float store_rot = rotation_amount;
        rotation_amount -= prev_rot;
        prev_rot = store_rot;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotation_amount, transform.eulerAngles.z);
    }
}