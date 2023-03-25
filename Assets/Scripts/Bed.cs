using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Bed : MonoBehaviour
{
    public List<string> speechTexts;
    public float interval = 1.5f;
    public string PlayerAnimation = Player.standAnime;
    public GameObject globalLight;
    public Light2D kagayakiHikouki;
    public GameObject kamiHikouki;
    private SpriteRenderer spriteRender;
    private bool isLooping = false;

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
        if (isLooping && Input.GetButtonDown("決定"))
        {
            isLooping = false;
        }

        if (spriteRender.enabled && Input.GetButtonDown("決定"))
        {
            if (!TextManager.Instance.IsTalking)
            {
                StartCoroutine(Speech());
            }
        }
    }

    IEnumerator Speech()
    {
        // ! を消す
        spriteRender.enabled = false;

        // 操作できないようにする
        Player.Instance.IsPlayable = false;
        Player.Instance.NowAnime = PlayerAnimation;

        // テキスト
        var speechTexts = new List<string>() { "そろそろ 寝ようかな…" };
        foreach (var text in speechTexts)
        {
            yield return TextManager.Instance.Speech2(text.Replace("/", "\n"));
        }

        TextManager.Instance.Assign("もう寝ますか？\n   ▶ はい 　 いいえ");

        var isYes = true;
        // var 
        isLooping = true;
        while (isLooping)
        {
            // 選択肢切り替え
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (!isYes && horizontal < 0)
            {
                // ピッ
                SeManager.Instance.Play("voice1");
                TextManager.Instance.Assign("もう寝ますか？\n   ▶ はい 　 いいえ");
                isYes = true;
            }
            else if (isYes && 0 < horizontal)
            {
                // ピッ
                SeManager.Instance.Play("voice1");
                TextManager.Instance.Assign("もう寝ますか？\n　    はい ▶ いいえ");
                isYes = false;
            }
            yield return new WaitForSeconds(0.1f);
        }

        // 決定音
        // SeManager.Instance.Play("voice1");
        // SeManager.Instance.Play("決定ボタンを押す28");
        // SeManager.Instance.Play("Motion-Pop08-1");
        SeManager.Instance.Play("Motion-Pop08-2");
        
        yield return new WaitForSeconds(0.5f);

        if (isYes)
        {
            // テキスト
            var yesSpeechTexts = new List<string>() { "…  …  …  " };
            foreach (var text in yesSpeechTexts)
            {
                yield return TextManager.Instance.Speech2(text.Replace("/", "\n"));
            }

            // ライトを点消す と BGMも止まる
            if (globalLight.active)
            {
                SeManager.Instance.Play("電源ON-Air_Conditioner01-01");
                globalLight.SetActive(false);
            }
            BgmManager.Instance.Stop();
            yield return new WaitForSeconds(1.0f);

            // ふとんに入る
            SeManager.Instance.Play("衣擦れ1");
            Player.Instance.NowAnime = Player.ushiroAnime;

            yield return new WaitForSeconds(1.0f);

            // Playerを無効にすると暗転する（Playerにライトがついてるから）
            Player.Instance.gameObject.SetActive(false);
            // TODO: 寝るSE

            yield return new WaitForSeconds(1.0f);

            if (false)
            {
                // 1日目の場合
                BgmManager.Instance.Play("Clock-Second_Hand02-1(Dry-Loop)");

                yield return new WaitForSeconds(4.0f);
                SceneManager.LoadScene("社畜Scene");
            }
            else
            {
                // 2日目の場合
                yield return new WaitForSeconds(1.5f);

                // ぼやん
                yield return DOTween.Sequence().Append(DOTween.To(() => 0f, (float x) => kagayakiHikouki.intensity = x, 0.2f, 3f).SetEase(Ease.Linear)).WaitForCompletion();
                yield return DOTween.Sequence().Append(DOTween.To(() => 0.2f, (float x) => kagayakiHikouki.intensity = x, 0f, 2f).SetEase(Ease.Linear)).WaitForCompletion();

                // ぼやーん(前半)
                yield return DOTween.Sequence().Append(DOTween.To(() => 0f, (float x) => kagayakiHikouki.intensity = x, 1f, 2f).SetEase(Ease.Linear)).WaitForCompletion();

                // よいん
                BgmManager.Instance.Play("audiostock_891509_sample");
                BgmManager.Instance.audioSource.volume = 0;
                BgmManager.Instance.audioSource.DOFade(endValue: 1f, duration: 7.5f);

                // ぼやーん(後半)
                yield return DOTween.Sequence().Append(DOTween.To(() => 1f, (float x) => kagayakiHikouki.intensity = x, 0f, 1.5f).SetEase(Ease.Linear)).WaitForCompletion();

                // ぱーっ！！（前半）
                yield return DOTween.Sequence().Append(DOTween.To(() => 0f, (float x) => kagayakiHikouki.intensity = x, 100f, 4f).SetEase(Ease.OutQuad));
                yield return new WaitForSeconds(2.5f);
                // ぱーっ！！（後半）
                yield return DOTween.Sequence().Append(DOTween.To(() => 7f, (float x) => kagayakiHikouki.pointLightOuterRadius = x, 150f, 3f).SetEase(Ease.OutQuad)).WaitForCompletion();

                // ライトの強さをもとに戻す
                // kagayakiHikouki.intensity = 2.0f;

                // 紙ひこうきが飛んでいく
                kamiHikouki.transform.localScale = new Vector3(4.0f, 4.0f, 1.0f); // 巨大化
                kamiHikouki.transform.localPosition = new Vector3(-30.0f, -5.5f, 0.0f); // 右上に移動

                DOTween.Sequence()
                .Append(kamiHikouki.transform.DOMoveX(-50f, 4.0f))
                .Join(kamiHikouki.transform.DOMoveY(-4f, 4.0f).SetEase(Ease.OutQuad));

                yield return new WaitForSeconds(3.5f);

                // 紙ひこうきEDへ
                SceneManager.LoadScene("紙ひこうきScene");
            }

            // 操作可能にする
            // Player.Instance.IsPlayable = true;
        }
        else
        {
            // テキスト
            var yesSpeechTexts = new List<string>() { "…  …  …  ", "寝たら 明日になっちゃう…", "まだ今日を 終わらせたくない。" };
            foreach (var text in yesSpeechTexts)
            {
                yield return TextManager.Instance.Speech2(text.Replace("/", "\n"));
            }

            // 操作可能にする
            Player.Instance.IsPlayable = true;

            // ! を表示
            yield return new WaitForSeconds(0.3f);
            spriteRender.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 近くに来たら表示
        spriteRender.enabled = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 離れたら非表示
        spriteRender.enabled = false;
    }
}
