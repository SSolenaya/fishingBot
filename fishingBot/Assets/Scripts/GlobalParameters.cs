using UnityEngine;

namespace Assets.Scripts {
    public interface IGP {
        Vector2 GetMainButtonLocation();
    }

    public class GlobalParameters: IGP {
        public static Vector2 mainButtonLocation = new Vector2(335, 610); // отслеживаемый пиксель кнопок Закинуть и Тянуть
        public static Vector2 pointOnRainbowLocation = new Vector2(473, 418); // отслеживаемый пиксель на индикаторе натяжения лески
        public static Vector2 fieldTrofyLocation = new Vector2(335, 610); // отслеживаемый пиксель окна трофеев
        public static Vector2 fieldFailWindowLocation = new Vector2(335, 610); // отслеживаемый пиксель окна проигыша
        public static Vector2 btnCloseTrofyWindow = new Vector2(670, 250); // положение кнопки Закрыть окно трофеев
        public static Vector2 btnCloseFailWindow = new Vector2(568, 273); // положение кнопки Закрыть окно проигрыша

        public static Vector2 positionOfWindowFishing = new Vector2(0,0); //позиция закрепления окна браузера
        public static Vector2 sizeOfWindowFishing = new Vector2(800, 1200); // размеры окна браузера
        
        public static Color colorOfMainButton = new Color(0.847f, 0.710f, 0.545f, 1); // цвет  кнопки Закинуть и Тянуть
        public static Color colorOfPointOnRainbow = new Color (0.239f, 0.388f, 0.529f, 1); // цвет индикатора лески
        public static Color colorOfFieldInTrofyWindow = new Color(0.9843137f, 0.8862745f, 0.7490196f, 1); // цвет поля в окне трофеев
        public static Color colorOfFieldInFailWindow = new Color(1f, 1f, 1f, 1); // цвет поля в окне проигрыша



        public Vector2 GetMainButtonLocation() {
            return mainButtonLocation;
        }
    }
}
