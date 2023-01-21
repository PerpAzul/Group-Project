using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class OptionsScreen : MonoBehaviour


{
    public Toggle fullscreenToggle, vsyncToggle;
    private int selectedResolution;
    
    public List<ResolutionItem> resolutions = new List<ResolutionItem>();
    public TMP_Text resolutionLabel;

    public AudioMixer theMixer;
    public TMP_Text masterLabel, musicLabel, sfxLabel;
    public Slider masterSlider, musicSlider, sfxSlider;
    
    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncToggle.isOn = false;
        }
        else
        {
            vsyncToggle.isOn = true;
        }

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                selectedResolution = i;
                
                updateResLabel();
            }
        }

        if (!foundRes)
        {
            ResolutionItem newRes = new ResolutionItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;
            
            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;
            
            updateResLabel();
        }

        float vol = 0f;
        theMixer.GetFloat("MasterVol", out vol);
        masterSlider.value = vol;
        theMixer.GetFloat("MusicVol", out vol);
        musicSlider.value = vol;
        theMixer.GetFloat("SFXVol", out vol);
        sfxSlider.value = vol;

        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " +
                               resolutions[selectedResolution].vertical.ToString();
    }

    public void resolutionLeft()
    {
        selectedResolution --;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        
        updateResLabel();
    }

    public void resolutionRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }
        
        updateResLabel();
    }

    public void ApplyGraphics()
    {
        Screen.fullScreen = fullscreenToggle.isOn;

        if (vsyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical,
            fullscreenToggle.isOn);
    }

    public void SetMasterVolume()
    {
        masterLabel.text = Mathf.RoundToInt(masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);
        
        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }
    
    public void SetMusicVolume()
    {
        musicLabel.text = Mathf.RoundToInt(musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);
        
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }
    
    public void SetSFXVolume()
    {
        sfxLabel.text = Mathf.RoundToInt(sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);
        
        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }
    

}
[System.Serializable]
public class ResolutionItem
{
    public int horizontal, vertical;
}
