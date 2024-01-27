using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class speedo_meter : MonoBehaviour
{
    [SerializeField] private Image dec0;
    [SerializeField] private Image dec1;
    [SerializeField] private Image dec2;
    [SerializeField] private GameObject m_plane;
    private float[] arr;
    private float[] f;
    
    // Start is called before the first frame update
    void Start()
    {
        arr = new float[3];
        f = new float[3];
        for(int i=0; i<3; i++)
        {
            arr[i] = 450 ;
            f[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        int number = Mathf.FloorToInt(m_plane.GetComponent<plane_controll>().f_speed);
        int d;
        int i = 0;
        while(number!=0)
        {
           
            d = number % 10;
            number = number / 10;
            //smooth
            arr[i] = Mathf.SmoothDamp(arr[i], (450 - (d * 100)),ref f[i],0.2f);//450-(d*100);
            i++;
        }
        dec0.rectTransform.localPosition = new Vector3(dec0.rectTransform.localPosition.x, arr[0], dec0.rectTransform.localPosition.z);
        dec1.rectTransform.localPosition = new Vector3(dec1.rectTransform.localPosition.x, arr[1], dec1.rectTransform.localPosition.z);
        dec2.rectTransform.localPosition = new Vector3(dec2.rectTransform.localPosition.x, arr[2], dec0.rectTransform.localPosition.z);
    }
}
