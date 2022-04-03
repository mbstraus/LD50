using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    private float InvulnerabilityTimeRemaining = 0f;
    private float LightDegradeTimeRemaining = 0f;

    [SerializeField]
    public float RadiusIncreaseByPip = 1f;
    [SerializeField]
    public Light2D LightSource;
    [SerializeField]
    public float InvulnerabiiltyTime = 0.5f;
    [SerializeField]
    public float LightDegradeTime = 10f;

    public delegate void OnLightPipChange(int maxLightPips, int currentLightPips);
    private OnLightPipChange lightPipChangeHandlers;

    private void Start() {
        Debug.Log("PlayerLight Start...");
        LightDegradeTimeRemaining = LightDegradeTime;
        lightPipChangeHandlers(GameManager.Instance.MaxLightPips, GameManager.Instance.CurrentLightPips);

        if (GameManager.Instance.LastPowerStationPosition != null) {
            Debug.Log("Resetting player position...");
            PlayerLight player = FindObjectOfType<PlayerLight>();
            if (player == null) {
                Debug.LogError("Player does not exist!");
                return;
            }
            player.transform.position = GameManager.Instance.LastPowerStationPosition;
        }
    }

    void Update() {
        LightSource.pointLightOuterRadius = RadiusIncreaseByPip * GameManager.Instance.CurrentLightPips;

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
            GameManager.Instance.CurrentLightPips += changeAmount;
            InvulnerabilityTimeRemaining = InvulnerabiiltyTime;
        } else {
            GameManager.Instance.CurrentLightPips += changeAmount;
        }
        lightPipChangeHandlers(GameManager.Instance.MaxLightPips, GameManager.Instance.CurrentLightPips);

        if (GameManager.Instance.CurrentLightPips <= 0) {
            GameManager.Instance.PlayerDied();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Power Station")) {
            GameManager.Instance.SetPowerStation(collision.gameObject.transform);
            if (GameManager.Instance.CurrentLightPips != GameManager.Instance.MaxLightPips) {
                GameManager.Instance.CurrentLightPips = GameManager.Instance.MaxLightPips;
                lightPipChangeHandlers(GameManager.Instance.MaxLightPips, GameManager.Instance.CurrentLightPips);
            }
        }
    }

    public void RegisterOnLightPipChange(OnLightPipChange handler) {
        lightPipChangeHandlers += handler;
    }

    public void UnregisterOnLightPipChange(OnLightPipChange handler) {
        lightPipChangeHandlers -= handler;
    }
}
