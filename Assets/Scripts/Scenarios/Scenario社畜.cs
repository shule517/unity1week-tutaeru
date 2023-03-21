using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class Scenario社畜 : MonoBehaviour
{
    public GameObject hitokage;
    public Light2D light2D;
    public static int days = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        days = 0;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        hitokage.GetComponent<SpriteRenderer>().DOFade(0f, 0f);

        StartCoroutine(TypingBgm());

        // 暗転から復帰
        yield return DOTween.Sequence().Append(DOTween.To(() => 0f, (float x) => light2D.intensity = x, 1f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();
        yield return new WaitForSeconds(4.5f);

        TextManager.Instance.Assign("");
        yield return new WaitForSeconds(4.5f);

        // 人のしゃべり声 ざわざわ
        BgmManager.Instance.Play("busy-office-1");
        BgmManager.Instance.audioSource.volume = 0;
        BgmManager.Instance.audioSource.DOFade(endValue: 1f, duration: 5f).SetEase(Ease.InQuad).WaitForCompletion();
        hitokage.GetComponent<SpriteRenderer>().DOFade(1f, 6f);

        if (days == 0)
        {
            // 初日は無言でいきたい
            // 人のしゃべりごえ ざわざわ
            // 光で一日を表現する → Eastword参考にできそう
            yield return new WaitForSeconds(14.5f);

            // ざわざわ声をフェードアウト
            BgmManager.Instance.audioSource.DOFade(endValue: 0f, duration: 5f).SetEase(Ease.InQuad).WaitForCompletion();
            hitokage.GetComponent<SpriteRenderer>().DOFade(0f, 6f);
            yield return new WaitForSeconds(14.5f);

            // 暗転
            yield return DOTween.Sequence().Append(DOTween.To(() => 1f, (float x) => light2D.intensity = x, 0f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();

            days++;
            SceneManager.LoadScene("ワンルームScene");
        }
        else if (days == 1)
        {
            // 2日目
            yield return new WaitForSeconds(4.5f);

            yield return TextManager.Instance.Speech2("えっ… (A)", 0.8f);
            yield return TextManager.Instance.Speech2("これ今日中ですか…？ (A)", 0.8f);
            yield return TextManager.Instance.Speech2("あっ はい (A)", 0.8f);
            yield return TextManager.Instance.Speech2("わかりました… (A)", 0.8f);
            yield return TextManager.Instance.Speech2("なんとかします… (A)", 0.8f);
            yield return new WaitForSeconds(4.5f);

            // ざわざわ声をフェードアウト
            BgmManager.Instance.audioSource.DOFade(endValue: 0f, duration: 5f).SetEase(Ease.InQuad).WaitForCompletion();
            hitokage.GetComponent<SpriteRenderer>().DOFade(0f, 6f);
            yield return new WaitForSeconds(14.5f);

            // 暗転
            yield return DOTween.Sequence().Append(DOTween.To(() => 1f, (float x) => light2D.intensity = x, 0f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();

            days++;
            SceneManager.LoadScene("ワンルームScene");
        }
        else
        {
            // 3日目
            yield return new WaitForSeconds(4.5f);

            yield return TextManager.Instance.Speech2("えっ…", 0.8f);
            yield return TextManager.Instance.Speech2("よるちゃん やめちゃったんですか…", 0.8f);
            yield return TextManager.Instance.Speech2("…そうなんですね", 0.8f);
            yield return TextManager.Instance.Speech2("…わかりました", 0.8f);

            // ざわざわ声をフェードアウト
            BgmManager.Instance.audioSource.DOFade(endValue: 0f, duration: 5f).SetEase(Ease.InQuad).WaitForCompletion();
            hitokage.GetComponent<SpriteRenderer>().DOFade(0f, 6f);
            yield return new WaitForSeconds(14.5f);

            // 暗転
            yield return DOTween.Sequence().Append(DOTween.To(() => 1f, (float x) => light2D.intensity = x, 0f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();

            yield return new WaitForSeconds(2.5f);

            // 2回目に 帰る意思 分岐
            var texts = new string[] { "きょうも しごとが おわらない。",
            "はぁ…",
            "… … …",
            "やりたいことって こんなこと だっけ…",
            "みんな おかねの はなし ばかり…",
            "なかまと 思える人は だれもいない…",
            "… … …",
            "じぶんは いいもの つくりたいだけなのに…",
            "… … …",
            "… … …",
            "もう、しゅうでん の じかんだ",
            "かえらなきゃ。" };

            foreach (string str in texts)
            {
                yield return TextManager.Instance.Speech2(str);
            }

            TextManager.Instance.Assign("");

            days++;
            SceneManager.LoadScene("夜道Scene");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator TypingBgm()
    {
        var typingTexts = new string[] {
            "    ",
            "        ",
            "                    ",
        };
        var talkingText = typingTexts[Random.Range(0, typingTexts.Length)];

        StartCoroutine(TypingSe(2.5f, talkingText));
        yield return new WaitForSeconds(Random.Range(0.5f, 2.0f));

        StartCoroutine(TypingBgm());
    }

    private IEnumerator TypingSe(float pitch, string talkingText)
    {
        int messageCount = 0;

        float minPitch = pitch - 0.5f;
        float maxPitch = pitch + 0.5f;

        foreach (var str in talkingText)
        {
            if (messageCount % 2 == 0)
            {
                SeManager.Instance.Play("カーソル移動2", Random.Range(minPitch, maxPitch), 1, 2);
            }
            messageCount++;

            yield return new WaitForSeconds(0.04f);
        }
    }
}
