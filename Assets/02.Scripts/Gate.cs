using UnityEngine;
using UnityEngine.SceneManagement;


public class Gate : MonoBehaviour
{
    [Header("Setting")]
    public GameObject textObject;
    public string seceName;

    private void Start()
    {
        textObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(seceName);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textObject.SetActive(true);
        }
    }
}
