using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public enum 社畜State
{
    None,
    Title,
    Ending1,
};

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public 社畜State shachikuState = 社畜State.Title;
    public bool watchedEnding1 = false;
    public bool hasKamihikouki = false;

    // Start is called before the first frame update
    void Start()
    {
        // 次のシーンでも破棄しない
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.R))
        // {
        //     // タイトルに戻る
        //     SceneManager.LoadScene("TitleScene");
        // }
        // else if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     // 散歩スタート
        //     SceneManager.LoadScene("夜道Scene");
        // }
    }
}
