﻿using System.Collections;
using Tango;
using UnityEngine;

public class AreaLearningStartup : MonoBehaviour, ITangoLifecycle {

    private TangoApplication m_tangoApplication;

    // Use this for initialization
    void Start()
    {
        m_tangoApplication = FindObjectOfType<TangoApplication>();
        if(m_tangoApplication != null)
        {
            m_tangoApplication.Register(this);
            m_tangoApplication.RequestPermissions();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTangoPermissions(bool permissionsGranted)
    {
        if(permissionsGranted)
        {
            AreaDescription[] list = AreaDescription.GetList();
            AreaDescription mostRecent = null;
            AreaDescription.Metadata mostRecentMetadata = null;

            if(list != null && list.Length > 0)
            {
                // Find and load the most recent Area Description
                mostRecent = list[0];
                mostRecentMetadata = mostRecent.GetMetadata();

                foreach (AreaDescription areaDescription in list)
                {
                    AreaDescription.Metadata metadata = areaDescription.GetMetadata();
                    if( metadata.m_dateTime > mostRecentMetadata.m_dateTime)
                    {
                        mostRecent = areaDescription;
                        mostRecentMetadata = metadata;
                    }
                }

                m_tangoApplication.Startup(mostRecent);
            }
            else
            {
                // No Area Descriptions available
                // Create new descriptions
                m_tangoApplication.Startup(null);
            }
        }
    }

    public void OnTangoServiceConnected()
    {
    }

    public void OnTangoServiceDisconnected()
    {
    }

}
