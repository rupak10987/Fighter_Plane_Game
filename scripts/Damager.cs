using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    public float damage=1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
   public void Damage(Damageable damageable)
    {
        damageable.Damage(damage);
        damageable.set_death_type(true);
    }
    public void KILL_Damage(Damageable damageable)
    {
        damageable.Damage(damageable.max_health);
        damageable.set_death_type(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
