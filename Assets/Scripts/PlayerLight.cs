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
        LightDegradeTimeRemaining = LightDegradeTime;
        lightPipChangeHandlers(GameManager.Instance.MaxLightPips, GameManager.Instance.CurrentLightPips);
        if (GameManager.Instance.IsPowerStationInAnotherScene) {
            StartPositionMarker startPosition = FindObjectOfType<StartPositionMarker>();
            transform.position = startPosition.transform.position;
        } else if (GameManager.Instance.LastPowerStationPosition != null) {
            transform.position = GameManager.Instance.LastPowerStationPosition;
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
            GameManager.Instance.CurrentLightPips = Mathf.Clamp(GameManager.Instance.CurrentLightPips + changeAmount, 0, GameManager.Instance.MaxLightPips);
            InvulnerabilityTimeRemaining = InvulnerabiiltyTime;
        } else {
            GameManager.Instance.CurrentLightPips = Mathf.Clamp(GameManager.Instance.CurrentLightPips + changeAmount, 0, GameManager.Instance.MaxLightPips);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Level Exit")) {
            LevelTransition levelTransition = collision.gameObject.GetComponent<LevelTransition>();
            GameManager.Instance.MoveToNextArea(levelTransition.SceneToLoad);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Power Charge Pickup")) {
            PowerChargePickup powerChargePickup = collision.gameObject.GetComponent<PowerChargePickup>();
            ChangeCurrentLightPips(powerChargePickup.ChargeAmount, false);
            Destroy(powerChargePickup.gameObject);
        }
    }

    public void RegisterOnLightPipChange(OnLightPipChange handler) {
        lightPipChangeHandlers += handler;
    }

    public void UnregisterOnLightPipChange(OnLightPipChange handler) {
        lightPipChangeHandlers -= handler;
    }
}
