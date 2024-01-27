using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class road_follower : MonoBehaviour
{
    public PathCreator pathcreatorr;
    public float speed=1f;
    float distance_travelled;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance_travelled += speed * Time.deltaTime;
        transform.position=pathcreatorr.path.GetPointAtDistance(distance_travelled);
        transform.rotation=pathcreatorr.path.GetRotationAtDistance(distance_travelled);
        
    }
}
