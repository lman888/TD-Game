using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoints : MonoBehaviour
{
    /* The List of Waypoints the Enemy will Travel to */
    public static Transform[] m_wayPoint;

    void Awake()
    {
        /* Gets an array of points that are a Child of this item */
        m_wayPoint = new Transform[transform.childCount];

        /* Iterates throughe ach Child and obtains the transform at position i */
        for (int i = 0; i < m_wayPoint.Length; i++)
        {
            m_wayPoint[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
