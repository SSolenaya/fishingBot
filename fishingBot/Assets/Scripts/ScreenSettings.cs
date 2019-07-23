using UnityEngine;

namespace Assets.Scripts {
   
    public abstract class ScreenSettings
    {
        public abstract string Name { get; }
        
        public abstract Vector2 GetMainBtnLocation();

        public abstract Vector2 GetPositionOfPointOnRainbow();

        public abstract Vector2 GetPositionOfTrofyField();

        public abstract Vector2 GetPositionOfFailModalWindow();

        public abstract Vector2 GetPositionOfBtnCloseForTrofyModalWin();

        public abstract Vector2 GetPositionOfBtnCloseForFailModalWin();

        public abstract Vector2 GetPositionForFishingWindow();

        public abstract Vector2 GetSizeForFishingWindow();

        public abstract Color GetColorOfMainBtn();

        public abstract Color GetColorOfPointOnRainbow();

        public abstract Color GetColorOfTrofyModalWin();

        public abstract Color GetColorOfFailModalWin();



    }
}
