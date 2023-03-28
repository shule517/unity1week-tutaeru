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
    Ending3
};

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public bool LoadedBaseScene = false;
    public 社畜State shachikuState = 社畜State.Title;
    public bool watchedEnding1 = false; // ED1: 変わらない日々
    public bool watchedEnding2 = false; // ED2: あのころのじぶんへ
    public bool watchedEnding3 = false; // ED3: そして、あたらしいじぶんへ
    public bool hasKamihikouki = false;

    public void InitFlag()
    {
        shachikuState = 社畜State.Title;
        watchedEnding1 = false; // ED1: 変わらない日々
        watchedEnding2 = false; // ED2: あのころのじぶんへ
        watchedEnding3 = false; // ED3: そして、あたらしいじぶんへ
        hasKamihikouki = false;
    }

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
