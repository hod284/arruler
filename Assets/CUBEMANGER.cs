using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using TMPro;
using System;

public class CUBEMANGER : MonoBehaviour
{
    [SerializeField] private ARRaycastManager m_RaycastManager;
    [SerializeField] private  List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject cubeprefab;
    private GameObject nowcube;
    [SerializeField] private float width;
    [SerializeField] private float height;
    private bool uitouch = false;
    [SerializeField] private RectTransform buttonparent;
    [SerializeField] private Button touchon;
    [SerializeField] private popupmanger popup;
    [SerializeField] private Slider Alphascrool;
    private void Start()
    {
        StartCoroutine(updateposition());
    }
    private void Update()
    {
        if (nowcube != null)
        {
            var po = Camera.main.WorldToScreenPoint(nowcube.transform.position);
            touchon.transform.position = po;
            buttonparent.gameObject.SetActive(uitouch == false ?false:true);
            Alphascrool.gameObject.SetActive(uitouch == false ? false : true);
            nowcube.GetComponentInChildren<MeshRenderer>().material.color = new Color(nowcube.GetComponentInChildren<MeshRenderer>().material.color.r, nowcube.GetComponentInChildren<MeshRenderer>().material.color.g, nowcube.GetComponentInChildren<MeshRenderer>().material.color.b, Alphascrool.value);
        }
       
        text.text =  nowcube !=null? $"height{nowcube.transform.localScale.y}m\nx-width{nowcube.transform.localScale.x}m\nz-width{nowcube.transform.localScale.z}m ":string.Empty;
    }
    public void widthchangescale()
    {
        if (nowcube != null)
        {
           float value = (float)Math.Round(nowcube.transform.localScale.x + width,1);
            nowcube.transform.localScale = new Vector3(value, nowcube.transform.localScale.y, nowcube.transform.localScale.z);
        }
    }
    public void zwidthchangescale()
    {
        if (nowcube != null)
        {
            float value = (float)Math.Round(nowcube.transform.localScale.z + width, 1);
            nowcube.transform.localScale = new Vector3(nowcube.transform.localScale.x, nowcube.transform.localScale.y, value);
        }
    }
    public void heightchangescale()
    {
        if (nowcube != null)
        {
            float value = (float)Math.Round(nowcube.transform.localScale.y + height, 1);
            nowcube.transform.localScale = new Vector3(nowcube.transform.localScale.x, value, nowcube.transform.localScale.z);
        }
    }
    public void widthchangescaleminus()
    {
        if (nowcube != null)
        {
            float value = (float)Math.Round(nowcube.transform.localScale.x < 0.2f ? 0.1f : nowcube.transform.localScale.x - width, 1);
            nowcube.transform.localScale = new Vector3(value, nowcube.transform.localScale.y, nowcube.transform.localScale.z);
        }
    }
    public void zwidthchangescaleminus()
    {
        if (nowcube != null)
        {
            float value = (float)Math.Round(nowcube.transform.localScale.x < 0.2f ? 0.1f : nowcube.transform.localScale.z - width, 1);
            nowcube.transform.localScale = new Vector3(nowcube.transform.localScale.x, nowcube.transform.localScale.y, value);
        }
    }
    public void heightchangescaleminus()
    {
        if (nowcube != null)
        {
            float value = (float)Math.Round(nowcube.transform.localScale.y < 0.2f ? 0.1f : nowcube.transform.localScale.y - height, 1);
            nowcube.transform.localScale = new Vector3(nowcube.transform.localScale.x,value, nowcube.transform.localScale.z);
        }
    }
    public void Rotateminus()
    {
        if (nowcube != null)
            nowcube.transform.eulerAngles = new Vector3(nowcube.transform.eulerAngles.x, nowcube.transform.eulerAngles.y+10.0f, nowcube.transform.eulerAngles.z);
    }
    public void Rotateplus()
    {
        if (nowcube != null)
            nowcube.transform.eulerAngles = new Vector3(nowcube.transform.eulerAngles.x, nowcube.transform.eulerAngles.y - 10.0f, nowcube.transform.eulerAngles.z);
    }
    public void uibuttonON()
    {
        if (uitouch==false)
        {
            buttonparent.gameObject.SetActive(true);
            Alphascrool.gameObject.SetActive(true);
            uitouch = true;
        }
        else if (uitouch == true)
        {
            buttonparent.gameObject.SetActive(false);
            Alphascrool.gameObject.SetActive(false);
            uitouch = false;
            StartCoroutine(updateposition());
        }
    }
    private IEnumerator updateposition()
    {
        while (uitouch == false)
        {
            if (Input.touchCount > 0)
            {
                if (m_RaycastManager.Raycast(Input.GetTouch(0).position, s_Hits, TrackableType.PlaneWithinPolygon))
                {
                    var hitPose = s_Hits[s_Hits.Count - 1].pose;
                    if (nowcube == null)
                    {
                        if (popup.position == Vector3.zero)
                        {
                            popup.gameObject.SetActive(true);
                            popup.Selected(hitPose.position);
                        }
                        else if (popup.position != Vector3.zero && popup.localscale != Vector3.zero)
                        {
                            var insite = Instantiate(cubeprefab, popup.position, Quaternion.identity);
                            insite.transform.localScale= popup.localscale;
                            nowcube = insite;
                        }
                    }
                    else
                    {
                        if (popup.position != Vector3.zero && popup.localscale != Vector3.zero)
                        {
                            popup.position = Vector3.zero;
                            popup.localscale = Vector3.zero;
                        }
                        else
                            nowcube.transform.position = new Vector3(hitPose.position.x, hitPose.position.y, hitPose.position.z);  
                    }
                }
            }
            yield return null;
        }
    }
    public void cubereset()
    {
        if (nowcube != null)
        {
            Destroy(nowcube);
            buttonparent.gameObject.SetActive(false);
            Alphascrool.gameObject.SetActive(false);
            uitouch = false;
            StartCoroutine(updateposition());
        }
    }

}
