using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField]
    public AudioClip EnergyPickup;
    [SerializeField]
    public AudioClip Jump;
    [SerializeField]
    public AudioClip LoseEnergy;
    [SerializeField]
    public AudioClip PowerStation;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayEnergyPickup() {
        audioSource.PlayOneShot(EnergyPickup);
    }

    public void PlayJump() {
        audioSource.PlayOneShot(Jump);
    }

    public void PlayLoseEnergy() {
        audioSource.PlayOneShot(LoseEnergy);
    }

    public void PlayPowerStation() {
        audioSource.PlayOneShot(PowerStation);
    }
}
