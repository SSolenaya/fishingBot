using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI: MonoBehaviour {
   // public Rect testRect;
    public Button startBtn;
    public Button stopBtn;
    public Text mousePositionTxt;
    //public ToggleGroup toggleGroup;
    //public Toggle togglePrefab;
    public Button btnSettingsBear;
    public Button btnSettingsManual;
    private ScreenSettings sSF;
    // public Text pixelColorTxt;
    private Color c;
    //public bool stop;
    public List<Coroutine> coroList = new List<Coroutine>();

    public IEnumerator IMouseCoords () { // корутин координаты курсора в реальном времени
        while(true) {
            ShowMousePos();
            yield return null;
        }
    }

    public IEnumerator IChoosingWindowDelay () {  //задержка времени для выбора окна рыбалки
        yield return new WaitForSeconds(2f);
        WindowMode.inst.SetCurrentPosAndSize(_gp.GetPositionForFishingWindow(), _gp.GetSizeForFishingWindow());
    }

    [ContextMenu("TestStartITestCheckColor")]
    public void TestStartITestCheckColor() {
        StartCoroutines(ITestCheckColor());
    }

    public IEnumerator ITestCheckColor () { // bear
        while(true) {
            var v2 = CursorControl.GetGlobalCursorPos();
            c = ColorController.inst.GetPixelColor(v2);
            Debug.Log(v2 + " " + c.r + " " + c.g + " " + c.b + " " + c.a);
            yield return null;
        }
    }

    public void StartCoroutines (IEnumerator Ienum) {
        Coroutine coro;
        coro = StartCoroutine(Ienum);
        coroList.Add(coro);
        //Debug.Log(coroList.Count);
    }

    public void StopCoroutines (Coroutine coro) {
        if(coro != null) {
            StopCoroutine(coro);
            coro = null;
        }
    }

    public void StopAllLocalCoroutines () {
        foreach(var c in coroList) {
            StopCoroutines(c);
        }
    }

    public Vector2 ShowMousePos () {  //координаты курсора
        Vector2 vecMouse = CursorControl.GetGlobalCursorPos();
        var x = vecMouse.x;
        var y = vecMouse.y;
        mousePositionTxt.text = "x = " + x + " ;" + "y = " + y + " ;";
        return vecMouse;
    }

    void Start () {

        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(() => {
            StartCoroutines(IMouseCoords());
            StartCoroutines(IChoosingWindowDelay());
            StartCoroutines(FishingController.inst.IFishingCycle());
            //Debug.Log("Запуск основного процесса рыбалки");
        });

        stopBtn.onClick.RemoveAllListeners();
        stopBtn.onClick.AddListener(StopAllLocalCoroutines);

        btnSettingsBear.onClick.RemoveAllListeners();
        btnSettingsBear.onClick.AddListener(() => { ScreenSettingsFactory.GetSettings("bear"); });

        btnSettingsManual.onClick.RemoveAllListeners();
        btnSettingsManual.onClick.AddListener(() => { ScreenSettingsFactory.GetSettings("manual"); });

    }
        


    void Update () {

    }
}
