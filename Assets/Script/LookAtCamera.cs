using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
    }
}
