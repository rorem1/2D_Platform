using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobby : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("MainLobby");
    }
    public void OnExitButton()
    {
        Debug.Log("버튼누름");
        Application.Quit();
    }
}
