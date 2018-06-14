using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextableFloatValue : MonoBehaviour
{
    public FloatVariable Max;
    public FloatVariable Current;
    public Text textValue;
    public bool useMaxValue = true;
    public string prefix;

    private void Update()
    {
        if (useMaxValue)
            textValue.text = prefix + Current.Value + "/" + Max.Value;
        else
            textValue.text = prefix + Current.Value.ToString();
    }
}
