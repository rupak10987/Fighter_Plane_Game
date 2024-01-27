using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//using UnityEngine.Rendering.PostProcessing;


public class post_processing_speed : MonoBehaviour
{ 
    public GameObject m_plane;
    private float speed;
    public Volume vlm;
    Bloom m_bloom;
    MotionBlur m_motionBlur;
   // DepthOfField m_depthOfField;
    ChromaticAberration m_chromaticAberration;
   
    // Start is called before the first frame update
    void Start()
    {
        speed = m_plane.GetComponent<plane_controll>().f_speed;

        vlm.profile.TryGet<Bloom>(out m_bloom);
        m_bloom.intensity.value = 1.5f;
        vlm.profile.TryGet<MotionBlur>(out m_motionBlur);
        m_motionBlur.intensity.value = 0;
        vlm.profile.TryGet<ChromaticAberration>(out m_chromaticAberration);
        m_chromaticAberration.intensity.value = 0;
        //
       /* vlm.profile.TryGet<DepthOfField>(out m_depthOfField);*/
    }

    // Update is called once per frame
    private void Update()
    {
      
        speed = m_plane.GetComponent<plane_controll>().f_speed;
        if (speed >= 150)
        {
            if (dive_or_not())
            {
               
                m_motionBlur.intensity.value +=Time.deltaTime*0.70f;
                m_motionBlur.intensity.value= Mathf.Clamp01(m_motionBlur.intensity.value);
                m_chromaticAberration.intensity.value += Time.deltaTime * 0.70f;
                m_chromaticAberration.intensity.value = Mathf.Clamp01(m_chromaticAberration.intensity.value);
            }
            else
            {
                m_motionBlur.intensity.value -= Time.deltaTime * 0.40f;
                m_motionBlur.intensity.value = Mathf.Clamp01(m_motionBlur.intensity.value);
                m_chromaticAberration.intensity.value -= Time.deltaTime * 0.40f;
                m_chromaticAberration.intensity.value = Mathf.Clamp01(m_chromaticAberration.intensity.value);
            }     
        }
        else
        {
            m_motionBlur.intensity.value -= Time.deltaTime;
            m_motionBlur.intensity.value = Mathf.Clamp01(m_motionBlur.intensity.value);
            m_chromaticAberration.intensity.value -= Time.deltaTime;
            m_chromaticAberration.intensity.value = Mathf.Clamp01(m_chromaticAberration.intensity.value);
        }
    }
    bool dive_or_not()
    {
        float Theta = Vector3.Angle(-Vector3.up, m_plane.transform.forward);
      
        if (Theta <= 60)//max angle where yaw cant perform
        {
            return true;
        }
        else
            return false;
    }
    private void OnDisable()
    {
        
    }
    private void OnDestroy()
    {
        
    }
}
