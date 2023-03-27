using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Desk : MonoBehaviour
{
    public List<string> speechTexts;
    public float interval = 1.5f;
    public string PlayerAnimation = Player.standAnime;
    public GameObject deskLight;
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
        if (spriteRender.enabled && Math.Abs(Player.Instance.gameObject.transform.position.x - this.transform.position.x) > 1.4f)
        {
            // 1.4以上はなれてても!が表示されてたらバグ
            // ! を消す
            spriteRender.enabled = false;
        }

        if (spriteRender.enabled && Input.GetButtonDown("決定"))
        {
            if (!TextManager.Instance.IsTalking)
            {
                StartCoroutine(Speech());
            }
        }

        if (isLooping && Input.GetButtonDown("決定"))
        {
            isLooping = false;
        }
    }

    IEnumerator Speech()
    {
        // ! を非表示
        spriteRender.enabled = false;

        // 操作できないようにする
        Player.Instance.IsPlayable = false;
        Player.Instance.NowAnime = PlayerAnimation;

        // ライトを点ける or 消す
        yield return new WaitForSeconds(0.8f);
        SeManager.Instance.Play("カーソル移動8");
        deskLight.SetActive(!deskLight.active);
        yield return new WaitForSeconds(0.8f);

        if (GameManager.Instance.watchedEnding1 && deskLight.active)
        {
            // 2日目は、日記を書く
            var speechTexts = new List<string>() { "気持ちが もやもやする日は 日記を書こうかな…" };
            foreach (var text in speechTexts)
            {
                yield return TextManager.Instance.Speech2(text.Replace("/", "\n"));
            }

            TextManager.Instance.Assign("日記を書きますか？\n   ▶ はい 　 いいえ");

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
                    TextManager.Instance.Assign("日記を書きますか？\n   ▶ はい 　 いいえ");
                    isYes = true;
                }
                else if (isYes && 0 < horizontal)
                {
                    // ピッ
                    SeManager.Instance.Play("voice1");
                    TextManager.Instance.Assign("日記を書きますか？\n　    はい ▶ いいえ");
                    isYes = false;
                }
                yield return new WaitForSeconds(0.1f);
            }

            // 決定音
            SeManager.Instance.Play("voice1");
            TextManager.Instance.Assign("");

            yield return new WaitForSeconds(0.5f);

            if (isYes)
            {
                SceneManager.LoadScene("日記Scene");
            }
            else
            {
                // Player.Instance.NowAnime = Player.standAnime;
            }
        }
        else
        {
            // 1日目の会話をすすめる
            if (deskLight.active)
            {
                foreach (var text in speechTexts)
                {
                    yield return TextManager.Instance.Speech2(text.Replace("/", "\n"));
                }
            }
        }

        // 操作できるようにする
        Player.Instance.IsPlayable = true;

        // ! を表示
        yield return new WaitForSeconds(0.3f);
        spriteRender.enabled = true;
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
