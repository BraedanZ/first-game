using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPositionManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 clickPosition = -Vector3.one;

            clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Debug.Log(clickPosition);
        }
    }
}
