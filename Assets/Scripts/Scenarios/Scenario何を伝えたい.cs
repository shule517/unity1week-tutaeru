using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using System.Linq;
using UnityEngine.UI;

public class Scenario何を伝えたい : MonoBehaviour
{
    // public UnityEngine.UI.Text text;
    // public GameObject hitokage;
    public Light2D light2D;
    // public static int days = 0;
    // public GameObject kaishaWorld;
    // public GameObject heyaWorld;
    public Text gameTitleText;
    // public GameObject kamiHikouki;
    // private bool typingbgm = true;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        StartCoroutine(Title());

        yield return null;
    }

    private IEnumerator Title()
    {
        // タイトルをじわじわ出す

        gameTitleText.text = "おしまい";
        // gameTitleText.text = "あのころの じぶんへ";
        gameTitleText.color = Color.black;
        gameTitleText.DOColor(Color.white, 8.0f).SetEase(Ease.Linear);
        yield return DOTween.Sequence().Append(DOTween.To(() => 0f, (float x) => light2D.intensity = x, 1f, 5f).SetEase(Ease.InQuad));

        yield return new WaitForSeconds(0.5f);
        
        // BgmManager.Instance.Play("audiostock_Indium");
        // BgmManager.Instance.Play("MusMus-BGM-157-original");
        // BgmManager.Instance.audioSource.volume = 0.5f;

        var endTime = 124.2f - 3f - 0.5f;

        endTime -= 16.8f;
        yield return new WaitForSeconds(16.8f);

        // endTime -= 8.7f;
        // yield return new WaitForSeconds(8.7f);
        gameTitleText.text = "あなたは じぶんへ 何を伝えたいですか？";

        yield return new WaitForSeconds(endTime);
        // 2分4秒(124秒)から再生したい
        gameTitleText.text = @"人生たいへんなことは たくさんあります
みなさん どうか自分らしく 生きられますように…";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
