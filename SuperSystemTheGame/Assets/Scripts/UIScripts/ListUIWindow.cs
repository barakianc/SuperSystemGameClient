using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListUIWindow : BaseUIWindow {
    
    //Delegates and Events
    public delegate void ListItemRendererClicked(BaseListItemRenderer itemRenderer);
    public event ListItemRendererClicked OnListItemRendererClicked;

    [SerializeField]
    protected BaseListItemRenderer m_ListItemPrefab = null;

    [SerializeField]
    protected GridLayoutGroup m_GridLayoutComponent = null;


    protected ArrayList m_ListItemRenders = null;
    protected ArrayList m_DataProvider = null;
    protected bool m_DataDirtyFlag = false;

	// Use this for initialization
	void Start ()
    {
        m_ListItemRenders = new ArrayList();

        ArrayList temp = new ArrayList();
        temp.Add(1);
        temp.Add(2);
        temp.Add(3);
        temp.Add(4);
        temp.Add(5);
        temp.Add(6);
        temp.Add(7);
        temp.Add(8);
        temp.Add(9);
        temp.Add(10);
        temp.Add(11);
        temp.Add(12);
        DataProvider = temp;

        OnListItemRendererClicked += testListItemClicked;
	}

    public void testListItemClicked( BaseListItemRenderer it )
    {
        Debug.Log("Clicked: " + it);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if( m_DataDirtyFlag )
        {
            m_DataDirtyFlag = false;
            RefreshListItemRenders();
        }
	}

    //public functions
    public ArrayList DataProvider
    {
        get
        {
            return m_DataProvider;
        }
        set
        {
            m_DataProvider = value;
            m_DataDirtyFlag = true;
        }
    }


    //protected functions

    protected bool RefreshListItemRenders()
    {
        if( m_ListItemPrefab != null )
        {
            if( m_DataProvider != null && m_DataProvider.Count > 0)
            {
                if( m_DataProvider.Count <= m_ListItemRenders.Count )
                {
                    int i = 0;
                    int count = m_DataProvider.Count;

                    foreach ( BaseListItemRenderer ir in m_ListItemRenders )
                    {
                        if( i < count)
                        {
                            ir.setData( m_DataProvider[i] );
                            i++;
                        }
                        else
                        {
                            ir.setData(null);
                        }
                    }
                }
                else
                {
                    int i = 0;
                    int count = m_ListItemRenders.Count;
                    foreach( object o in m_DataProvider )
                    {
                        if( i < count )
                        {
                            BaseListItemRenderer ir = m_DataProvider[i] as BaseListItemRenderer;

                            if( ir != null)
                            {
                                ir.setData(o);
                            }
                            i++;
                        }
                        else
                        {
                            BaseListItemRenderer ir = GameObject.Instantiate<BaseListItemRenderer>(m_ListItemPrefab);

                            ir.OnListItemClicked += OnListItemClicked;

                            ir.transform.SetParent( m_GridLayoutComponent.transform );

                            m_ListItemRenders.Add(ir);

                            ir.setData(o);
                            i++;
                            count++;
                        }


                    }
                }
            }
            else if( m_GridLayoutComponent )
            {

                foreach( BaseListItemRenderer ir in m_ListItemRenders )
                {
                    ir.setData(null);
                }
            }
        }

        return false;
    }

    protected void OnListItemClicked( BaseListItemRenderer listItemRenderer )
    {
        if( OnListItemRendererClicked != null )
        {
            OnListItemRendererClicked(listItemRenderer);
        }
    }
}
