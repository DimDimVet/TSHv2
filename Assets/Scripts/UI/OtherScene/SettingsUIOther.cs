namespace UI
{
    public class SettingsUIOther : SettingsUI
    {
        protected override void SetClass()
        {
            if (!isRun)
            {
                if (panels != null)
                {
                    WinAudioSetting winAudioSetting = new WinAudioSetting()
                    {
                        MuzVol = muzVol,
                        EfectVol = efectVol,
                        AudioClipButton = audioClipButton,
                        AudioClipGnd = audioClipGnd,
                        MinWidth = minWidth,
                        MinHeight = minHeight
                    };

                    PanelsLvl panelsLvl = new PanelsLvl()
                    {
                        GndPanel = gndPanel,
                        RezultPanel = rezultPanel
                    };

                    SceneIndex sceneIndex = new SceneIndex()
                    {
                        MenuSceneIndex = menuSceneIndex,
                        GameSceneIndex = gameSceneIndex,
                        VictorySceneIndex = victorySceneIndex,
                        OverSceneIndex = overSceneIndex
                    };
                    panels.Set(winAudioSetting, panelsLvl, sceneIndex);
                    isRun = true;
                    RunAudio();
                    panels.CallRezultPanel(true,true);
                }
                else { isRun = false; }
            }
        }

    }
}

