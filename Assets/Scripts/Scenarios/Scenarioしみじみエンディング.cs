using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Scenarioしみじみエンディング : MonoBehaviour
{
    public GameObject endroll;
    public UnityEngine.UI.Text text;
    public GameObject player;
    public List<GameObject> gameObjects;
    public GameObject museum;
    public Light2D light2D;
    public AudioSource audioSource;

    private bool isEndingScenario = false;
    private bool isDisplayedMuseum = false;

    IEnumerator Start()
    {
        // エンドロールのスクロール
        //text.transform.DOMoveY(850f, 50f).SetEase(Ease.Linear);
        text.transform.DOLocalMoveY(750f, 50f).SetEase(Ease.Linear);
        // var textScrollSpeed = 35f;
        // text.transform.position = new Vector2(text.transform.position.x, text.transform.position.y + textScrollSpeed * Time.deltaTime);

        light2D.intensity = 0; // 最初は真っ暗

        // 背景を非表示にする
        gameObjects.ForEach((gameObject) => gameObject.SetActive(false));

        // 美術館を非表示にする
        museum.SetActive(false);
        player.SetActive(false);

        // 最初の背景だけ表示する
        gameObjects.First().SetActive(true);

        // TODO: ↓お試し中
        // yield return new WaitForSeconds(1.5f); // BaseScene読み込み待ち
        //BgmManager.Instance.Play("audiostock_822608_sample");

        yield return DOTween.Sequence().Append(DOTween.To(() => 0, (float x) => light2D.intensity = x, 1f, 0.1f).SetEase(Ease.InQuad)).WaitForCompletion();
        //endroll.transform.DOLocalMoveX(-100f, 150f);
        //endroll.transform.DOLocalMoveY(80f, 230f);

//        text.text = @"
//- まよなかさんぽ -




//- BGM -
//音の園


//- SE -
//ringtones.miyanova.com
//効果音ラボ
//効果音辞典


//- Font -
//k8x12
//LanaPixel


//- Photo -
//pixelはる

//- スペシャルサンクス -
//あね
//じゅん
//";

        //text.transform.DOLocalMoveX(-447f, 10.68f);

        //yield return new WaitForSeconds(14.9f);
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(10.68f); // 15s 22.5s 30s 37.5s 46s
            gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(3f);
        yield return BgmManager.Instance.audioSource.DOFade(endValue: 0f, duration: 7.5f).WaitForCompletion();

        // 無音＆暗くする
        light2D.intensity = 0;
        BgmManager.Instance.Stop();
        yield return new WaitForSeconds(4f);

        // プレイヤーを表示
        player.SetActive(true);
        // 美術館の入り口を表示する
        museum.SetActive(true);

        BgmManager.Instance.Play("audiostock_パラレルワールド");
        yield return BgmManager.Instance.audioSource.DOFade(endValue: 1f, duration: 5f).SetEase(Ease.InQuad).WaitForCompletion();

        yield return DOTween.Sequence().Append(DOTween.To(() => 0, (float x) => light2D.intensity = x, 1f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();
        isDisplayedMuseum = true;
    }

    //IEnumerator endingScenario()
    //{
    //    isEndingScenario = true;
    //    Debug.Log("museumScenario");

    //    // 暗くする
    //    light2D.intensity = 0;
    //    player.SetActive(false);

    //    yield return BgmManager.Instance.audioSource.DOFade(endValue: 0f, duration: 0.1f).SetEase(Ease.InQuad).WaitForCompletion();
    //    yield return new WaitForSeconds(1.5f);
    //    SeManager.Instance.Play("鉄の扉を開ける");
    //    yield return new WaitForSeconds(5f);

    //    yield return TextManager.Instance.Speech2("プレイしていただき ありがとうございました。");
    //    yield return TextManager.Instance.Speech2("体験版は ここまでです。");
    //    yield return TextManager.Instance.Speech2("この作品は 自分のつらかった過去が \nモチーフとなっています。");
    //    yield return TextManager.Instance.Speech2("同じように つらい思いをしている人が──");
    //    yield return TextManager.Instance.Speech2("前向きになってくれるような\n作品をめざしています。");
    //    yield return TextManager.Instance.Speech2("だれかに ひびいてくれたら うれしいです。");
    //    yield return TextManager.Instance.Speech2("ここが良かった、悪かった、こんなこと思ったなど──");
    //    yield return TextManager.Instance.Speech2("どんなことでも良いので\nプレイの感想を教えていただけると嬉しいです。");
    //    yield return TextManager.Instance.Speech2("良い作品になるように\nこれから がんばっていきます！       はる");
    //    yield return new WaitForSeconds(3.5f);

    //    // タイトルに戻る
    //    SceneManager.LoadScene("TitleScene");
    //    yield return null;
    //}

    void Update()
    {
        // // エンドロールのスクロール
        // var textScrollSpeed = 35f;
        // text.transform.position = new Vector2(text.transform.position.x, text.transform.position.y + textScrollSpeed * Time.deltaTime);
    }
}
