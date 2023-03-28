using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenario紙ひこうき : MonoBehaviour
{
    public Light2D light2D;
    public Text text;

    IEnumerator Start()
    {
        // 明転から復帰（前半）
        yield return DOTween.Sequence().Append(DOTween.To(() => 1.5f, (float x) => light2D.intensity = x, 1f, 5f - 0.941f).SetEase(Ease.Linear)).WaitForCompletion();
        // 明転から復帰（後半）
        yield return new WaitForSeconds(0.3f);
        light2D.color = Color.white;

        var waitTime = 0.941f*3.0f;
        var spaceTime = 0.941f*3.0f;

        // yield return new WaitForSeconds(waitTime);
        yield return new WaitForSeconds(0.941f);
        text.text = "- あのころの じぶんへ -"; yield return new WaitForSeconds(waitTime*2);
        text.text = ""; yield return new WaitForSeconds(spaceTime);
        text.text = "- Pixelart -"; yield return new WaitForSeconds(waitTime);
        text.text = "pixelはる"; yield return new WaitForSeconds(waitTime);
        text.text = ""; yield return new WaitForSeconds(spaceTime);
        text.text = "- Font -"; yield return new WaitForSeconds(waitTime);
        text.text = "k8x12"; yield return new WaitForSeconds(waitTime);
        text.text = ""; yield return new WaitForSeconds(spaceTime);
        text.text = "- SE -"; yield return new WaitForSeconds(waitTime);
        text.text = "効果音ラボ"; yield return new WaitForSeconds(waitTime);
        text.text = "効果音辞典"; yield return new WaitForSeconds(waitTime);
        text.text = "OtoLogic"; yield return new WaitForSeconds(waitTime);
        text.text = "無料効果音で遊ぼう！"; yield return new WaitForSeconds(waitTime);
        text.text = ""; yield return new WaitForSeconds(spaceTime);
        text.text = "- BGM -"; yield return new WaitForSeconds(waitTime);
        text.text = "フリーBGM・音楽素材 MusMus"; yield return new WaitForSeconds(waitTime);
        text.text = "Audiostock"; yield return new WaitForSeconds(waitTime);
        text.text = "No.140678 切なくも温かいエレクトロニカ"; yield return new WaitForSeconds(waitTime);
        text.text = "No.891509 エンディング・かわいい・楽しい EDM"; yield return new WaitForSeconds(waitTime);
        text.text = ""; yield return new WaitForSeconds(spaceTime);
        text.text = "unity1week「つたえる」"; yield return new WaitForSeconds(spaceTime*2 + 0.941f*2.0f);
        text.text = ""; yield return new WaitForSeconds(spaceTime);

        text.text = "ED2: あのころの じぶんへ";
        yield return new WaitUntil(() => Input.GetButtonDown("決定"));
        yield return new WaitForSeconds(waitTime);
        text.text = "";

        // text.text = "あなたは 過去の自分に 何をつたえたいですか？";
        // yield return new WaitUntil(() => Input.GetButtonDown("決定"));

        yield return DOTween.Sequence().Append(DOTween.To(() => 1f, (float x) => light2D.intensity = x, 0f, 5f).SetEase(Ease.Linear)).WaitForCompletion();
        BgmManager.Instance.audioSource.volume = 1;
        yield return BgmManager.Instance.audioSource.DOFade(endValue: 0f, duration: 5f);
        yield return new WaitForSeconds(5.0f);

        // フラグをたてる
        GameManager.Instance.watchedEnding2 = true;
        SceneManager.LoadScene("社畜Scene");
    }

    IEnumerator DOText(string message)
    {
        foreach (char str in message.ToCharArray())
        {
            text.text += str;
            yield return new WaitForSeconds(0.15f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
