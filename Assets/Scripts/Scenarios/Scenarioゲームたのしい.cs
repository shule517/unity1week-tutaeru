using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Scenarioゲームたのしい : MonoBehaviour
{
    public Light2D light2D;

    IEnumerator Start()
    {
        // 最初は真っ暗
        light2D.intensity = 0;

        // 目をあけた
        yield return DOTween.Sequence().Append(DOTween.To(() => 0, (float x) => light2D.intensity = x, 1f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();

        yield return new WaitForSeconds(2.5f);

        yield return TextManager.Instance.Speech2("あー 終わっちゃった… (A)", 0.8f);
        yield return TextManager.Instance.Speech2("このゲーム めちゃくちゃよかった…！ (A)", 0.8f);
        yield return TextManager.Instance.Speech2("雰囲気がとてもいい… (A)", 0.8f);
        yield return TextManager.Instance.Speech2("ドット絵も、音楽も、ストーリーも── (A)", 0.8f);
        yield return TextManager.Instance.Speech2("すべてが つながってる… (A)", 0.8f);
        yield return TextManager.Instance.Speech2("こんな 人の心を動かすようなゲーム── (A)", 0.8f);
        yield return TextManager.Instance.Speech2("いつか 自分も 作れたらなぁ… … … (A)", 0.8f);

        yield return new WaitForSeconds(2f);

        BgmManager.Instance.Play("バイブ音＃01");
        TextManager.Instance.Assign("");
        yield return new WaitForSeconds(2.5f);

        yield return TextManager.Instance.Speech2("─ アラームを止める ─ (A)", 0f);

        // アラームを止める
        BgmManager.Instance.Stop();
        TextManager.Instance.Assign("");

        // 暗転
        yield return DOTween.Sequence().Append(DOTween.To(() => 1f, (float x) => light2D.intensity = x, 0f, 2f).SetEase(Ease.InQuad)).WaitForCompletion();

        SceneManager.LoadScene("ワンルームScene");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
