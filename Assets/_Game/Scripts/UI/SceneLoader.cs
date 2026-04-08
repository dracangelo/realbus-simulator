using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        ConfigureLandscapeOrientation();
        DontDestroyOnLoad(gameObject);
    }

    void ConfigureLandscapeOrientation()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.orientation = ScreenOrientation.AutoRotation;
#endif
    }

    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
    public void LoadCountrySelect() => SceneManager.LoadScene("CountrySelect");
    public void LoadCitySelect() => SceneManager.LoadScene("CitySelect");
    public void LoadRouteSelect() => SceneManager.LoadScene("RouteSelect");
    public void LoadGame() => SceneManager.LoadScene("GameScene");

    public void LoadCitySelectChecked()
    {
        if (HasCountrySelection())
        {
            LoadCitySelect();
            return;
        }

        Debug.LogWarning("SceneLoader: Cannot open CitySelect before selecting a country.");
        LoadCountrySelect();
    }

    public void LoadRouteSelectChecked()
    {
        if (HasCitySelection())
        {
            LoadRouteSelect();
            return;
        }

        Debug.LogWarning("SceneLoader: Cannot open RouteSelect before selecting a city.");
        if (HasCountrySelection()) LoadCitySelect();
        else LoadCountrySelect();
    }

    public void LoadGameChecked()
    {
        if (HasRouteSelection())
        {
            LoadGame();
            return;
        }

        Debug.LogWarning("SceneLoader: Cannot open GameScene before selecting a route.");
        if (HasCitySelection()) LoadRouteSelect();
        else if (HasCountrySelection()) LoadCitySelect();
        else LoadCountrySelect();
    }

    public void LoadWithDelay(string sceneName, float delay)
    {
        StartCoroutine(DelayedLoad(sceneName, delay));
    }

    System.Collections.IEnumerator DelayedLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    bool HasCountrySelection()
    {
        return GameState.Instance?.selectedCountry != null
            || CityManager.Instance?.activeCountry != null;
    }

    bool HasCitySelection()
    {
        return GameState.Instance?.selectedCity != null
            || CityManager.Instance?.activeCity != null;
    }

    bool HasRouteSelection()
    {
        return GameState.Instance?.selectedRoute != null;
    }
}
