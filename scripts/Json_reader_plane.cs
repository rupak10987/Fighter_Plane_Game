using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Json_reader_plane : MonoBehaviour
{
    public TextAsset plane_data;
    [System.Serializable]
    public class Plane
    {
        public int id;
        public string name;
        public int size;
        public float Damage;
        public float fire_rate;
        public float max_health;
    }
    [System.Serializable]
    public class plane_list
    {
        public Plane[] planes;
    }
    public plane_list myplanes=new plane_list();
    // Start is called before the first frame update
     void Awake()
    {
        myplanes = JsonUtility.FromJson<plane_list>(plane_data.text);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
