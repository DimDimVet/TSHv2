using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "ScreenSetting", menuName = "ScriptableObjects/ScreenSetting")]
    public class ScreenSetting : ScriptableObject
    {
        [Header("Мин ширина(width)")]
        public int MinWidth = 1280;
        [Header("Мин высота(height)")]
        public int MinHeight = 1024;

        public void SetResolution(Resolution currentScreen)
        {
            PlayerPrefs.SetInt("CurrentWidth", currentScreen.width);
            PlayerPrefs.SetInt("CurrentHeight", currentScreen.height);
        }
        public Resolution GetResolution()
        {
            Resolution temp = new Resolution();
            temp.width = PlayerPrefs.GetInt("CurrentWidth");
            temp.height = PlayerPrefs.GetInt("CurrentHeight");
            return temp;
        }
    }
}

