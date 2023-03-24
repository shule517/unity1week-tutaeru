using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Bed : MonoBehaviour
{
    public List<string> speechTexts;
    public float interval = 1.5f;
    public string PlayerAnimation = Player.standAnime;
    private SpriteRenderer spriteRender;

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

        TextManager.Instance.Assign("寝ますか？   ▶ はい 　 いいえ");

        var isYes = true;
        var isLooping = true;
        while (isLooping)
        {
            // 次に進む
            if (Input.GetButtonDown("決定"))
            {
                isLooping = false;
            }

            // 選択肢切り替え
            float horizontal = Input.GetAxisRaw("Horizontal");
            if (!isYes && horizontal < 0)
            {
                // ピッ
                SeManager.Instance.Play("voice1");
                TextManager.Instance.Assign("寝ますか？   ▶ はい 　 いいえ");
                isYes = true;
            }
            else if (isYes && 0 < horizontal)
            {
                // ピッ
                SeManager.Instance.Play("voice1");
                TextManager.Instance.Assign("寝ますか？      はい ▶ いいえ");
                isYes = false;
            }
            yield return new WaitForSeconds(0.1f);
        }


        if (isYes)
        {
            // テキスト
            var yesSpeechTexts = new List<string>() { "…  …  …  " };
            foreach (var text in yesSpeechTexts)
            {
                yield return TextManager.Instance.Speech2(text.Replace("/", "\n"));
            }

            // TODO: 暗転
            // TODO: 寝るSE
            yield return new WaitForSeconds(0.3f);

            SceneManager.LoadScene("社畜Scene");

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
