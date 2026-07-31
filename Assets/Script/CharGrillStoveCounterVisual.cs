using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharGrillStoveCounterVisual : MonoBehaviour
{

    [SerializeField] private GameObject grillOnGameObject;
    [SerializeField] private GameObject partcileGameObject;
    [SerializeField] private CharGrillStoveCounter charGrillStoveCounter;


    private void Start()
    {
        charGrillStoveCounter.OnStateChanged += CharGrillStoveCounter_OnStateChanged;
    }

    private void CharGrillStoveCounter_OnStateChanged(object sender, CharGrillStoveCounter.OnStateChangedEventsArgs e)
    {
        bool showVisual = e.state ==  CharGrillStoveCounter.State.Grilling || e.state == CharGrillStoveCounter.State.Grilled;
        grillOnGameObject.SetActive(showVisual);
        partcileGameObject.SetActive(showVisual);
    }
}