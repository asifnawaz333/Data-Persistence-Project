using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{   
    public Text highScoreText;
    private string myText;
    private string textkey = "Mykey";

    void Start()
    {
        myText = null;
        PlayerPrefs.SetString(textkey,myText);
        PlayerPrefs.Save();
        ShowHighScore();         
    }

    private void ShowHighScore()
    {
        highScoreText.text = PresistData.instance.PresText;
        myText = highScoreText.text;
         PlayerPrefs.GetString(textkey,myText);
        PlayerPrefs.Save();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
