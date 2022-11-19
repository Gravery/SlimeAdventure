using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sandstorm : MonoBehaviour
{
    public GameObject filter;
    public bool left, right, up, down;

    private GameObject particles;
    // Start is called before the first frame update
    void Start()
    {
        if(left || right || up || down){
            filter.SetActive(true);
            if(up) particles = filter.transform.GetChild(0).gameObject;
            if(down) particles = filter.transform.GetChild(1).gameObject;
            if(right) particles = filter.transform.GetChild(2).gameObject;
            if(left) particles = filter.transform.GetChild(3).gameObject;

            if(particles) particles.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
