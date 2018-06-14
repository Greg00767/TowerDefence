using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextableIntValue : MonoBehaviour
{
    public IntVariable Max;
    public IntVariable Current;
    public Text textValue;
    public bool useMaxValue = true;
    public string prefix;

    private void Update()
    {
        if (useMaxValue && Max != null)
            textValue.text = prefix + Current.Value + "/" + Max.Value;
        else
            textValue.text = prefix + Current.Value.ToString();
    }
}
