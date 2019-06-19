using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {
    public Rect testRect;
    public Button startBtn;
    public Button stopBtn;
    public Text mousePositionTxt;

    public Coroutine coroMouseCoords;
    public bool stop;
    public Color mainBtnColor;
    public Color rainbowColor;
    

    
    public IEnumerator IMouseCoords() { // корутин координаты курсора в реальном времени
        while (!stop) {
            ShowMousePos();
            yield return null;
        }
    }

    public IEnumerator choosingWindowDelay() {  //задержка времени для выбора окна рыбалки
        {
            yield return new WaitForSeconds(5f);
            WindowMode.inst.SetCurrentPosAndSize(GlobalParameters.positionOfWindowFishing, GlobalParameters.sizeOfWindowFishing);
        }
    }

    public void ShowMousePos() {  //координаты курсора
        Vector2 vecMouse = CursorControl.GetGlobalCursorPos();
        var x = vecMouse.x;
        var y = vecMouse.y;
        mousePositionTxt.text = "x = " + x + " ;" + "y = " + y + " ;";
    }

    void Start() {
        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(() => {
                stop = false;
                coroMouseCoords = StartCoroutine(IMouseCoords());
                StartCoroutine(choosingWindowDelay());
                FishingController.inst.Fishing();
            }
            );

        stopBtn.onClick.RemoveAllListeners();
        stopBtn.onClick.AddListener(() => {
            stop = true;
            StopAllCoroutines();
            
        });
    }

    void Update()
    {
        
    }
}
