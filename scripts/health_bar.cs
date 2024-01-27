using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class health_bar : MonoBehaviour
{
    public Slider slider;
    public GameObject Object;
    public Gradient gradient;
    public Image fill_img;
    public void set_health()
    {
        slider.value = Object.GetComponent<Damageable>().get_current_health();
        fill_img.color = gradient.Evaluate(slider.normalizedValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = Object.GetComponent<Damageable>().max_health;
        fill_img.color = gradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        set_health();
    }
}
