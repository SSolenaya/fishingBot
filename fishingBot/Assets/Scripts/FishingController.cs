using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class FishingController : MonoBehaviour {

    public static FishingController inst;
    public bool greenLight; // можно ли тянуть,учитывая цвет радуги
    public bool isEqual; //ведется проверка на равенство или неравенство цветов
    public Coroutine coroClicker;
    public Coroutine coroCheckerColor;

    void Awake() {
        inst = this;
    }

    
     public void StartCoro(Coroutine coro, IEnumerator ienum) {
        StopCoro(coro);
        coro = StartCoroutine(ienum);
    }

    public void StopCoro(Coroutine coro) {
        if (coro != null) {
            StopCoroutine(coro);
            coro = null;
        }
    }
    
    public IEnumerator IImitationLongClick () { //имитация нажатия ЛК мыши
        while(greenLight) {
            CursorControl.SimulateLeftClick();
            yield return null;
        }
    }

    public bool CheckColors(Vector2 location, Color concreteColor) {
        bool exit = ColorController.inst.GetPixelColor(location) == concreteColor;
        return exit;
    }

    public IEnumerator ICheckingColor (Vector2 location, Color concreteColor) { //имитация нажатия ЛК мыши
        
        while(!CheckColors(location, concreteColor))
            {
                CheckColors(location, concreteColor);
            }
           yield return null;
        }
    

    public IEnumerator FishingCycle () { //общий корутин цикла рыбалки
        yield return StartCoroutine(ICheckingColor(GlobalParameters.mainButtonLocation,GlobalParameters.colorOfMainButton));
             // StartCoro(coroCheckerColor, ICheckingColor(GlobalParameters.mainButtonLocation, GlobalParameters.colorOfMainButton));
        PressBtn(GlobalParameters.mainButtonLocation);
        yield return StartCoroutine(ICheckingColor(GlobalParameters.mainButtonLocation, GlobalParameters.colorOfMainButton));
        //StartCoro (coroCheckerColor, ICheckingColor(GlobalParameters.mainButtonLocation, GlobalParameters.colorOfMainButton));
        while (greenLight) {
            PressBtn(GlobalParameters.mainButtonLocation);
            if (CheckColors(GlobalParameters.pointOnRainbowLocation, GlobalParameters.colorOfPointOnRainbow)) {
                greenLight = false;
            }

            yield return null;
        }
        
        //StartCoroutine(ICheckingColor(GlobalParameters.pointOnRainbowLocation, GlobalParameters.colorOfPointOnRainbow));
        //StartCoro (coroCheckerColor, ICheckingColor(GlobalParameters.pointOnRainbowLocation, GlobalParameters.colorOfPointOnRainbow));
    }

    public void PressBtn (Vector2 vec) { //Нажатие кнопки Закинуть
        CursorControl.SetGlobalCursorPos(vec);
        CursorControl.SimulateLeftClick();
    }

    public void Fishing() {
        StartCoro(coroClicker, FishingCycle());
    }

    void Start ()
    {
        
    }
}
