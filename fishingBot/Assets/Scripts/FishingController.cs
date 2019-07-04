using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    public IEnumerator IImitationLongClick (Vector2 mousePosition) {
        //имитация нажатия ЛК мыши
        while(true) {
            Clicker.SimulateLongClick(mousePosition);
            if (!ColorController.inst.CheckColors(GlobalParameters.pointOnRainbowLocation, GlobalParameters.colorOfPointOnRainbow)) {
                Clicker.SimulateEndOfClick(mousePosition);
                break;
            }
            yield return null;
        }
        Clicker.SimulateEndOfClick(mousePosition);
    }

 public IEnumerator ICheckingColor (Vector2 location, Color controlColor) {
     while(!ColorController.inst.CheckColors(location, controlColor)) {
            yield return null;
        }
    }

    public IEnumerator IFishingCycle () { //общий корутин цикла рыбалки
        yield return new WaitForSeconds(5f);
        yield return StartCoroutine(ICheckingColor(GlobalParameters.mainButtonLocation, GlobalParameters.colorOfMainButton));
        yield return StartCoroutine(IEnumPressBtn(GlobalParameters.mainButtonLocation));

        yield return new WaitForSeconds(2f);  //bear
        //yield return StartCoroutine(ICheckingColor(GlobalParameters.mainButtonLocation, GlobalParameters.colorOfMainButton));
        
        while (true) {
            yield return StartCoroutine(IImitationLongClick(GlobalParameters.mainButtonLocation));

            if(ColorController.inst.CheckColors(GlobalParameters.fieldTrofyLocation, GlobalParameters.colorOfFieldInTrofyWindow)) {
                yield return StartCoroutine(IEnumPressBtn(GlobalParameters.btnCloseTrofyWindow));
                break;
            }
            if(ColorController.inst.CheckColors(GlobalParameters.fieldFailWindowLocation, GlobalParameters.colorOfFieldInFailWindow)) {
               
                yield return StartCoroutine(IEnumPressBtn(GlobalParameters.btnCloseFailWindow));
                break;
            }
            
            yield return null;
        }
     
        yield return null;
    }
      
    
    public IEnumerator IEnumPressBtn (Vector2 vec) { //Нажатие кнопки Закинуть
        CursorControl.SetGlobalCursorPos(vec);
        yield return new WaitForSeconds(0.1f);
        CursorControl.SimulateLeftClick();
    }

    private void Start ()
    {
        
    }
}
