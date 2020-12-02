using UnityEngine;
using UnityEngine.SceneManagement;

public class exit : MonoBehaviour
{
    public void clicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
