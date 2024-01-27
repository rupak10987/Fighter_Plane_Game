using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMPASS : MonoBehaviour
{
    public GameObject m_plane;
    private float prev_rot = 0;
    private float offset_rot;
    // Start is called before the first frame update
    void Start()
    {
        offset_rot = Vector3.Angle(new Vector3(m_plane.transform.forward.x, 0, m_plane.transform.forward.z), Vector3.right);
        prev_rot = Vector3.SignedAngle(Vector3.right, new Vector3(m_plane.transform.forward.x, 0, m_plane.transform.forward.z), Vector3.up);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z+offset_rot);
    }

    // Update is called once per frame
    void Update()
    {
        float rotation_amount = Vector3.SignedAngle(Vector3.right, new Vector3(m_plane.transform.forward.x, 0, m_plane.transform.forward.z), Vector3.up);
        float store_rot = rotation_amount;
        rotation_amount -= prev_rot;
        prev_rot = store_rot;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y , transform.eulerAngles.z + rotation_amount);
    }
}
