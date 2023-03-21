using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class ScenarioEnding : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> gameObjects;
    public GameObject museum;
    public Light2D light2D;
    public AudioSource audioSource;

    private bool isEndingScenario = false;

    IEnumerator Start()
    {
        light2D.intensity = 0; // 最初は真っ暗

        // 背景を非表示にする
        gameObjects.ForEach((gameObject) => gameObject.SetActive(false));

        // 美術館を非表示にする
        museum.SetActive(false);
        player.SetActive(false);

        // 最初の背景だけ表示する
        gameObjects.First().SetActive(true);

        yield return new WaitForSeconds(1.5f); // BaseScene読み込み待ち

        //BgmManager.Instance.Play("audiostock_2075_sample");
        //BgmManager.Instance.Play("audiostock_822608_sample");
        //BgmManager.Instance.Play("audiostock_1224447_sample");

        //BgmManager.Instance.Play("audiostock_1266016_sample");
        BgmManager.Instance.Play("audiostock_892577_sample");

        yield return new WaitForSeconds(14.9f);
        yield return DOTween.Sequence().Append(DOTween.To(() => 0, (float x) => light2D.intensity = x, 1f, 0.1f).SetEase(Ease.InQuad)).WaitForCompletion();
        foreach (var gameObject in gameObjects)
        {
            gameObject.SetActive(true);
            yield return new WaitForSeconds(7.5f); // 15s 22.5s 30s 37.5s 46s
            gameObject.SetActive(false);
        }

        // 無音＆暗くする
        light2D.intensity = 0;
        yield return new WaitForSeconds(2f);
        BgmManager.Instance.Stop();

        yield return new WaitForSeconds(1f);

        // プレイヤーを表示
        player.SetActive(true);
        // 美術館の入り口を表示する
        museum.SetActive(true);
        yield return DOTween.Sequence().Append(DOTween.To(() => 0, (float x) => light2D.intensity = x, 1f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();
    }

    IEnumerator endingScenario()
    {
        isEndingScenario = true;
        Debug.Log("museumScenario");

        // 暗くする
        light2D.intensity = 0;
        player.SetActive(false);

        yield return new WaitForSeconds(1.5f);
        SeManager.Instance.Play("鉄の扉を開ける");
        yield return new WaitForSeconds(5f);

        yield return TextManager.Instance.Speech2("プレイしていただき ありがとうございました！");
        yield return TextManager.Instance.Speech2("この作品は 自分のつらかった過去が \nモチーフとなっています。");
        yield return TextManager.Instance.Speech2("同じように つらい思いをしている人が、");
        yield return TextManager.Instance.Speech2("前向きになってくれるような\n作品をめざしています。");
        yield return TextManager.Instance.Speech2("だれかに ひびいてくれたら うれしいです。");
        yield return TextManager.Instance.Speech2("良い作品になるように\nこれから がんばっていきます！       はる");
        yield return new WaitForSeconds(3.5f);

        // タイトルに戻る
        SceneManager.LoadScene("TitleScene");
        yield return null;
    }

    void Update()
    {
        if (isEndingScenario == false && - 29f < player.transform.position.x)
        {
            // 美術館＆エンディング
            StartCoroutine(endingScenario());
        }
    }
}
