using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    private float InvulnerabilityTimeRemaining = 0f;
    private float LightDegradeTimeRemaining = 0f;

    [SerializeField]
    public int MaxLightPips = 10;
    [SerializeField]
    public int CurrentLightPips = 10;
    [SerializeField]
    public float RadiusIncreaseByPip = 1f;
    [SerializeField]
    public Light2D LightSource;
    [SerializeField]
    public float InvulnerabiiltyTime = 0.5f;
    [SerializeField]
    public float LightDegradeTime = 10f;

    private void Start() {
        LightDegradeTimeRemaining = LightDegradeTime;
    }

    void Update() {
        LightSource.pointLightOuterRadius = RadiusIncreaseByPip * CurrentLightPips;

        if (InvulnerabilityTimeRemaining > 0f) {
            InvulnerabilityTimeRemaining -= Time.deltaTime;
        }

        LightDegradeTimeRemaining -= Time.deltaTime;
        if (LightDegradeTimeRemaining <= 0f) {
            ChangeCurrentLightPips(-1, false);
            LightDegradeTimeRemaining = LightDegradeTime;
        }
    }

    public void ChangeCurrentLightPips(int changeAmount, bool IsDamage) {
        if (IsDamage && InvulnerabilityTimeRemaining <= 0f) {
            CurrentLightPips += changeAmount;
            InvulnerabilityTimeRemaining = InvulnerabiiltyTime;
        } else {
            CurrentLightPips += changeAmount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Power Station")) {
            CurrentLightPips = MaxLightPips;
        }
    }
}
