using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SliderController : MonoBehaviour
{
    public FloatReference Variable;
    public FloatReference Min;
    public FloatReference Max;

    public Slider slider;

    public void Init()
    {
        slider.minValue = Min.Value;
        slider.maxValue = Max.Value;
        slider.value = Variable.Value;
    }

    void Update()
    {
        #if UNITY_EDITOR
            slider.minValue = Min.Value;
            slider.maxValue = Max.Value;
        #endif

        slider.value = Mathf.Clamp(Variable.Value, Min.Value, Max.Value);
    }
}
