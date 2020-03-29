namespace VFG.Core
{
    public interface ILoadingGame
    {
        void LoadJSONFiles();
        void LoadSprites();
        void LoadLocalization();
        void LoadPlayerPreferences();
        void LoadMainScene();
    }
}
