using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryControler : MonoBehaviour
{
    [SerializeField] private Canvas VictoryCanvas;
    [SerializeField] private Image VictoryBackGround;
    [SerializeField] private Button PlayAgainButton;
    [SerializeField] private Button MenuButton;
    [SerializeField] private Button QuitButton;

        
    // Start is called before the first frame update
    void Start()
    {
        PlayAgainButton.onClick.AddListener(PlayAgainButtonOnClick);
        MenuButton.onClick.AddListener(MenuButtonOnClick); 
        QuitButton.onClick.AddListener(QuitButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        VictoryBackGround.rectTransform.sizeDelta = VictoryCanvas.GetComponent<RectTransform>().sizeDelta;
    }
    public void PlayAgainButtonOnClick()
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
