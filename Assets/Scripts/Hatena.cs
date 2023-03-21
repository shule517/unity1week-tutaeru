using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Hatena : MonoBehaviour
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
        spriteRender.enabled = false;
        Player.Instance.IsPlayable = false; // 操作できないようにする
        Player.Instance.NowAnime = PlayerAnimation;
        foreach (var text in speechTexts)
        {
            yield return TextManager.Instance.Speech2(text);
        }
        Player.Instance.IsPlayable = true;

        // ？を表示
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
