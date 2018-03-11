using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseUIWindowContentControls : MonoBehaviour, IScrollHandler
{
    public bool bCanZoom = true;
    [SerializeField]
    protected RectTransform m_InnerContentTransform;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IScrollHandler.OnScroll(PointerEventData eventData)
    {
        if (bCanZoom)
        {
            m_InnerContentTransform.sizeDelta = new Vector2(m_InnerContentTransform.sizeDelta.x + (eventData.scrollDelta.y * 10), m_InnerContentTransform.sizeDelta.y + (eventData.scrollDelta.y * 10));
        }
    }

    public RectTransform InnerContentTransform
    {
        get
        {
            return m_InnerContentTransform;
        }
    }
}
