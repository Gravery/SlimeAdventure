using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class cooldownFireball : MonoBehaviour
{

    public Slider slider;

    
    public void SetMaxTime(float time) 
    {
        slider.maxValue = time;    
    }


    public void SetLoading(float value)
    {
        slider.value = value;
    }
}
