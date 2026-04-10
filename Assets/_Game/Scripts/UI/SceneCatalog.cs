using UnityEngine;

[CreateAssetMenu(fileName = "SceneCatalog", menuName = "RealBus/Scenes/Scene Catalog")]
public class SceneCatalog : ScriptableObject
{
    [Header("Build Indices (no scene-name strings)")]
    public int mainMenuBuildIndex = 0;
    public int countrySelectBuildIndex = 1;
    public int citySelectBuildIndex = 2;
    public int routeSelectBuildIndex = 3;
    public int gameplayBuildIndex = 4;

#if UNITY_EDITOR
    [Header("Editor convenience (optional)")]
    public UnityEditor.SceneAsset mainMenuScene;
    public UnityEditor.SceneAsset countrySelectScene;
    public UnityEditor.SceneAsset citySelectScene;
    public UnityEditor.SceneAsset routeSelectScene;
    public UnityEditor.SceneAsset gameplayScene;
#endif
}

