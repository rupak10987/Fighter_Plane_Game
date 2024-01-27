using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repair_script : MonoBehaviour
{
    public GameObject plane;
    [SerializeField] private float diameter;
    private float repair_again_timer = 0;
    // Start is called before the first frame update
    void Start()
    {
     
    }
    // Update is called once per frame
    void Update()
    {
     float dist=Vector3.Magnitude(transform.position-plane.transform.position);
        if (dist <= diameter && repair_again_timer>=0.5f)
        {
            plane.GetComponent<Damageable>().repair();
            repair_again_timer = 0;
            plane.GetComponent<shoot>().refill();
        }
        repair_again_timer+=Time.deltaTime;
        
    }
}
