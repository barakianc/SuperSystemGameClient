using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WindowTabItemRender : BaseListItemRenderer {

    public class TabItemRendererData
    {
        protected string m_TabName;
        protected int m_TabIndx;

        public string TabName
        {
            get { return m_TabName;  }
            set { m_TabName = value; }
        }

        public int TabIndx
        {
            get { return m_TabIndx;  }
            set { m_TabIndx = value; }
        }

        public TabItemRendererData( string name, int indx)
        {
            this.m_TabName = name;
            this.m_TabIndx = indx;
        }
    }

    [SerializeField]
    protected Text m_TabItemLabel;

    protected int m_TabPageIndx;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public functions

    public override void setData(object data)
    {

        if( data is TabItemRendererData )
        {
            TabItemRendererData tabData = data as TabItemRendererData;

            if (tabData != null)
            {
                if (m_TabItemLabel != null)
                {
                    m_TabItemLabel.text = tabData.TabName;
                }

                m_TabPageIndx = tabData.TabIndx;
            }
        }
    }

    public override void onClicked()
    {
        base.onClicked();
    }
}
