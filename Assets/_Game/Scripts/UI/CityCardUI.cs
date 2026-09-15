using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CityCardUI : MonoBehaviour
{
    public TextMeshProUGUI cityNameText;
    public TextMeshProUGUI countryText;
    public TextMeshProUGUI routeCountText;
    public Button selectButton;

    public void Setup(CityDefinition city, Action<CityDefinition> onSelect)
    {
        if (cityNameText) cityNameText.text = city.cityName;
        if (countryText) countryText.text = city.country;
        if (routeCountText)
            routeCountText.text = $"{city.availableRoutes?.Length ?? 0} routes";

        selectButton?.onClick.AddListener(() => onSelect(city));
        if (selectButton != null && selectButton.GetComponent<CityPinPulse>() == null)
            selectButton.gameObject.AddComponent<CityPinPulse>();
    }
}
