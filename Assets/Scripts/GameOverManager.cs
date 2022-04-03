using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{

    public void Respawn() {
        if (GameManager.Instance == null) {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene(GameManager.Instance.LastPowerStationScene, LoadSceneMode.Single);
        }
    }
}
