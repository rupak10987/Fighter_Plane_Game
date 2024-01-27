using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alti_meter : MonoBehaviour
{
    [SerializeField] private GameObject m_plane;
    [SerializeField] private Image img_rotating_and_sliding;
    [SerializeField] private Image img_rotating;
    private float prev_rot;
    private float prev_rot_extra;
    private float prev_slide;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotation_amount = Vector3.SignedAngle(Vector3.Cross(m_plane.transform.forward, Vector3.up), m_plane.transform.right, m_plane.transform.forward);
       
            float store_rot = rotation_amount;
            rotation_amount -= prev_rot;
            prev_rot = store_rot;
 
      //img_rotating_and_sliding.rectTransform.localEulerAngles = new Vector3(img_rotating_and_sliding.rectTransform.localEulerAngles.x, img_rotating_and_sliding.rectTransform.localEulerAngles.y, img_rotating_and_sliding.rectTransform.localEulerAngles.z -rotation_amount);
        img_rotating.rectTransform.localEulerAngles = new Vector3(img_rotating.rectTransform.localEulerAngles.x, img_rotating.rectTransform.localEulerAngles.y, img_rotating.rectTransform.localEulerAngles.z - rotation_amount);
        rot_slide();    
    }
   private void rot_slide()
    {
      float rot=Vector3.Angle(-Vector3.up, m_plane.transform.forward);
        rot=Mathf.Cos(rot*(3.1416f/180));
        rot *= 500;
        img_rotating_and_sliding.rectTransform.localPosition = new Vector3(img_rotating_and_sliding.rectTransform.localPosition.x, rot, img_rotating_and_sliding.rectTransform.localPosition.z);
    }
}

