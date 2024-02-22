using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
public class classfication : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    private CUBEMANGER cUBEMANGERs;
    // Start is called before the first frame update

    private void Start()
    {
        cUBEMANGERs= GameObject.FindObjectOfType<CUBEMANGER>();
    }
    // Update is called once per frame
    void Update()
    {
        text.text = transform.position.y.ToString();
    }
}
