using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ColorController: MonoBehaviour {

    public static ColorController inst;

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetDesktopWindow ();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern IntPtr GetWindowDC (IntPtr window);

    [DllImport("gdi32.dll", SetLastError = true)]
    public static extern uint GetPixel (IntPtr dc, int x, int y);

    [DllImport("user32.dll", SetLastError = true)]
    public static extern int ReleaseDC (IntPtr window, IntPtr dc);

    void Awake () {
        inst = this;
    }

    public Color GetPixelColor(Vector2 positionOfPxl) {
        IntPtr desk = GetDesktopWindow();
        IntPtr dc = GetWindowDC(desk);
        int a = (int)GetPixel(dc, (int)positionOfPxl.x, (int)positionOfPxl.y);
        ReleaseDC(desk, dc);
        return ConvertFromRgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
    }

    public static Color ConvertFromRgb (int alpha, int red, int green, int blue) {
        float fa = ((float)alpha) / 255.0f;
        float fr = ((float)red) / 255.0f;
        float fg = ((float)green) / 255.0f;
        float fb = ((float)blue) / 255.0f;
        return new Color(fr, fg, fb, fa);
    }

    public bool ColorsEqual (Color c, Color controlColor) {
        var zero = 0.01f;
        if(Mathf.Abs(c.r - controlColor.r) > zero)
            return false;
        if(Mathf.Abs(c.g - controlColor.g) > zero)
            return false;
        if(Mathf.Abs(c.b - controlColor.b) > zero)
            return false;
        if(Mathf.Abs(c.a - controlColor.a) > zero)
            return false;

        return true;
    }

    public bool CheckColors (Vector2 location, Color controlColor) {
        var localColor = GetPixelColor(location);
        var exit = ColorsEqual(localColor, controlColor);
        if(!exit) {
            Debug.Log("Pos " + location + " CheckColors " + localColor + " " + controlColor + " r= " + (localColor - controlColor)); //bear
        }
        return exit;
    }

    void Start () {

    }
}
