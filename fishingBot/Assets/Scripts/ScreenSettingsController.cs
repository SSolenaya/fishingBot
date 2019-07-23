using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettings : MonoBehaviour
{

    public IEnumerator IMouseCoords () { // корутин координаты курсора в реальном времени
        while(true) {
            ShowMousePos();
            yield return null;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
