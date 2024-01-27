using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollPitchYaw : MonoBehaviour
{
    public float spring_loose;
    public GameObject roll_L;
    public GameObject roll_R;
    public GameObject pitch_L;
    public GameObject pitch_R;
    public GameObject Yaw;
  
    private float yaw_conter;
    private float pitch_conter;
    private float pitch_conter2;
    private float rollcounter;
    private bool roll_left;
    private bool roll_right;
    private bool pitch_UP;
    private bool pitch_down;
    private bool pitch_UP2;
    private bool pitch_down2;
    private bool yaw_left;
    private bool yaw_right;

    private string prev_roll;
    private string prev_pitch;
    private string prev_pitch2;
    private string prev_yaw;

    
    // Start is called before the first frame update
    void Start()
    {
        yaw_conter = 0;
        pitch_conter = 0;
        pitch_conter2 = 0;
        rollcounter = 0;
        yaw_left = false;
        yaw_right = false;
        roll_left = false;
        roll_right = false;
        pitch_UP = false;
        pitch_down = false;

        pitch_down2=false;
        pitch_UP2 = false;
        prev_roll = "o";
        prev_pitch = "o";
        prev_pitch2 = "o";
        prev_yaw = "o";

    }
   
    // Update is called once per frame
    void Update()
    {
        yaw_left = false;
        yaw_right = false;
        roll_left = false;
        roll_right = false;
        pitch_UP = false;
        pitch_down = false;

        //now check
      
        roll_left = Input.GetKey(KeyCode.A);
        roll_right = Input.GetKey(KeyCode.D);
        pitch_UP2=pitch_UP = Input.GetKey(KeyCode.W);
        pitch_down2= pitch_down = Input.GetKey(KeyCode.S);
       
        YAW_get();

        rollPitchYaw_controll(ref rollcounter, ref prev_roll, ref roll_left, ref roll_right, ref roll_L, ref roll_R);
        GameObject nalan = null;
        GameObject nalan2 = null;
        GameObject nalan3 = null;
        rollPitchYaw_controll(ref pitch_conter2, ref prev_pitch2, ref pitch_UP2, ref pitch_down2, ref pitch_R, ref nalan2);
        rollPitchYaw_controll(ref pitch_conter, ref prev_pitch, ref pitch_UP, ref pitch_down, ref pitch_L, ref nalan);
        rollPitchYaw_controll(ref yaw_conter, ref prev_yaw, ref yaw_left, ref yaw_right, ref Yaw, ref nalan3);
        
    }


    private void rollPitchYaw_controll(ref float counter, ref string prev, ref bool left,ref bool right,ref GameObject L_gmobj,ref GameObject R_gmobg)//two bool, two gameobject/one game object, one counter,1str,1vector
    {
        if (right && !left)
        {
            if(prev=="l")
            {
                if (counter == 0)
                {
                    prev = "r";
                    counter += Time.deltaTime;
                    if (counter < spring_loose)
                    {
                        if(L_gmobj.name == "yaw")//starrt
                        {
                            L_gmobj.transform.RotateAround(L_gmobj.transform.forward, -Time.deltaTime * 10f);
                        }
                        else//end
                        L_gmobj.transform.RotateAround(L_gmobj.transform.right, -Time.deltaTime * 10f);
                        R_gmobg.transform.RotateAround(R_gmobg.transform.right, +Time.deltaTime * 10f);
                    }
                    if (counter > spring_loose)
                    {
                        counter = spring_loose;
                    }
                }
                else
                    right = false;
            }
            if (prev == "r" || prev=="o")
            {
                prev = "r";
                counter += Time.deltaTime;
                if (counter < spring_loose)
                {
                    if (L_gmobj.name == "yaw")//starrt
                    {
                        L_gmobj.transform.RotateAround(L_gmobj.transform.forward, -Time.deltaTime * 10f);
                    }
                    else//end
                        L_gmobj.transform.RotateAround(L_gmobj.transform.right, -Time.deltaTime * 10f);
                    R_gmobg.transform.RotateAround(R_gmobg.transform.right, +Time.deltaTime * 10f);
                }
                if (counter > spring_loose)
                {
                    counter = spring_loose;
                }
            }

        }
        if (left && !right)
        {
            if (prev == "r")
            {
                if (counter == 0)
                {
                    prev = "l";
                    counter += Time.deltaTime;
                    if (counter < spring_loose)
                    {
                        if (L_gmobj.name == "yaw")//starrt
                        {
                            L_gmobj.transform.RotateAround(L_gmobj.transform.forward, +Time.deltaTime * 10f);
                        }
                        else//end
                            L_gmobj.transform.RotateAround(L_gmobj.transform.right, +Time.deltaTime * 10f);
                        R_gmobg.transform.RotateAround(R_gmobg.transform.right, -Time.deltaTime * 10f);
                    }
                    if (counter > spring_loose)
                    {
                        counter = spring_loose;
                    }
                }
                else
                    left = false;
            }
            if (prev == "l" || prev== "o")
            {
                prev = "l";
                counter += Time.deltaTime;
                if (counter < spring_loose)
                {
                    if (L_gmobj.name == "yaw")//starrt
                    {
                        L_gmobj.transform.RotateAround(L_gmobj.transform.forward, +Time.deltaTime * 10f);
                    }
                    else//end
                        L_gmobj.transform.RotateAround(L_gmobj.transform.right, +Time.deltaTime * 10f);
                    R_gmobg.transform.RotateAround(R_gmobg.transform.right, -Time.deltaTime * 10f);
                }
                if (counter > spring_loose)
                {
                    counter = spring_loose;
                }
            }

        }
        if(left && right)
        {
            if (prev != "o")
            {
                counter -= Time.deltaTime;
                if (counter > 0)
                {
                    if (prev == "l")
                    {
                        if (L_gmobj.name == "yaw")//starrt
                        {
                            L_gmobj.transform.RotateAround(L_gmobj.transform.forward, -Time.deltaTime * 10f);
                        }
                        else//end
                            L_gmobj.transform.RotateAround(L_gmobj.transform.right, -Time.deltaTime * 10f);
                        R_gmobg.transform.RotateAround(R_gmobg.transform.right, Time.deltaTime * 10f);
                    }
                    if (prev == "r")
                    {
                        if (L_gmobj.name == "yaw")//starrt
                        {
                            L_gmobj.transform.RotateAround(L_gmobj.transform.forward, Time.deltaTime * 10f);
                        }
                        else//end
                            L_gmobj.transform.RotateAround(L_gmobj.transform.right, Time.deltaTime * 10f);
                        R_gmobg.transform.RotateAround(R_gmobg.transform.right, -Time.deltaTime * 10f);
                    }
                }
                if (counter < 0)
                {
                   counter = 0;
                    prev = "o";
                }
            }
        }
        if (!left && !right)
        {
        if (prev != "o")
            {
                counter -= Time.deltaTime;
                if (counter > 0)
                {
                    if (prev == "l")
                    {
                        if (L_gmobj.name == "yaw")//starrt
                        {
                            L_gmobj.transform.RotateAround(L_gmobj.transform.forward, -Time.deltaTime * 10f);
                        }
                        else//end
                            L_gmobj.transform.RotateAround(L_gmobj.transform.right, -Time.deltaTime * 10f);
                        R_gmobg.transform.RotateAround(R_gmobg.transform.right, Time.deltaTime * 10f);
                    }
                    if (prev == "r")
                    {
                        if (L_gmobj.name == "yaw")//starrt
                        {
                            L_gmobj.transform.RotateAround(L_gmobj.transform.forward, Time.deltaTime * 10f);
                        }
                        else//end
                            L_gmobj.transform.RotateAround(L_gmobj.transform.right, Time.deltaTime * 10f);
                        R_gmobg.transform.RotateAround(R_gmobg.transform.right, -Time.deltaTime * 10f);
                    }
                }
                if (counter < 0)
                {
                    counter = 0;
                    prev = "o";
                }     
            }
        }
    }

    public void YAW_get()
    {
        float ms = (Input.mousePosition.x - (Display.main.systemWidth/2f)) / (Display.main.systemWidth);
        if(ms > 0)
        {
            yaw_right = true;
            yaw_left = false;

        }
        if(ms<0)
        {
            yaw_left = true;
            yaw_right = false;
        }
        if(ms==0)
        {
            yaw_left = false;
            yaw_right = false;
        }

    }

}
