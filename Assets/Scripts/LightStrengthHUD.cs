using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStrengthHUD : MonoBehaviour
{
    private PlayerLight PlayerLight;

    [SerializeField]
    public LightPipHUD lightPipHUDPrefab;
    [SerializeField]
    public Transform LightPipHUDContainer;

    void Awake()
    {
        PlayerLight = FindObjectOfType<PlayerLight>();
        PlayerLight.RegisterOnLightPipChange(HandleLightPipChange);
    }

    public void HandleLightPipChange(int MaxLightPips, int CurrentLightPips) {
        foreach (Transform child in LightPipHUDContainer) {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < MaxLightPips; i++) {
            LightPipHUD pip = Instantiate(lightPipHUDPrefab, LightPipHUDContainer);
            if (i < CurrentLightPips) {
                pip.SetActiveState(true);
            } else {
                pip.SetActiveState(false);
            }
        }
    }
}
