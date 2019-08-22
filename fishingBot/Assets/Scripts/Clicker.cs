using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum MouseEventFlags {
    MOUSEEVENTF_LEFTDOWN = 0x02,
    MOUSEEVENTF_LEFTUP = 0x04,
}

public struct Point {
    public int x;
    public int y;
}

public class Clicker
{
    [DllImport("user32.dll")]
    private static extern void mouse_event (MouseEventFlags dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos (int x, int y);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos (out Point pos);


    public static void SimulateLongClick (Vector2 position) {
       mouse_event(MouseEventFlags.MOUSEEVENTF_LEFTDOWN, (uint)position.x, (uint)position.y, 0U, UIntPtr.Zero);
    }

    public static void SimulateEndOfClick (Vector2 position) {
       mouse_event(MouseEventFlags.MOUSEEVENTF_LEFTUP, (uint)position.x, (uint)position.y, 0U, UIntPtr.Zero);
    }

    public static void SimulateOneClick(Vector2 position) {
        mouse_event(MouseEventFlags.MOUSEEVENTF_LEFTDOWN, (uint)position.x, (uint)position.y, 0U, UIntPtr.Zero);
        mouse_event(MouseEventFlags.MOUSEEVENTF_LEFTUP, (uint)position.x, (uint)position.y, 0U, UIntPtr.Zero);
    }

    public static void SetGlobalCursorPos (Vector2 pos) {
        SetCursorPos((int)pos.x, (int)pos.y);
    }

    public static Vector2 GetGlobalCursorPos () {
        Point pos;
        GetCursorPos(out pos);
        return new Vector2(pos.x, pos.y);
    }
}
