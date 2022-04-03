using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    public int MaxLightPips = 10;
    [SerializeField]
    public int CurrentLightPips = 10;

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

    public void SetPowerStation(Transform powerStation) {
        IsPowerStationInAnotherScene = false;
        LastPowerStationPosition = powerStation.position;
        LastPowerStationScene = SceneManager.GetActiveScene().name;
    }

    public void PlayerDied() {
        CurrentLightPips = MaxLightPips;
        SceneManager.LoadScene("Death", LoadSceneMode.Single);
    }

    public void MoveToNextArea(string sceneName) {
        IsPowerStationInAnotherScene = true;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
