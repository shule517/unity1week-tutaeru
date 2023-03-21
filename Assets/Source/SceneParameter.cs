using UnityEngine.SceneManagement;

public static class SceneParameter {
    public static string CrossSceneInformation { get; private set; } = ""; // = "yuki77mi";

    public static void LoadScene(string artist_id)
    {
        SceneManager.LoadScene("SampleScene");
        CrossSceneInformation = artist_id;
    }
}
