using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseListItemRenderer : MonoBehaviour {
    public delegate void ListItemClicked(BaseListItemRenderer itemRender);
    public event ListItemClicked OnListItemClicked;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Public Functions

    public virtual void setData( object data )
    {
        //Abstract
    }

    public virtual void onClicked()
    {
        if (OnListItemClicked != null)
        {
            OnListItemClicked( this );
        }
    }
}
