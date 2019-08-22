using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MousePositionService : MonoBehaviour {

    public static MousePositionService inst;
    public Text mousePositionTxt;
    public Vector2 vecMouse;
    //public Button testMousePosBtn;

    public void Awake() {
        inst = this;
        StartCoroutine(IMouseCoords());
    }

    public Vector2 GetCursorPos() {
        return vecMouse;
    }
    public IEnumerator IMouseCoords () { // корутин координаты курсора в реальном времени
        while(true) {
            ShowMousePos();
            yield return null;
        }
    }

    public void ShowMousePos () {  //координаты курсора
        vecMouse = Clicker.GetGlobalCursorPos();
        var x = vecMouse.x;
        var y = vecMouse.y;
        mousePositionTxt.text = "x = " + x + " ;" + "y = " + y + " ;";
    }

 }
