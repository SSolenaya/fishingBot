using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI: MonoBehaviour {

    public static UI inst;
    public Canvas mainScreenCanvas;
    public Canvas manualSettingsCanvas;
    private bool isShowed;
    public Button startBtn;
    public Button stopBtn;
    public Text programStateTxt;
    public Text colorTest1; //bear
    public Text colorTest2;  // bear
    public Button btnSettingsBear;
    public Button btnSettingsManual;
    public static ScreenSettings sS;
    private Color c;
    public List<Coroutine> coroList = new List<Coroutine>();

    void Awake () {
        inst = this;
    }
    
    public IEnumerator IChoosingWindowDelay () {  //задержка времени для выбора окна рыбалки
        yield return new WaitForSeconds(2f);
        WindowMode.inst.SetCurrentPosAndSize(sS.GetPositionForFishingWindow(), sS.GetSizeForFishingWindow());
        programStateTxt.text = "set position forfishing window";
    }

    [ContextMenu("TestStartITestCheckColor")]
    public void TestStartITestCheckColor() {
        StartCoroutines(ITestCheckColor());
    }

    public IEnumerator ITestCheckColor () { // bear
        while(true) {
            var v2 = new Vector2(473, 418);
            c = ColorController.inst.GetPixelColor(v2);
            Debug.Log(v2 + " " + c.r + " " + c.g + " " + c.b + " " + c.a);
            yield return null;
        }
    }

    public void StartCoroutines (IEnumerator ienum) {
        Coroutine coro;
        coro = StartCoroutine(ienum);
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

  public void SwapCanvas() {
        mainScreenCanvas.gameObject.SetActive(!isShowed);
        manualSettingsCanvas.gameObject.SetActive(isShowed);
        isShowed = !isShowed;
    }

    void Start () {

        isShowed = false;
        SwapCanvas();
        
        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(() => {
            //StartCoroutines(IMouseCoords());
            StartCoroutines(IChoosingWindowDelay());
            StartCoroutines(FishingController.inst.IFishingCycle());
            //Debug.Log("Запуск основного процесса рыбалки");
        });

        stopBtn.onClick.RemoveAllListeners();
        stopBtn.onClick.AddListener(StopAllLocalCoroutines);

        btnSettingsBear.onClick.RemoveAllListeners();
        btnSettingsBear.onClick.AddListener(() => {
            sS = ScreenSettingsFactory.GetSettings("bear");
            programStateTxt.text = "bearLaptop Screen Settings is active";

        });

        btnSettingsManual.onClick.RemoveAllListeners();
        btnSettingsManual.onClick.AddListener(() => {
            CreateManualSettings();
            ManualSettingsController.inst.BindingTextAndPrefabs();
            SwapCanvas();
           
        });

        

    }

    public void CreateManualSettings() {
        var key = ManualSettingsController.inst.keyForPlayerPrefs;
        var json = PlayerPrefs.GetString(key, "{}");
       var sS1 = JsonUtility.FromJson<ManualInputScreenSettings>(json);
        if(sS1 != null) {
            sS = sS1;
        }
        if(sS == null) {
            sS = ScreenSettingsFactory.GetSettings("manual");
        }
    }

}
