using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSystem : MonoBehaviour
{
 
    public List<Scene> EasyScenes;
    public List<Scene> MediumScenes;
    public List<Scene> HardScenes;


    public void Start()
    {
        
    }

    public void RemoveRoom(SceneType sceneType)
    {
        Scene actualScene = SceneManager.GetSceneAt(1);

        if (sceneType == SceneType.Easy)
        EasyScenes.Remove(actualScene);

        if (sceneType == SceneType.Medium)
            MediumScenes.Remove(actualScene);

        if (sceneType == SceneType.Hard)
            HardScenes.Remove(actualScene);

        SceneManager.UnloadSceneAsync(actualScene);
    }
    public bool EasyRoomsRemaining()
    {
        return EasyScenes.Count >= 1;
    }

    public bool MediumRoomsRemaining()
    {
        return MediumScenes.Count >= 1;
    }

    public bool HardRoomsRemaining()
    {
        return HardScenes.Count >= 1;
    }


}
