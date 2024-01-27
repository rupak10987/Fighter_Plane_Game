using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prev_and_next_arrow : MonoBehaviour
{
    public GameObject brain;
    // Start is called before the first frame update
    public void left_action()
    {
        brain.GetComponent<start_screen_plane_preview>().prev_plane_show();
    }
    public void right_action()
    {
        brain.GetComponent<start_screen_plane_preview>().next_plane_show();
    }
}
