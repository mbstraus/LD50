using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    [SerializeField]
    public string SceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Moving to " + SceneToLoad);
        GameManager.Instance.MoveToNextArea(SceneToLoad);
    }
}
