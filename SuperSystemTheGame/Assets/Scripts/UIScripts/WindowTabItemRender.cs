using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowTabItemRender : BaseListItemRenderer {

    [SerializeField]
    protected Text m_TabItemLabel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public functions

    public override void setData(object data)
    {

        if( data is string )
        {
            string tabLabel = data as string;

            if (m_TabItemLabel != null )
            {
                m_TabItemLabel.text = tabLabel;
            }
        }
    }

    public override void onClicked()
    {
        base.onClicked();
    }
}
