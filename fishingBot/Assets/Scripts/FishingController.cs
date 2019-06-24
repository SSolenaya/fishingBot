using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class FishingController : MonoBehaviour {

    public static FishingController inst;
    //public bool greenLight = true; // можно ли тянуть,учитывая цвет радуги
    public bool isEqual; //ведется проверка на равенство или неравенство цветов

    private void Awake() {
        inst = this;
    }

    public IEnumerator IImitationLongClick () {
        //имитация нажатия ЛК мыши
        while(true) {
            //for (var i = 0; i < 10; i++) {
                //for(var j = 0; j < 3; j++) {
                    CursorControl.SimulateLeftClick();
            CursorControl.SetGlobalCursorPos(new Vector2(335, 590) + new Vector2(UnityEngine.Random.Range(1, 5), UnityEngine.Random.Range(1, 5)));
            //}
            // yield return null;
            //}


            if(/*!CheckColors(GlobalParameters.pointOnRainbowLocation, GlobalParameters.colorOfPointOnRainbow)*/Input.GetKeyDown(KeyCode.A)) {
               // Debug.Log("click");
                break;

            }
            yield return null;
        }
    }

    public bool ColorsEqual(Color c, Color controlColor) {
        var zero = 0.1f;
        if (Mathf.Abs(c.r - controlColor.r)>zero) return false;
        if(Mathf.Abs(c.g- controlColor.g) > zero) return false;
        if(Mathf.Abs(c.b - controlColor.b) > zero) return false;
        if(Mathf.Abs(c.a - controlColor.a) > zero) return false;

        return true;
    }

    public bool CheckColors(Vector2 location, Color controlColor) {
        var localColor = ColorController.inst.GetPixelColor(location);
        var exit = ColorsEqual(localColor, controlColor);
      
        //Debug.Log(localColor.r + " " + localColor.g + " " + localColor.b + " " + localColor.a);
        if (!exit) {
           // Debug.Log("CheckColors " + exit + " " + localColor + " " + controlColor); //bear
        }
        return exit;
    }

    public IEnumerator ICheckingColor (Vector2 location, Color concreteColor) { //имитация нажатия ЛК мыши
        while(!CheckColors(location, concreteColor)) {
            //Debug.Log("location = " + location + " " + concreteColor);
            //CheckColors(location, controlColor);
            yield return null;
        }
    }

    public IEnumerator IFishingCycle () { //общий корутин цикла рыбалки
        yield return new WaitForSeconds(4f);
        Debug.Log("IFishingCycle");
        yield return StartCoroutine(ICheckingColor(GlobalParameters.mainButtonLocation, GlobalParameters.colorOfMainButton));
        Debug.Log("Кнопка Закинуть нашлась");
        yield return StartCoroutine(IEnumPressBtn(GlobalParameters.mainButtonLocation));
        Debug.Log("Единичное нажатие");
        //yield return new WaitForSeconds(3.0f);  //bear
        yield return StartCoroutine(ICheckingColor(GlobalParameters.mainButtonLocation, GlobalParameters.colorOfMainButton));
        Debug.Log("Кнопка Тянуть нашлась");
        int t = 0;
        while (true) {
            yield return StartCoroutine(IImitationLongClick());
            t++;
            //Debug.Log("t = " + t);
            if(CheckColors(GlobalParameters.fieldTrofyLocation, GlobalParameters.colorOffieldInTrofyWindow)) {
               // Debug.Log("btnCloseTrofyWindow");
                yield return StartCoroutine(IEnumPressBtn(GlobalParameters.btnCloseTrofyWindow));
                break;
            }

            if(CheckColors(GlobalParameters.fieldTrofyLocation, GlobalParameters.colorOffieldInFailWindow)) {
               // Debug.Log("btnCloseFailWindow");
                yield return StartCoroutine(IEnumPressBtn(GlobalParameters.btnCloseFailWindow));
                break;
            }
            //yield return new WaitForSeconds(0.3f);
            yield return null;
        }
        /*else if(CheckColors(GlobalParameters.fieldTrofyLocation, GlobalParameters.colorOffieldInFailWindow)) {
            yield return StartCoroutine(IEnumPressBtn(GlobalParameters.btnCloseFailWindow));
        }*/
        yield return null;
    }
      
    
    public IEnumerator IEnumPressBtn (Vector2 vec) { //Нажатие кнопки Закинуть
        yield return new WaitForSeconds(0.5f);
        CursorControl.SetGlobalCursorPos(vec);
        yield return new WaitForSeconds(0.5f);
        CursorControl.SimulateLeftClick();
    }

    private void Start ()
    {
        
    }
}
