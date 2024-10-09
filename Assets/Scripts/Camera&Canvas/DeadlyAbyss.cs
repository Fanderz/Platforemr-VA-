using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadlyAbyss : MonoBehaviour
{
    private const string SceneName = "SampleScene";

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Frog frog))
            SceneManager.LoadScene(SceneName);
    }
}
