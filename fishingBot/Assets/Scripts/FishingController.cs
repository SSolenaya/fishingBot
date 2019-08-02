using System.Collections;
using Assets.Scripts;
using UnityEngine;

public class FishingController: MonoBehaviour {

    public static FishingController inst;
   
    private void Awake () {
        inst = this;
    }

    public IEnumerator IImitationLongClick (Vector2 mousePosition) {
        //имитация нажатия ЛК мыши
        Clicker.SimulateLongClick(mousePosition);
        while(true) {
            if(!ColorController.inst.CheckColors(UI.sS.GetPositionOfPointOnRainbow(), UI.sS.GetColorOfPointOnRainbow())) {
                //Clicker.SimulateEndOfClick(mousePosition);
                break;
            }
            yield return null;
        }
        Clicker.SimulateEndOfClick(mousePosition);
        yield return new WaitForSeconds(0.2f);
    }

    public IEnumerator IEnumWaitSaughtforColor (Vector2 location, Color controlColor) {
        while(!ColorController.inst.CheckColors(location, controlColor)) {
            yield return null;
        }
    }

    public IEnumerator IFishingCycle () { //общий корутин цикла рыбалки
        yield return new WaitForSeconds(5f);
        while (true) {
            yield return new WaitForSeconds(2f);
            yield return StartCoroutine(IEnumWaitSaughtforColor(UI.sS.GetMainBtnLocation(), UI.sS.GetColorOfMainBtn()));
            yield return StartCoroutine(IEnumPressBtn(UI.sS.GetMainBtnLocation()));
            Debug.LogError("here1");
            yield return new WaitForSeconds(0.5f);  //bear
            yield return StartCoroutine(IEnumWaitSaughtforColor(UI.sS.GetMainBtnLocation(), UI.sS.GetColorOfMainBtn()));
            //yield return StartCoroutine(IEnumPressBtn(ScreenSettings.mainButtonLocation));
            Clicker.SimulateLongClick(UI.sS.GetMainBtnLocation());
            Debug.LogError("here2");
            while(true) {
                //ColorController.inst.CheckColors(ScreenSettings.pointOnRainbowLocation, ScreenSettings.colorOfFieldInFailWindow);
                yield return StartCoroutine(IImitationLongClick(UI.sS.GetMainBtnLocation()));

                if(ColorController.inst.CheckColors(UI.sS.GetPositionOfTrofyField(), UI.sS.GetColorOfTrofyModalWin())) {
                    yield return new WaitForSeconds(2f);
                    yield return StartCoroutine(IEnumPressBtn(UI.sS.GetPositionOfBtnCloseForTrofyModalWin()));
                    break;
                }
                if(ColorController.inst.CheckColors(UI.sS.GetPositionOfFailModalWindow(), UI.sS.GetColorOfFailModalWin())) {
                    yield return new WaitForSeconds(2f);
                    yield return StartCoroutine(IEnumPressBtn(UI.sS.GetPositionOfBtnCloseForFailModalWin()));
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
        Clicker.SetGlobalCursorPos(vec);
        yield return new WaitForSeconds(0.1f);
        Clicker.SimulateOneClick(vec);
    }
}
