using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace Assets.Scripts {
    public class GlobalParameters
    {
        public static Vector2 mainButtonLocation = new Vector2(335, 590); // отслеживаемый пиксель кнопок Закинуть и Тянуть
        public static Vector2 pointOnRainbowLocation = new Vector2(450, 381); // отслеживаемый пиксель на индикаторе натяжения лески
        public static Vector2 fieldTrofyLocation = new Vector2(335, 590); // отслеживаемый пиксель окна трофеев
        public static Vector2 btnCloseTrofyWindow = new Vector2(670, 230); // положение кнопки Закрыть окно трофеев
        public static Vector2 btnCloseFailWindow = new Vector2(569, 254); // положение кнопки Закрыть окно проигрыша

        public static Vector2 positionOfWindowFishing = new Vector2(0,0); //позиция закрепления окна браузера
        public static Vector2 sizeOfWindowFishing = new Vector2(800, 1200); // размеры окна браузера

        public static Color colorOfMainButton = new Color(0.8352941f, 0.7019608f, 0.5333334f, 1); // цвет  кнопки Закинуть и Тянуть
        public static Color colorOfPointOnRainbow = new Color (0.2392157f, 0.3882353f, 0.5294118f, 1); // цвет индикатора лески
        public static Color colorOffieldInTrofyWindow = new Color(0.9843137f, 0.8862745f, 0.7490196f, 1); // цвет поля в окне трофеев
        public static Color colorOffieldInFailWindow = new Color(1f, 1f, 1f, 1); // цвет поля в окне проигрыша
    }
}
