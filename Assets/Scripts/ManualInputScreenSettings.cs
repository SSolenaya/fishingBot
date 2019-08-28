using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class ManualInputScreenSettings : ScreenSettings {

    public override string Name => "manual";

    //public float zero = 0.01f; // точность сравнения двух цветов

    public Vector2 mainButtonLocation; // отслеживаемый пиксель кнопок Закинуть и Тянуть
    public Vector2 pointOnRainbowLocation; // отслеживаемый пиксель на индикаторе натяжения лески
    public Vector2 fieldTrofyLocation; // отслеживаемый пиксель окна трофеев
    public Vector2 fieldFailWindowLocation; // отслеживаемый пиксель окна проигыша
    public Vector2 btnCloseTrofyWindow; // положение кнопки Закрыть окно трофеев
    public Vector2 btnCloseFailWindow; // положение кнопки Закрыть окно проигрыша
           
    public Vector2 positionOfWindowFishing = new Vector2(0, 0); //позиция закрепления окна браузера
    public Vector2 sizeOfWindowFishing = new Vector2(800, 1200); // размеры окна браузера
          
    public Color colorOfMainButton; // цвет  кнопки Закинуть и Тянуть
    public Color colorOfPointOnRainbow; // цвет индикатора лески
    public Color colorOfFieldInTrofyWindow; // цвет поля в окне трофеев
    public Color colorOfFieldInFailWindow; // цвет поля в окне проигрыша

    public static Color colorOfPointOnRainbowEtalon = new Color(0.239f, 0.388f, 0.529f, 1); // эталонный цвет индикатора лески

    public override Vector2 GetMainBtnLocation () {
        return mainButtonLocation;
    }
    public override Vector2 GetPositionOfPointOnRainbow () {
        return pointOnRainbowLocation;
    }
    public override Vector2 GetPositionOfTrofyField () {
        //fieldTrofyLocation = mainButtonLocation;
        return fieldTrofyLocation;
    }
    public override Vector2 GetPositionOfFailModalWindow () {
        //fieldFailWindowLocation = mainButtonLocation;
        return fieldFailWindowLocation;
    }
    public override Vector2 GetPositionOfBtnCloseForTrofyModalWin () {
        return btnCloseTrofyWindow;
    }
    public override Vector2 GetPositionOfBtnCloseForFailModalWin () {
        return btnCloseFailWindow;
    }
    public override Vector2 GetPositionForFishingWindow () {
        return positionOfWindowFishing;
    }
    public override Vector2 GetSizeForFishingWindow () {
        return sizeOfWindowFishing;
    }

    public override Color GetColorOfMainBtn () {
        return colorOfMainButton;
    }
    public override Color GetColorOfPointOnRainbow () {
        return colorOfPointOnRainbow;
    }
    public override Color GetColorOfTrofyModalWin () {
        return colorOfFieldInTrofyWindow;
    }
    public override Color GetColorOfFailModalWin () {
        return colorOfFieldInFailWindow;
    }
}
