using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CommandController : MonoBehaviour
{
    [SerializeField] private Canvas CanvasMenu;
    [SerializeField] private Image MenuBackGround;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button BackButton;
    // Start is called before the first frame update
    void Start()
    {
        StartButton.onClick.AddListener(StartButtonOnClick);
        BackButton.onClick.AddListener(BackButtonOnClick);
    }

    // Update is called once per frame
    void Update()
    {
        MenuBackGround.rectTransform.sizeDelta = CanvasMenu.GetComponent<RectTransform>().sizeDelta;
    }
    public void StartButtonOnClick()
    {
        SceneManager.LoadScene("SceneGameplay");
    }
    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("SceneMenu");
    }
}
