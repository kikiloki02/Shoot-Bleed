using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomSystem : MonoBehaviour
{

    public List<string> EasyScenes;
    public List<string> MediumScenes;
    public List<string> HardScenes;
    public List<string> UpgradeScenes;
    public List<string> VictoryScenes;
    public int totalScenesCompleted = 0;
    public int fightingRoomsCompleted = 0;
    public int lastUpgradeRoom = 1;
    public int numberOfRoomsForUpgrade = 4;


    public void RemoveRoom(SceneType sceneType)
    {
        totalScenesCompleted++;
        FindObjectOfType<RewardSystem>().stageReached = totalScenesCompleted;
        FindObjectOfType<CanvasText>().SetStageTxt(totalScenesCompleted);
        string actualScene = SceneManager.GetSceneAt(SceneManager.sceneCount-1).name;

        if (sceneType == SceneType.Easy)
            EasyScenes.Remove(actualScene); 

        else if (sceneType == SceneType.Medium)
            MediumScenes.Remove(actualScene);

        else if (sceneType == SceneType.Hard)
            HardScenes.Remove(actualScene);

    }
    public void UnloadRoom()
    {
        string actualScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1).name;
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

        else if (sceneType == SceneType.Upgrade)
            return UpgradeScenes.Count >= 1;

        return false;
    }

}
