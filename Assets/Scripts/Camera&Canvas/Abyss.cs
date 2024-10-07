using UnityEngine;
using UnityEngine.SceneManagement;

public class Abyss : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Frog frog))
            SceneManager.LoadScene("SampleScene");
    }
}
