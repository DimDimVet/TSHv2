using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneSelector
{
    [CreateAssetMenu(fileName = "SceneSetting", menuName = "ScriptableObjects/SceneSetting")]
    public class SceneSetting : ScriptableObject
    {
        [Header("MenuSceneIndex")]
        public int MenuSceneIndex = 0;
        [Header("GameSceneIndex")]
        public int GameSceneIndex = 1;
        [Header("VictorySceneIndex")]
        public int VictorySceneIndex = 2;
        [Header("OverSceneIndex")]
        public int OverSceneIndex = 3;
        //
        public void ReturnMainMenu()
        {
            SceneManager.LoadScene(MenuSceneIndex);
        }
    }
}

