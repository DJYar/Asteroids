using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnTryAgainClick);
    }

    void Update() {}

    private void OnTryAgainClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
