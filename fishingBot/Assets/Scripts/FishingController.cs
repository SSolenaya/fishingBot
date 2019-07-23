using System.Collections;
using Assets.Scripts;
using UnityEngine;

public class FishingController: MonoBehaviour {

    public static FishingController inst;
    //public bool greenLight = true; // можно ли тянуть,учитывая цвет радуги
    public bool isEqual; //ведется проверка на равенство или неравенство цветов
    private ScreenSettings sS;
    private void Awake () {
        inst = this;
    }

    public IEnumerator IImitationLongClick (Vector2 mousePosition) {
        //имитация нажатия ЛК мыши
        Clicker.SimulateLongClick(mousePosition);
        while(true) {
            if(!ColorController.inst.CheckColors(sS.GetPositionOfPointOnRainbow(), sS.GetColorOfPointOnRainbow())) {
                //Clicker.SimulateEndOfClick(mousePosition);
                break;
            }
            yield return null;
        }
        Clicker.SimulateEndOfClick(mousePosition);
        yield return new WaitForSeconds(0.35f);
    }

    public IEnumerator IEnumWaitNeedColor (Vector2 location, Color controlColor) {
        while(!ColorController.inst.CheckColors(location, controlColor)) {
            yield return null;
        }
    }

    public IEnumerator IFishingCycle () { //общий корутин цикла рыбалки
        yield return new WaitForSeconds(5f);
        while (true) {
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(IEnumWaitNeedColor(sS.GetMainBtnLocation(), sS.GetColorOfMainBtn()));
            yield return StartCoroutine(IEnumPressBtn(sS.GetMainBtnLocation()));
            Debug.LogError("here1");
            yield return new WaitForSeconds(0.5f);  //bear
            yield return StartCoroutine(IEnumWaitNeedColor(sS.GetMainBtnLocation(), sS.GetColorOfMainBtn()));
            //yield return StartCoroutine(IEnumPressBtn(ScreenSettings.mainButtonLocation));
            Clicker.SimulateLongClick(sS.GetMainBtnLocation());
            Debug.LogError("here2");
            while(true) {
                //ColorController.inst.CheckColors(ScreenSettings.pointOnRainbowLocation, ScreenSettings.colorOfFieldInFailWindow);
                yield return StartCoroutine(IImitationLongClick(sS.GetMainBtnLocation()));

                if(ColorController.inst.CheckColors(sS.GetPositionOfTrofyField(), sS.GetColorOfTrofyModalWin())) {
                    yield return new WaitForSeconds(2f);
                    yield return StartCoroutine(IEnumPressBtn(sS.GetPositionOfBtnCloseForTrofyModalWin()));
                    break;
                }
                if(ColorController.inst.CheckColors(sS.GetPositionOfFailModalWindow(), sS.GetColorOfFailModalWin())) {
                    yield return new WaitForSeconds(2f);
                    yield return StartCoroutine(IEnumPressBtn(sS.GetPositionOfBtnCloseForFailModalWin()));
                    break;
                }
                if(Input.GetKeyDown(KeyCode.A)) {
                    Debug.Log("STOP");
                    break;
                }
                yield return null;
            }

            yield return null;
        }
      
       
    }


    public IEnumerator IEnumPressBtn (Vector2 vec) { //Нажатие кнопки Закинуть
        CursorControl.SetGlobalCursorPos(vec);
        yield return new WaitForSeconds(0.1f);
        CursorControl.SimulateLeftClick();
    }

    private void Start () {
        sS = 
    }
}
