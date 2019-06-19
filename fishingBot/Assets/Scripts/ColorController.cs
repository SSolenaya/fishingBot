using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController: MonoBehaviour {

    public static ColorController inst;
    private Camera cam = Camera.main;

    public Camera Cam { get => cam; set => cam = value; }

    void Awake () {
        inst = this;
    }

    public Color GetPixelColor(Vector2 positionOfPxl) {
        Ray ray = Cam.ScreenPointToRay(positionOfPxl);
        Physics.Raycast(Cam.transform.position, ray.direction, out var hit, 10000.0f);
        Color c;
        if (hit.collider) {
            Texture2D tex =
                (Texture2D) hit.collider.GetComponent<Renderer>().material.mainTexture; // текстура объекта под курсором
            c = tex.GetPixelBilinear(hit.textureCoord2.x, hit.textureCoord2.y); // цвет этой текстуры
            return c;
        }
        else return Color.red;
    }

    void Start () {

    }
}
