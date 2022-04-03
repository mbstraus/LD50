using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    public int MaxLightPips = 10;
    [SerializeField]
    public int CurrentLightPips = 10;
    [SerializeField]
    public AudioClip LevelComplete;
    [SerializeField]
    public AudioClip PlayerDiedSound;

    private AudioSource audioSource;
    public bool IsPowerStationInAnotherScene = true;
    public Vector3 LastPowerStationPosition;
    public string LastPowerStationScene;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
            IsPowerStationInAnotherScene = true;
        }
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void SetPowerStation(Transform powerStation) {
        IsPowerStationInAnotherScene = false;
        LastPowerStationPosition = powerStation.position;
        LastPowerStationScene = SceneManager.GetActiveScene().name;
    }

    public void PlayerDied() {
        CurrentLightPips = MaxLightPips;
        PlayDied();
        SceneManager.LoadScene("Death", LoadSceneMode.Single);
    }

    public void MoveToNextArea(string sceneName) {
        IsPowerStationInAnotherScene = true;
        PlayLevelComplete();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void StartGame() {
        IsPowerStationInAnotherScene = true;
        SceneManager.LoadScene("Level 1", LoadSceneMode.Single);
    }

    public void PlayLevelComplete() {
        audioSource.PlayOneShot(LevelComplete);
    }

    public void PlayDied() {
        audioSource.PlayOneShot(PlayerDiedSound);
    }
}
