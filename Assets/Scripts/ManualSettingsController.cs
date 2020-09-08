using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ManualSettingsController: MonoBehaviour {

    public static ManualSettingsController inst;

    public ToggleGroup toggleGroup;  //bear
    public PrefabToggle pointOnRainbowTog;
    public List<PrefabToggle> listPrefabToggles;
    public GameObject textGroupData;
    public Text textForRainbowPoint;
    public Button oKBtn;
    public Button findBtn;
    public Button saveBtn;
    private Coroutine coro;
    private InputField _coordsRainbow;
    private InputField _colorRainbow;

    public string keyForPlayerPrefs = "current manual settings";

    public Vector2 tempV2;
    public Color tempColor;

    void Awake () {
        inst = this;

    }

    public void Start () {
        InputFields(pointOnRainbowTog);
        var rainTog = pointOnRainbowTog.toggle;

        oKBtn.onClick.RemoveAllListeners();
        oKBtn.onClick.AddListener(() => {
            UI.inst.SwapCanvas();
            SavingData();
        });

        findBtn.onClick.RemoveAllListeners();
        findBtn.onClick.AddListener(() => {
            findBtn.interactable = false;
            StartCoroutine(IeWaitingPressA());
        });

        rainTog.onValueChanged.RemoveAllListeners();
        rainTog.onValueChanged.AddListener((var) => {
            toggleGroup.allowSwitchOff = true;
            toggleGroup.SetAllTogglesOff();
            /* _coordsRainbow.gameObject.SetActive(var);
             _colorRainbow.gameObject.SetActive(var);
             if(var) {*/
            
            _coordsRainbow.onValueChanged.RemoveAllListeners();
            _coordsRainbow.onValueChanged.AddListener((txt) => {
                var v2 = ReadingCoordsInputField(_coordsRainbow.text);
                if(v2 != Vector2.zero) {
                    tempV2 = v2;
                }
                if(var && coro == null) {
                    coro = StartCoroutine(IEColorOnRaibow());
                }
            });
            if(!var && coro != null) {
                StopCoroutine(coro);
            }
         });

        saveBtn.onClick.RemoveAllListeners();
        saveBtn.onClick.AddListener(() => {
            ReferenceToSettings(pointOnRainbowTog);
            ToggleText(pointOnRainbowTog, tempV2, tempColor);
        });
    }

    public IEnumerator IEColorOnRaibow () {
        while(true) {
            var currColor = ColorController.inst.GetPixelColor(tempV2);
            if(ColorController.inst.ColorsEqual(currColor, ManualInputScreenSettings.colorOfPointOnRainbowEtalon, 0.05f)) {
                tempColor = currColor;
                pointOnRainbowTog.color = tempColor;
                pointOnRainbowTog.location = tempV2;
                ReferenceToSettings(pointOnRainbowTog);
                SavingData();
                _colorRainbow.image.color = Color.green;
                _colorRainbow.text = currColor.ToString();
                break;
            } else {
                _colorRainbow.text = currColor.ToString();
                _colorRainbow.image.color = Color.red;
            }
            yield return null;
        }
    }

    public void InputFields (PrefabToggle prefTog) {
        InputField[] inputFields = prefTog.GetComponentsInChildren<InputField>();
        foreach(var iF in inputFields) {
            var nameIf = iF.gameObject.transform.name;
            switch(nameIf) {
                case "CoordsInput":
                    _coordsRainbow = iF;
                    break;
                case "ColorInput":
                    _colorRainbow = iF;
                    break;
            }
        }
    }

    public Vector2 ReadingCoordsInputField (string txt) {
        if(txt == "") {
            return Vector2.zero;
        }
        var coords = txt.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        if(coords.Length < 2) {
            return Vector2.zero;
        }
        Vector2 vec = new Vector2 {
            x = int.Parse(coords[0]),
            y = int.Parse(coords[1])
        };
        return vec;
    }

    public void BindingTextAndPrefabs () {
        for(int i = 0; i < listPrefabToggles.Count; i++) {
            listPrefabToggles[i].text = textGroupData.GetComponentsInChildren<Text>()[i];
            ReverseReferenceToSettings(listPrefabToggles[i]);
        }
        pointOnRainbowTog.text = textForRainbowPoint;
        ReverseReferenceToSettings(pointOnRainbowTog);
    }

    public PrefabToggle GetPrefabToggleEnable () {
        foreach(var tog in listPrefabToggles) {
            if(tog.toggle.isOn) {
                return tog;
            }
        }
        return null;
    }

    public void SavingData () {
        var m = UI.sS as ManualInputScreenSettings;
        string json = JsonUtility.ToJson(m);
        PlayerPrefs.SetString(keyForPlayerPrefs, json);
    }

    public void ReferenceToSettings (PrefabToggle tt) {
        var man = UI.sS as ManualInputScreenSettings;
        switch(tt.typeToggle) {
            case TypeToggle.mainButton:
                man.mainButtonLocation = tt.location;
                man.colorOfMainButton = tt.color;
                break;
            case TypeToggle.pointOnRainbow:
                man.pointOnRainbowLocation = tt.location;
                man.colorOfPointOnRainbow = tt.color;
                Debug.Log("rainbow" + " " + tt.location + " " + tt.color); //bear
                break;
            case TypeToggle.fieldTrofy:
                man.fieldTrofyLocation = tt.location;
                man.colorOfFieldInTrofyWindow = tt.color;
                break;
            case TypeToggle.fieldFailWindow:
                man.fieldFailWindowLocation = tt.location;
                man.colorOfFieldInFailWindow = tt.color;
                break;
            case TypeToggle.btnCloseTrofyWindow:
                man.btnCloseTrofyWindow = tt.location;
                break;
            case TypeToggle.btnCloseFailWindow:
                man.btnCloseFailWindow = tt.location;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void ReverseReferenceToSettings (PrefabToggle tt) {
        var man = UI.sS as ManualInputScreenSettings;
        switch(tt.typeToggle) {
            case TypeToggle.mainButton:
                ToggleText(tt, man.mainButtonLocation, man.colorOfMainButton);
                break;
            case TypeToggle.pointOnRainbow:
                ToggleText(tt, man.GetPositionOfPointOnRainbow(), man.GetColorOfPointOnRainbow());
                break;
            case TypeToggle.fieldTrofy:
                ToggleText(tt, man.fieldTrofyLocation, man.colorOfFieldInTrofyWindow);
                break;
            case TypeToggle.fieldFailWindow:
                ToggleText(tt, man.fieldFailWindowLocation, man.colorOfFieldInFailWindow);
                break;
            case TypeToggle.btnCloseTrofyWindow:
                ToggleText(tt, man.btnCloseTrofyWindow, Color.black);
                break;
            case TypeToggle.btnCloseFailWindow:
                ToggleText(tt, man.btnCloseFailWindow, Color.black);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public IEnumerator IeWaitingPressA () {
        var currentToggle = GetPrefabToggleEnable();
        currentToggle.text.text = "";

        while(true) {
            if(Input.GetKeyDown(KeyCode.A)) {
                findBtn.interactable = true;
                Vector2 tempVec = MousePositionService.inst.GetCursorPos();
                Color tempCol = ColorController.inst.GetPixelColor(tempVec);
                ToggleText(currentToggle, tempVec, tempCol);
                break;
            }
            yield return null;
        }
        ReferenceToSettings(currentToggle);
        SavingData();
    }

    public void ToggleText (PrefabToggle prefTog, Vector2 togLocation, Color color) {
        prefTog.location = togLocation;
        prefTog.color = color;
        prefTog.text.text = togLocation.ToString();
        prefTog.text.text += " " + prefTog.color;
    }
}
