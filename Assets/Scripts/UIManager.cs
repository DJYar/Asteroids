using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;

    void Start() { }

    void Update() { }

    public void GameOver()
    {
        canvas.gameObject.SetActive(true);
    }
}
