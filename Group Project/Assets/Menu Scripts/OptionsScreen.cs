using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Properties;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionsScreen : MonoBehaviour


{
    public Toggle fullscreenToggle, vsyncToggle;
    private int selectedResolution;
    public List<ResolutionItem> resolutions = new List<ResolutionItem>();
    public TMP_Text resolutionLabel;
    
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
        //Screen.fullScreen = fullscreenToggle.isOn;

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

}
[System.Serializable]
public class ResolutionItem
{
    public int horizontal, vertical;
}
