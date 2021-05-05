using UnityEngine;
using UnityEditor;

public class Playscene0 : MonoBehaviour
{
    [MenuItem("Edit/Play-Stop, But From Prelaunch Scene %0")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene("Assets/Scenes/Main.unity");
        EditorApplication.isPlaying = true;
    }
}
