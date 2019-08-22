using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class BearLaptop : ScreenSettings {

    public override string Name => "bear";

    //public float zero = 0.01f; // точность сравнения двух цветов

    public Vector2 mainButtonLocation = new Vector2(335, 610); // отслеживаемый пиксель кнопок Закинуть и Тянуть
    public Vector2 pointOnRainbowLocation = new Vector2(450, 390); // отслеживаемый пиксель на индикаторе натяжения лески
    public Vector2 fieldTrofyLocation = new Vector2(335, 610); // отслеживаемый пиксель окна трофеев
    public Vector2 fieldFailWindowLocation = new Vector2(335, 610); // отслеживаемый пиксель окна проигыша
    public Vector2 btnCloseTrofyWindow = new Vector2(653, 255); // положение кнопки Закрыть окно трофеев
    public Vector2 btnCloseFailWindow = new Vector2(557, 271); // положение кнопки Закрыть окно проигрыша

    public Vector2 positionOfWindowFishing = new Vector2(0, 0); //позиция закрепления окна браузера
    public Vector2 sizeOfWindowFishing = new Vector2(800, 1200); // размеры окна браузера

    public Color colorOfMainButton = new Color(0.847f, 0.710f, 0.545f, 1); // цвет  кнопки Закинуть и Тянуть
    public Color colorOfPointOnRainbow = new Color(0.239f, 0.388f, 0.529f, 1); // цвет индикатора лески
    public Color colorOfFieldInTrofyWindow = new Color(0.9843137f, 0.8862745f, 0.7490196f, 1); // цвет поля в окне трофеев
    public Color colorOfFieldInFailWindow = new Color(1f, 1f, 1f, 1); // цвет поля в окне проигрыша

    public override Vector2 GetMainBtnLocation () {
        return mainButtonLocation;
    }
    public override Vector2 GetPositionOfPointOnRainbow () {
        return pointOnRainbowLocation;
    }
    public override Vector2 GetPositionOfTrofyField () {
        return fieldTrofyLocation;
    }
    public override Vector2 GetPositionOfFailModalWindow () {
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
