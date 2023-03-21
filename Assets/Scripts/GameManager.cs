using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    // Start is called before the first frame update
    void Start()
    {
        // 次のシーンでも破棄しない
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // タイトルに戻る
            SceneManager.LoadScene("TitleScene");
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // 散歩スタート
            SceneManager.LoadScene("夜道Scene");
        }
    }
}
