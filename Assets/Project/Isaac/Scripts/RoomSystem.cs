using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSystem : MonoBehaviour
{

    public List<string> EasyScenes;
    public List<string> MediumScenes;
    public List<string> HardScenes;


    public void Start()
    {
        
    }

    public void RemoveRoom(SceneType sceneType)
    {
        string actualScene = SceneManager.GetSceneAt(1).name;

        if (sceneType == SceneType.Easy)
            EasyScenes.Remove(actualScene);

        if (sceneType == SceneType.Medium)
            MediumScenes.Remove(actualScene);

        if (sceneType == SceneType.Hard)
            HardScenes.Remove(actualScene);

        SceneManager.UnloadSceneAsync(actualScene);
    }
    public bool RoomsRemaining(SceneType sceneType)
    {
        if (sceneType == SceneType.Easy)
            return EasyScenes.Count >= 1;

        else if (sceneType == SceneType.Medium)
            return MediumScenes.Count >= 1;

        else if (sceneType == SceneType.Hard)
            return HardScenes.Count >= 1;

        return false;
    }

}
