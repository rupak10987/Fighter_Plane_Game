using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class start_screen_plane_preview : MonoBehaviour
{
    public GameObject[] amar_planes;
    private int curr_plane=0;
    private GameObject instance;
    private void Awake()
    {
        curr_plane=PlayerPrefs.GetInt("active_plane_index", 0);
        instance = GameObject.Instantiate(amar_planes[curr_plane], this.gameObject.transform,true);
        instance.SetActive(true);
    }

    public void next_plane_show()
    {
        deactivator();
        curr_plane++;
        curr_plane=curr_plane%amar_planes.Length;
        activator();
        PlayerPrefs.SetInt("active_plane_index",curr_plane);
        
    }

    public void prev_plane_show()
    {
        deactivator();
        curr_plane--;
        if(curr_plane<0)
            curr_plane=amar_planes.Length-1;
        curr_plane = curr_plane % amar_planes.Length;
        activator();
        PlayerPrefs.SetInt("active_plane_index", curr_plane);

    }
    void activator()
    {
        instance = GameObject.Instantiate(amar_planes[curr_plane], this.gameObject.transform,true);
        instance.SetActive(true);
    }
    void deactivator()
    {
        DestroyObject(instance);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            prev_plane_show();
           
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            next_plane_show();
        }
    }
}
