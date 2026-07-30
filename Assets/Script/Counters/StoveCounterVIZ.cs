using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVIZ : MonoBehaviour
{

    [SerializeField] private CharGrillStoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObject;
    [SerializeField] private GameObject particlesGameObject;

    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, CharGrillStoveCounter.OnStateChangedEventArgs e)
    {
        bool showVisual = e.state == CharGrillStoveCounter.State.Grilling || e.state == CharGrillStoveCounter.State.Grilled;

        stoveOnGameObject.SetActive(showVisual);
        particlesGameObject.SetActive(showVisual);
    }

}
