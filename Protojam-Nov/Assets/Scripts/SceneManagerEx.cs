using UnityEngine.SceneManagement;

public enum SceneType
{
    Main,
    InGame,
    Result
}

public class SceneManagerEx : Singleton<SceneManagerEx>
{
    public SceneType CurrentSceneType 
        => (SceneType)SceneManager.GetActiveScene().buildIndex;
    
    public static void LoadScene(SceneType type)
    {
        SceneManager.LoadScene((int) type);
    }
    // Extend SceneManager
}