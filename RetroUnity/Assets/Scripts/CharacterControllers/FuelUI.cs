using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    public Image fillImage;

    LanderController landerController;

    void Start()
    {
        landerController = FindObjectOfType<LanderController>();
    }

    void Update()
    {
        if(fillImage)
            fillImage.fillAmount = landerController.GetFuelPercentage();
    }
}
