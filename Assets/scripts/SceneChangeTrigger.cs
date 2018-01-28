using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    public string targetScene;

    void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(targetScene, LoadSceneMode.Single);
    }
}
