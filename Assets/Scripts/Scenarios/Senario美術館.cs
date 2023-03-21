using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Senario美術館 : MonoBehaviour
{
    public List<string> speechTexts;
    public float interval = 1.5f;

    public GameObject player;
    public UnityEngine.Rendering.Universal.Light2D light2D;

    private SpriteRenderer spriteRender;
    private bool isEndingScenario = false;
    private bool isDisplayedMuseum = false;
    private bool isDoorTrigger = false;

    void Start()
    {
        // 初期は非表示
        spriteRender = GetComponent<SpriteRenderer>();
        spriteRender.enabled = false;

        // ぴょんぴょん
        transform.DOMoveY(transform.position.y + 0.5f, interval).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        Debug.Log("light2D.intensity: " + light2D.intensity);

        if (light2D.intensity >= 1f)
        {
            spriteRender.enabled = isDoorTrigger;
        }

        if (spriteRender.enabled && Input.GetButtonDown("決定") && light2D.intensity >= 1f)
        {
            if (!TextManager.Instance.IsTalking)
            {
                // 美術館＆エンディング
                StartCoroutine(endingScenario());
            }
        }
    }

    IEnumerator endingScenario()
    {
        isEndingScenario = true;
        Debug.Log("museumScenario");

        // 暗くする
        light2D.intensity = 0;
        player.SetActive(false);

        yield return BgmManager.Instance.audioSource.DOFade(endValue: 0f, duration: 0.1f).SetEase(Ease.InQuad).WaitForCompletion();
        yield return new WaitForSeconds(1.5f);
        SeManager.Instance.Play("鉄の扉を開ける");

        yield return new WaitForSeconds(1.5f);
        yield return TextManager.Instance.Speech2("\n\n\n                                    つ づ く");
        yield return new WaitForSeconds(5f);

        yield return TextManager.Instance.Speech2("プレイしていただき ありがとうございました。");
        yield return TextManager.Instance.Speech2("体験版は ここまでです。");
        yield return TextManager.Instance.Speech2("つらい思いをしている人が──");
        yield return TextManager.Instance.Speech2("前向きになってくれるような\n作品をめざしています。");
        yield return TextManager.Instance.Speech2("だれかに ひびいてくれたら うれしいです。");
        yield return TextManager.Instance.Speech2("ここが良かった、悪かった、こんなこと思ったなど──");
        yield return TextManager.Instance.Speech2("どんなことでも良いので\nプレイの感想を教えていただけると嬉しいです。");
        yield return TextManager.Instance.Speech2("良い作品になるように\nこれから がんばっていきます！       はる");
        yield return new WaitForSeconds(3.5f);

        // タイトルに戻る
        SceneManager.LoadScene("TitleScene");
        yield return null;
    }

    IEnumerator Speech()
    {
        Player.Instance.IsPlayable = false; // 操作できないようにする
        foreach (var text in speechTexts)
        {
            yield return TextManager.Instance.Speech2(text);
        }
        Player.Instance.IsPlayable = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 近くに来たら表示
        //spriteRender.enabled = true;
        isDoorTrigger = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 離れたら非表示
        //spriteRender.enabled = false;
        isDoorTrigger = false;
    }
}
