using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Scenarioワンルーム : MonoBehaviour
{
    public Light2D light2D;
    public AudioSource audioSource;
    public static int days = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void Init()
    {
        days = 0;
    }

    IEnumerator Start()
    {
        // 最初は真っ暗
        light2D.intensity = 0;

        yield return new WaitForSeconds(1.5f);

        if (days != 0)
        {
            BgmManager.Instance.Play("バイブ音＃01");
            TextManager.Instance.Assign("");
            yield return new WaitForSeconds(2.5f);

            yield return TextManager.Instance.Speech2("─ アラームを止める ─ (A)", 0f);

            // アラームを止める
            BgmManager.Instance.Stop();
            audioSource.Pause();
            TextManager.Instance.Assign("");
        }

        // 目をあけた
        yield return DOTween.Sequence().Append(DOTween.To(() => 0, (float x) => light2D.intensity = x, 1f, 5f).SetEase(Ease.InQuad)).WaitForCompletion();

        yield return new WaitForSeconds(2.5f);

        if (days == 0)
        {
            // 初日
            yield return TextManager.Instance.Speech2("… … … (A)", 0.8f);
            yield return TextManager.Instance.Speech2("夢か… (A)", 0.8f);
            yield return TextManager.Instance.Speech2("そういえば あの頃は── (A)", 0.8f);
            yield return TextManager.Instance.Speech2("ゲームつくりたかったんだよなぁ… (A)", 0.8f);
            yield return TextManager.Instance.Speech2("… … !! (A)", 0.8f);
            yield return TextManager.Instance.Speech2("もう時間だ 行かなきゃ! (A)", 0.8f);
        }
        else if (days == 1)
        {
            // 2日目
            yield return TextManager.Instance.Speech2("もう朝だ… (A)", 0.8f);
            yield return TextManager.Instance.Speech2("つかれた… (A)", 0.8f);
            yield return TextManager.Instance.Speech2("行かなきゃ… (A)", 0.8f);
        }
        else
        {
            // 3日目
            TextManager.Instance.Speech2("… … … (A)", 0.8f);
        }

        TextManager.Instance.Assign("");
        yield return new WaitForSeconds(2f);

        SeManager.Instance.Play("お着替え");
        yield return new WaitForSeconds(5.5f);
        SeManager.Instance.Stop();
        yield return new WaitForSeconds(3.5f);
        SeManager.Instance.Play("ドアを開ける3");
        yield return new WaitForSeconds(1.5f);
        SeManager.Instance.Play("ドアを閉める2");
        yield return new WaitForSeconds(2.5f);
        SeManager.Instance.Play("電車通過1");

        // 光で飛ばす
        yield return DOTween.Sequence().Append(DOTween.To(() => 1, (float x) => light2D.intensity = x, 4f, 5f)).WaitForCompletion();
        yield return new WaitForSeconds(2.5f);
        BgmManager.Instance.Stop();

        days++;

        // 社畜シーンへ切り替え
        SceneManager.LoadScene("社畜Scene");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
