using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    public int MaxLightPips = 10;
    [SerializeField]
    public int CurrentLightPips = 10;

    public Vector3 LastPowerStationPosition;
    public string LastPowerStationScene;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void SetPowerStation(Transform powerStation) {
        LastPowerStationPosition = powerStation.position;
        LastPowerStationScene = SceneManager.GetActiveScene().name;
    }

    public void PlayerDied() {
        CurrentLightPips = MaxLightPips;
        SceneManager.LoadScene("Death", LoadSceneMode.Single);
    }
}
