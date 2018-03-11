using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BaseUIWindow : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler {

    protected bool m_MouseDown = false;
    protected bool m_ScaleX = false;
    protected bool m_ScaleY = false;
    protected Vector3 m_MouseClickPos;

    [SerializeField]
    protected Image m_WindowContentImage;

    [SerializeField]
    protected Texture2D m_MouseCursorScaleH;

    [SerializeField]
    protected BaseUIWindowContentControls m_WindowContentContainer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 windowPos = gameObject.transform.position;
        RectTransform windowTrans = gameObject.transform as RectTransform;       

        Vector3[] worldConters = new Vector3[4];

        windowTrans.GetWorldCorners(worldConters);

        Vector3 bottomRight = worldConters[3];

        if ( mousePos.x > ( bottomRight.x - 20 ) )
        {
            m_ScaleX = true;
           // Cursor.SetCursor(m_MouseCursorScaleH, Vector2.zero, CursorMode.Auto);
        }

        if ( mousePos.y < ( bottomRight.y + 20 ) )
        {
            m_ScaleY = true;
        }
    }
    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!m_MouseDown)
        {
            m_ScaleX = false;
            m_ScaleY = false;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        m_MouseDown = true;
        m_MouseClickPos = Input.mousePosition;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        m_MouseDown = false;
        m_ScaleX = false;
        m_ScaleY = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if( m_MouseDown )
        {
            RectTransform rectTransform = gameObject.transform as RectTransform;

            Vector3 mouseTranslation = Input.mousePosition - m_MouseClickPos;

            Vector2 minOff = rectTransform.offsetMin;
            Vector2 maxOff = rectTransform.offsetMax;

            if( m_ScaleX || m_ScaleY )
            {
                Vector2 newSize = rectTransform.sizeDelta;
                if( m_ScaleX )
                {
                    maxOff.Set(maxOff.x + mouseTranslation.x, maxOff.y);
                }

                if( m_ScaleY )
                {
                    minOff.Set(minOff.x, minOff.y + mouseTranslation.y);
                }

                Debug.Log(minOff);
                Debug.Log(maxOff);

                if( Screen.width + maxOff.x > ( minOff.x + 50) )
                {
                    rectTransform.offsetMax = maxOff;
                }

                if ((Screen.height + maxOff.y) > minOff.y + 75)
                {
                    rectTransform.offsetMin = minOff;
                }
            }
            else
            {
                rectTransform.Translate( mouseTranslation );
            }

            m_MouseClickPos = Input.mousePosition;
        }
    }


    //Public Functions
     public void CloseButtonPressed()
    {
        Destroy(this.gameObject);
    }

    public void SetWindowImage( Sprite image )
    {
        if (m_WindowContentImage)
        {
            m_WindowContentImage.sprite = image;
        }

    }
}
