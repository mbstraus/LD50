using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lantern : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    private Light2D LanternLight;

    [SerializeField]
    public Sprite UnlitSprite;
    [SerializeField]
    public Sprite LitSprite;
    [SerializeField]
    public bool IsLit = false;
    [SerializeField]
    public GameObject Effector;
    [SerializeField]
    public AudioClip LanternLit;

    private AudioSource audioSource;

    void Start() {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        LanternLight = GetComponentInChildren<Light2D>();
        audioSource = GetComponent<AudioSource>();

        if (IsLit) {
            Light();
        } else {
            Douse();
        }
    }

    public void Light() {
        SpriteRenderer.sprite = LitSprite;
        LanternLight.intensity = 1f;
        audioSource.PlayOneShot(LanternLit);
        if (Effector != null) {
            Effector.SetActive(false);
        }
    }

    public void Douse() {
        SpriteRenderer.sprite = UnlitSprite;
        LanternLight.intensity = 0f;

        if (Effector != null) {
            Effector.SetActive(true);
        }
    }
}
