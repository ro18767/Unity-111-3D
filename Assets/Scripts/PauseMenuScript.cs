using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private GameObject compass;
    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private Toggle compassToggle;
    [SerializeField]
    private Toggle muteToggle;
    [SerializeField]
    private Slider ambientSlider;
    [SerializeField]
    private Slider effectsSlider;
    [SerializeField]
    private Slider musicSlider;

    void Start()
    {
        // тут можуть бути коди відновлення збережених налаштувань
        Dictionary<String, String> settings = new() {  // це імітація
             { "ambientVolume", "0.215" },
             { "compassToggle", "false" }
        };
        if(settings.ContainsKey("ambientVolume"))
        {
            ambientSlider.value = Convert.ToSingle(settings["ambientVolume"],
                CultureInfo.InvariantCulture);
        }
        OnAmbientSlider(ambientSlider.value);
        if (settings.ContainsKey("effectsVolume"))
        {
            effectsSlider.value = Convert.ToSingle(settings["effectsVolume"],
                CultureInfo.InvariantCulture);
        }
        OnEffectsSlider(effectsSlider.value);
        if (settings.ContainsKey("musicVolume"))
        {
            musicSlider.value = Convert.ToSingle(settings["musicVolume"],
                CultureInfo.InvariantCulture);
        }
        OnMusicSlider(musicSlider.value);
        if (settings.ContainsKey("compassToggle"))
        {
            // Debug.Log(settings["compassToggle"]);
            compassToggle.isOn = Boolean.Parse(settings["compassToggle"]);
        }
        OnCompassToggle(compassToggle.isOn);
        if (settings.ContainsKey("muteToggle"))
        {
            muteToggle.isOn = Convert.ToBoolean(settings["muteToggle"]);
        }
        OnMuteToggle(muteToggle.isOn);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            content.SetActive( ! content.activeSelf );
        }
    }

    public void OnCompassToggle(Boolean value)
    {
        compass.SetActive(value);
    }
    public void OnMuteToggle(Boolean value)
    {
        audioMixer.SetFloat("MasterVolume", value ? -80f : 0f);
    }
    public void OnAmbientSlider(Single value)
    {
        audioMixer.SetFloat(     // встановлення Exposed parameter
            "AmbientVolume",     // назва параметру,    
            value * 100f - 80f   // [0, 1] --> [-80, 20]
        );
    }
    public void OnMusicSlider(Single value)
    {
        audioMixer.SetFloat(     // встановлення Exposed parameter
            "MusicVolume",       // назва параметру,    
            value * 100f - 80f   // [0, 1] --> [-80, 20]
        );
    }
    public void OnEffectsSlider(Single value)
    {
        audioMixer.SetFloat(     // встановлення Exposed parameter
            "EffectsVolume",     // назва параметру,    
            value * 100f - 80f   // [0, 1] --> [-80, 20]
        );
    }
}
/* Д.З. Меню паузи та налаштування
 * Реалізувати блок кнопок:
 * - Повернутись до гри
 * - Закрити програму (вихід)
 * - Відновити налаштування за замовчанням
 * - Зберегти налаштування
 * ! Закриття програми залежить від способу
 * запуску - через редактор чи самостійною 
 * програмою 
 * + Підібрати звуки (підготувати до наступного заняття)
 * - фонова музика
 * - звукові ефекти (кроки, збирання монети тощо)
 */