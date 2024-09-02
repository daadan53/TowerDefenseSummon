using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Canvas CanvasMenu;
    [SerializeField] private Image MenuBackGround;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;
    void Start()
    {
        PlayButton.onClick.AddListener(PlayButtonOnClick);
        QuitButton.onClick.AddListener(QuitButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        MenuBackGround.rectTransform.sizeDelta = CanvasMenu.GetComponent<RectTransform>().sizeDelta;
    }
    public void PlayButtonOnClick()
    {
        SceneManager.LoadScene("SceneCommands");
    }
    public void QuitButtonOnClick()
    {
        Application.Quit();
    }
}
