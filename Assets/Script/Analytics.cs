using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Analytics : MonoBehaviour
{
    private void Start()
    {
        GameAnalytics.Initialize();
    }
}
