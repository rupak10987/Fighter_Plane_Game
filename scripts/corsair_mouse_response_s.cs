using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class corsair_mouse_response_s : MonoBehaviour
{
    [SerializeField] private Image bar0;
    [SerializeField] private Image bar1;
    // Start is called before the first frame update
    Vector2 center;
    public GameObject childs;
    private Vector2 delta=Vector2.zero;
    void Start()
    {
        center = new Vector2(Display.displays.GetLength(1)/2, Display.displays.GetLength(2) / 2);
        this.GetComponent<RectTransform>().localPosition = center;
        bar0.enabled = false;
        bar1.enabled = false;
    }
    void Update()
    {
        Vector2 local_delta = yaw_calculate();
       this.GetComponent<RectTransform>().localPosition = center+new Vector2(local_delta.x*Display.main.systemWidth/2, local_delta.y * Display.main.systemHeight / 2);
       this.GetComponent<RectTransform>().eulerAngles=new Vector3 (0,0,Mathf.Atan2(local_delta.y,local_delta.x)*(180/3.14159f));

        //distamce of this ob from center
        Vector2 dist = center - new Vector2(this.GetComponent<RectTransform>().localPosition.x, this.GetComponent<RectTransform>().localPosition.y);
        if (local_delta.magnitude>=0.4f)
        {
            bar0.enabled = true;
            bar0.rectTransform.localPosition = new Vector3(/*-local_delta.magnitude * (Display.main.systemHeight / 8)*/-dist.magnitude/8, bar0.rectTransform.localPosition.y,  bar0.rectTransform.localPosition.z);
            if(local_delta.magnitude>=0.7f)
            {
                bar1.enabled = true;
                bar1.rectTransform.localPosition = new Vector3(/*-local_delta.magnitude * (Display.main.systemHeight / 6)*/-dist.magnitude / 6, bar1.rectTransform.localPosition.y, bar1.rectTransform.localPosition.z);
            }
            else
            {
                bar1.enabled=false;
            }
        }
        else
        {
            bar0.enabled=false;
            bar1.enabled=false;
        }

    }
    private Vector2 yaw_calculate()
    {
        Vector2 new_delta=new Vector2();
        float vel = 0;
        float vel2 = 0;
        new_delta.x = Input.GetAxis("Mouse X");
        new_delta.y = Input.GetAxis("Mouse Y");
        delta.x = Mathf.SmoothDamp(delta.x, new_delta.x, ref vel, 0.11f);
        delta.y = Mathf.SmoothDamp(delta.y, new_delta.y, ref vel2, 0.11f);

        return new Vector2(delta.x, delta.y);
    }
}
