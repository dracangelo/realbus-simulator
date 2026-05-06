using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeatherUI : MonoBehaviour
{
    public TextMeshProUGUI weatherText;
    public Image weatherTint;

    void Update()
    {
        if (WeatherSystem.Instance == null)
            return;

        string icon = WeatherToIcon(WeatherSystem.Instance.currentWeather);
        string nightSuffix = TimeOfDaySystem.Instance != null && TimeOfDaySystem.Instance.IsNight ? " ☾" : string.Empty;

        if (weatherText != null)
            weatherText.text = $"{icon}{nightSuffix}";

        if (weatherTint != null)
            weatherTint.color = WeatherToColor(WeatherSystem.Instance.currentWeather);
    }

    string WeatherToIcon(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.Overcast: return "☁";
            case WeatherState.Fog: return "〰";
            case WeatherState.Drizzle: return "☂";
            case WeatherState.LightRain: return "☔";
            case WeatherState.HeavyRain: return "☔+";
            case WeatherState.Thunderstorm: return "⚡";
            case WeatherState.Snow: return "❄";
            case WeatherState.Blizzard: return "❄+";
            case WeatherState.PartlyCloudy: return "⛅";
            default: return "☀";
        }
    }

    Color WeatherToColor(WeatherState state)
    {
        switch (state)
        {
            case WeatherState.LightRain:
            case WeatherState.HeavyRain:
            case WeatherState.Thunderstorm:
                return new Color(0.42f, 0.68f, 0.95f, 0.9f);
            case WeatherState.Fog:
                return new Color(0.84f, 0.86f, 0.88f, 0.88f);
            case WeatherState.Overcast:
                return new Color(0.72f, 0.76f, 0.82f, 0.9f);
            case WeatherState.Snow:
            case WeatherState.Blizzard:
                return new Color(0.9f, 0.95f, 1f, 0.9f);
            default:
                return new Color(0.98f, 0.82f, 0.32f, 0.9f);
        }
    }
}
