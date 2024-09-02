using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverControler : MonoBehaviour
{
    [SerializeField] private Canvas GameOverCanvas;
    [SerializeField] private Image GameOverBackGround;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button QuitButton;
    // Start is called before the first frame update
    void Start()
    {
        RestartButton.onClick.AddListener(RestartButtonOnClick);
        MenuButton.onClick.AddListener(MenuButtonOnClick);
        QuitButton.onClick.AddListener(QuitButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        GameOverBackGround.rectTransform.sizeDelta = GameOverCanvas.GetComponent<RectTransform>().sizeDelta;
    }
    public void RestartButtonOnClick()
    {
        SceneManager.LoadScene("SceneGameplay");
    }
    public void MenuButtonOnClick()
    {
        SceneManager.LoadScene("SceneMenu");
    }
    public void QuitButtonOnClick()
    {
        Application.Quit();
    }
}
