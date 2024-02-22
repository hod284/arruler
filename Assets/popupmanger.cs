using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class popupmanger : MonoBehaviour
{

    [SerializeField]  private  CUBEMANGER cUBEMANGERs;
    [SerializeField] private TMP_InputField tMP_input_height;
    [SerializeField] private TMP_InputField tMP_input_width;
    [SerializeField] private TMP_InputField tMP_input_zwidth;
    public Vector3  position = Vector3.zero;
    public Vector3 localscale = Vector3.zero;
    // Start is called before the first frame update

    public void Selected( Vector3 nowposition)
    {
        position =nowposition;
    }


    public void making()
    {
        float x = 0.2f;
        float.TryParse(tMP_input_height.text, out x);
        x = (float)Math.Round(x, 1);
        float y = 0.2f;
        float.TryParse(tMP_input_width.text, out y);
        y = (float)Math.Round(y, 1);
        float z = 0.2f;
        float.TryParse(tMP_input_zwidth.text, out z);
        z = (float)Math.Round(z, 1);
        localscale = new Vector3(x, y, z);
        gameObject.SetActive(false);
    }


    public void onValuechangezwidth()
    {
        float value = 0.2f;
        float.TryParse(tMP_input_zwidth.text, out value); 
        float re = (float)Math.Clamp(value, 0.2, 60.0f);
        tMP_input_zwidth.text = re.ToString();
    }
    public void onValuechangeheight()
    {
        float value = 0.2f;
        float.TryParse(tMP_input_height.text, out value);
        float re = (float)Math.Clamp(value, 0.2, 60.0f);
        tMP_input_height.text = re.ToString();
    }
    public void onValuechangewidth()
    {
        float value = 0.2f;
        float.TryParse(tMP_input_width.text, out value);
        float re = (float)Math.Clamp(value, 0.2, 60.0f);
        tMP_input_width.text = re.ToString();
    }
}
