using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts {

    public enum TypeToggle {
        mainButton,
        pointOnRainbow,
        fieldTrofy,
        fieldFailWindow,
        btnCloseTrofyWindow,
        btnCloseFailWindow
    }

    public class PrefabToggle : MonoBehaviour {
        public TypeToggle typeToggle;
        public Toggle toggle;
        public Text text;

        public Vector2 location;
        public Color color;

        private bool boolVar;
        private int numberOne;

        public void Awake() {
            toggle = GetComponent<Toggle>();
        }
    }
}
