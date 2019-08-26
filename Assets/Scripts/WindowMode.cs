using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class WindowMode : MonoBehaviour
{
   public static WindowMode inst;

    [DllImport("user32.dll")]
    static extern bool SetWindowPos (IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow ();

    const uint SWP_SHOWWINDOW = 0x0040;
    const int WS_BORDER = 1;

    void Awake() {
        inst = this;
    }

    public void SetCurrentPosAndSize (Vector2 pos, Vector2 size) {
        bool result = SetWindowPos(GetForegroundWindow(), 0, (int)pos.x, (int)pos.y, (int)size.x, (int)size.y, SWP_SHOWWINDOW);
    }
}
