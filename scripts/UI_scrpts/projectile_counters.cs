using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class projectile_counters : MonoBehaviour
{
    public Text bulls;
    public GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        /*bulls.text =plane.GetComponent<shoot>().get_current_maz_size().ToString()+"/"+ plane.GetComponent<shoot>().get_current_storage().ToString();*/
    }

    // Update is called once per frame
    void Update()
    {
        bulls.text = plane.GetComponent<shoot>().get_current_maz_size().ToString() + "/" + plane.GetComponent<shoot>().get_current_storage().ToString();
    }
}
