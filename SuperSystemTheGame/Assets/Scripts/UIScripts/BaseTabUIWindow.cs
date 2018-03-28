using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTabUIWindow : BaseUIWindow {

    //Delegates and Events
    public delegate void TabItemRendererClicked(WindowTabItemRender itemRenderer);
    public event TabItemRendererClicked OnTabItemRendererClicked;

    public WindowTabItemRender tabPrefab;

    public BaseWindowTabPageItemRenderer[] baseWindowTabPageItemRendererTest;

    [SerializeField]
    protected RectTransform m_TabListContainer;

    [SerializeField]
    protected GameObject[] m_TabList;

    [SerializeField]
    protected GameObject[] m_TabPageList;

    public BaseTabUIWindow()
    {
        
    }

    ~BaseTabUIWindow()
    {
        clearTabs();
    }


    // Use this for initialization
    void Start() {


        setData(baseWindowTabPageItemRendererTest);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public functions
    public virtual void setData( object data )
    {
        if (data != null)
        {
            ArrayList tabList = new ArrayList(data as BaseWindowTabPageItemRenderer[] );


            if( tabList != null )
            {
                clearTabs();

                m_TabList = new GameObject[tabList.Count];
                m_TabPageList = new GameObject[tabList.Count];
                
                for( int i = 0; i < tabList.Count; i++ )
                {
                    if (tabList[i] is BaseWindowTabPageItemRenderer)
                    {
                        //Spawn the tab for the page
                        WindowTabItemRender tabIR = GameObject.Instantiate<WindowTabItemRender>(tabPrefab);
                        m_TabList[i] = tabIR.gameObject;
                        tabIR.transform.SetParent(m_TabListContainer);

                        //spawn the page
                        BaseWindowTabPageItemRenderer pageIR = GameObject.Instantiate<BaseWindowTabPageItemRenderer>(tabList[i] as BaseWindowTabPageItemRenderer);
                        m_TabPageList[i] = pageIR.gameObject;
                        pageIR.transform.SetParent(m_WindowContentContainer.InnerContentTransform);
                        tabIR.setData( new WindowTabItemRender.TabItemRendererData( pageIR.pageName, i ));

                        tabIR.OnListItemClicked += OnListItemClicked;
                    }
                }
            }
        }
    }

    //protected functions

    protected virtual void clearTabs()
    {
        foreach (GameObject ir in m_TabList)
        {
            GameObject.Destroy(ir);
        }

        m_TabList = null;

        foreach( GameObject ir in m_TabPageList )
        {
            GameObject.Destroy(ir);
        }

        m_TabPageList = null;
    }

    protected void OnListItemClicked(BaseListItemRenderer listItemRenderer)
    {
        if( listItemRenderer is WindowTabItemRender && OnTabItemRendererClicked != null)
        {
            OnTabItemRendererClicked(listItemRenderer as WindowTabItemRender);
        }

        //TODO: CHANGE to that page
    }

}
