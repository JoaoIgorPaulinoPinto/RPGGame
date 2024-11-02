using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerPrefsNameConfigNaming", menuName = "Player/PlayerPrefsNameConfig")]
public class PlayerPrefsDataSaving : ScriptableObject
{
    public string musicVolume = "MusicVolume";
    public string generalVolume = "MasterVolume";
    public string SFXVolume = "SFXVolume";

    public string screenWidth = "ScreenWidth";
    public string screenHeight = "ScreenHeight";
    public string isWindowMode = "WindowMode";
}
