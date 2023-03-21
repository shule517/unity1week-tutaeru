using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class crossingSignalEvent : MonoBehaviour
{
    public List<string> speechTexts;
    public float interval = 1.5f;
    public string PlayerAnimation = Player.standAnime;

    public GameObject train;
    public GameObject player;
    public GameObject fumikiri;
    public AudioSource crossingSignalAudioSource;
    bool isEventStarted = false;

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
            // イベントは一回しか実行しない
            if (isEventStarted) { return; }
            spriteRender.enabled = false;

            StartCoroutine(StartEvent());
        }
    }

    //IEnumerator Speech()
    //{
    //    spriteRender.enabled = false;
    //    Player.Instance.IsPlayable = false; // 操作できないようにする
    //    Player.Instance.NowAnime = PlayerAnimation;
    //    foreach (var text in speechTexts)
    //    {
    //        yield return TextManager.Instance.Speech2(text);
    //    }
    //    Player.Instance.IsPlayable = true;

    //    // ？を表示
    //    yield return new WaitForSeconds(0.3f);
    //    spriteRender.enabled = true;
    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 近くに来たら表示
        if (!isEventStarted)
        {
            spriteRender.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 離れたら非表示
        spriteRender.enabled = false;
    }

    private IEnumerator StartEvent()
    {
        isEventStarted = true;

        // 操作できないようにする
        Player.Instance.IsPlayable = false;

        // 電車を動かす
        var position = train.transform.position;
        train.transform.DOMove(new Vector3(25f, position.y, position.z), 20f).SetEase(Ease.OutQuad).SetDelay(3f);
        SeManager.Instance.Play("電車停車");
        SeManager.Instance.audioSource.volume = 0f;
        SeManager.Instance.audioSource.DOFade(endValue: 1f, duration: 7.5f);

        // 踏切の前まで移動
        Player.Instance.NowAnime = Player.walkAnime;
        yield return player.transform.DOMove(new Vector3(-37.7f, player.transform.position.y, player.transform.position.z), 2f).WaitForCompletion();
        Player.Instance.NowAnime = Player.standAnime;

        // 電車が走り去るのを待つ
        yield return new WaitForSeconds(12f);
        // 踏切の音を止める
        crossingSignalAudioSource.Stop();
        // 踏切が開く アニメーション
        fumikiri.GetComponent<Animator>().Play("fumikiri_open");

        yield return new WaitForSeconds(1.5f);

        // 踏切を渡る
        Player.Instance.FlipX = true;
        player.GetComponent<Renderer>().sortingOrder = 7;
        Player.Instance.NowAnime = Player.walkAnime;
        yield return player.transform.DOMove(new Vector3(-39f, -2.22f, player.transform.position.z), 5f).WaitForCompletion();
        Player.Instance.NowAnime = Player.standAnime;

        // 歩けるようにする
        Player.Instance.IsPlayable = true;
        Player.Instance.MinMoveX = -39.7f;
        Player.Instance.MaxMoveX = 100f;

        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
    }

}

//{
//    public GameObject train;
//    public AudioSource crossingSignalAudioSource;
//    bool isEventStarted = false;

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        Debug.Log("OnTriggerEnter2D");

//        // イベントは一回しか実行しない
//        if (isEventStarted) { return; }

//        isEventStarted = true;
//        var position = train.transform.position;
//        train.transform.DOMove(new Vector3(25f, position.y, position.z), 20f).SetEase(Ease.OutQuad).SetDelay(3f);

//        SeManager.Instance.Play("電車停車");
//        SeManager.Instance.audioSource.volume = 0f;
//        SeManager.Instance.audioSource.DOFade(endValue: 1f, duration: 7.5f);

//        StartCoroutine(StopCrossingSigna());
//    }

//    private IEnumerator StopCrossingSigna()
//    {
//        yield return new WaitForSeconds(20f);
//        crossingSignalAudioSource.Stop();
//        yield return null;
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        Debug.Log("OnCollisionEnter2D");
//    }
//}
