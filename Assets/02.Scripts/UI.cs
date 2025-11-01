using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private GameObject textObject1;
    [SerializeField] private GameObject textObject2;
    [SerializeField] private GameObject textObject3;
    private bool textRange = false;
    private bool sceneRange = false;
    public string sceneName;
    private void Start()
    {
        textObject1.SetActive(false);
        textObject2.SetActive(false);
        textObject3.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (textRange == true)
            {
                textObject2.SetActive(true);
            }
            else if (sceneRange==true)
            {
                SceneManager.LoadScene("Boss1");
            }


                ////조작법텍스트띄우기
                //Time.timeScale = 0;
                //textObject2.SetActive(!textObject2.activeSelf);
            //if (Input.GetKeyDown(KeyCode.E))
            //{
            //    Time .timeScale = 1;
            //    textObject2.SetActive(false);
            //}
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{gameObject.name}트리거enter{collision.name}");
        
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Text"))
            {
                Debug.Log("텍스트 트리거 ㅇ");
                textRange = true;
                textObject1.SetActive(true);
            }
            if (gameObject.CompareTag("Gate"))
            {
                Debug.Log("문트리거 ㅇ");
                sceneRange = true;
                textObject3.SetActive(true);
            }

            
            //textRange = true;
            //textObject1.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Text"))
            {
                textRange = false;
                textObject1.SetActive(false);
            }
            if (gameObject.CompareTag("Gate"))
            {
                sceneRange = false;
                textObject3.SetActive(false);
            }

            //textObject1.SetActive(false);
        }
    }

}
