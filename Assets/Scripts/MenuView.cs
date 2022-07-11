using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuView : MonoBehaviour
{
    public void StartGame() => SceneManager.LoadScene("GameScene");
    public void ExitGame() => Application.Quit();

}
