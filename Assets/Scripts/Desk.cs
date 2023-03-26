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

        if (GameManager.Instance.watchedEnding1)
        {
            // 2日目は、日記を書く
            SceneManager.LoadScene("日記Scene");
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
