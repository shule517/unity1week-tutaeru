using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

public class Player : SingletonMonoBehaviour<Player>
{
    /// <summary> 移動操作を受け付けるタッチエリア </summary>
    [SerializeField]
    private DragHandler _moveController;

    /// <summary> 移動速度（m/秒） </summary>
    [SerializeField]
    private float _movePerSecond = 7f;

    /// <summary> 移動操作としてタッチ開始したスクリーン座標 </summary>
    private Vector2 _movePointerPosBegin;
    private float dragHorizontal = 0;

    private Vector3 _moveVector;

    public AudioClip soundWalk;
    public AudioClip soundTalk;
    AudioSource audioSourceSeWalk;
    // AudioSource audioSourceTalk;
    Animator animator;
    private SpriteRenderer spriteRenderer;
    Tween tween = null;

    // キャラクターが操作可能か
    public bool IsPlayable { get; set; } = true;

    public float MinMoveX = -100f;
    public float MaxMoveX = 100f;

    // アニメーションの設定
    public string NowAnime
    {
        get { return this.nowAnime; }
        set { this.nowAnime = value; Debug.Log("nowAnime = " + value); }
    }

    public bool FlipX
    {
        get { return spriteRenderer.flipX; }
        set { spriteRenderer.flipX = value; }
    }

    void Start()
    {
        audioSourceSeWalk = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary> 起動時 </summary>
    private void Awake()
    {
        if (_moveController)
        {
            _moveController.OnBeginDragEvent += OnBeginDragMove;
            _moveController.OnDragEvent += OnDragMove;
            _moveController.OnEndDragEvent += OnEndDragMove;
        }
    }

    /// <summary> 更新処理 </summary>
    private void Update()
    {
        if (!IsPlayable) { return; }

        UpdateMove(_moveVector);

        float horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 0 && dragHorizontal != 0)
        {
            horizontal = dragHorizontal;
        }
        //float vertical = Input.GetAxisRaw("Vertical");
        _moveVector = new Vector3(0f, 0f, 0f);

        if (horizontal < 0)
        {
            _moveVector.x = -1f;
            //_moveVector = new Vector3(-1, 0f, 0f);
            spriteRenderer.flipX = true;
            nowAnime = walkAnime;
        }
        else if (0 < horizontal)
        {
            _moveVector.x = 1f;
            //_moveVector = new Vector3(1, 0f, 0f);
            spriteRenderer.flipX = false;
            nowAnime = walkAnime;
        }
        // else if (vertical < 0)
        // {
        //     _moveVector.y = -0.1f;
        //     //_moveVector = new Vector3(0f, -0.1f, 0f);
        //     //spriteRenderer.flipX = true;
        //     nowAnime = walkAnime;
        // }
        // else if (0 < vertical)
        // {
        //     _moveVector.y = 0.1f;
        //     //_moveVector = new Vector3(0f, 0.1f, 0f);
        //     //spriteRenderer.flipX = false;
        //     nowAnime = walkAnime;
        // }
        else
        {
            _moveVector = new Vector3(0, 0f, 0f);
            if (nowAnime == walkAnime)
            {
                nowAnime = standAnime;
            }
        }

        //// スペースキーの取得
        //if (Input.GetKeyDown(KeyCode.Space)) {
        //    //音を鳴らす
        //    // audioSourceTalk.PlayOneShot();
        //    // audioSourceTalk.Play();
        //    audioSourceSeWalk.PlayOneShot(soundTalk);
        //}

        //// クリックしたオブジェクトまで移動
        //if (Input.GetMouseButtonDown(0)) {
        //    if (tween != null && tween.IsPlaying())
        //    {
        //        // 移動中はスキップ
        //        return;
        //    }

        //    GameObject clickedGameObject = getClickObject();

        //    if (clickedGameObject != null) {
        //        // 歩き始めた
        //        audioSourceSeWalk.PlayOneShot(soundWalk);
        //        nowAnime = walkAnime;

        //        // 移動を中断
        //        transform.DOKill();

        //        // 移動処理
        //        var vector = clickedGameObject.transform.position - transform.position;
        //        var moveDistance = vector.x;
        //        var second = Math.Abs(moveDistance / _movePerSecond);

        //        var artwork = clickedGameObject.GetComponent<Artwork>();
        //        var artist_id = artwork.artworkInfo.artist.screen_name;

        //        Debug.Log($"artist: {artist_id}");

        //        // キャラの向きを変える
        //        fitPlayerDirection(vector);

        //        // 移動
        //        tween = transform.DOLocalMove(new Vector3(clickedGameObject.transform.position.x, transform.position.y, 0), second)
        //            .SetEase(Ease.Linear)
        //            .OnComplete(() => {
        //                // 歩き終わった
        //                audioSourceSeWalk.Stop();
        //                nowAnime = standAnime;

        //                SceneParameter.LoadScene(artist_id);
        //            });
        //    } 
        //}
    }

    //// 左クリックしたオブジェクトを取得する関数(2D)
    //// https://rikoubou.hatenablog.com/entry/2016/01/29/163518
    //private GameObject getClickObject() {
    //    // 左クリックされた場所のオブジェクトを取得
    //    if(Input.GetMouseButtonDown(0)) {
    //        Vector2 tapPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        Collider2D collition2d = Physics2D.OverlapPoint(tapPoint);
    //        if (collition2d) {
    //            return collition2d.transform.gameObject;
    //        }
    //    }
    //    return null;
    //}

    private void FixedUpdate()
    {
        if (nowAnime != oldAnime)
        {
            Debug.Log("nowAnime != oldAnime -> " + nowAnime);
            oldAnime = nowAnime;
            animator.Play(nowAnime);

            if (nowAnime == walkAnime)
            {
                audioSourceSeWalk.Play();
                //BgmManager.Instance.Play("se_walk1");
            }
            else
            {
                audioSourceSeWalk.Stop();
                //BgmManager.Instance.Stop();
            }
        }
    }

    ////////////////////////////////////////////////////////////
    /// 移動操作
    ////////////////////////////////////////////////////////////
    #region Move

    public static string standAnime = "PlayerStand";
    public static string standKageNashiAnime = "PlayerStandKageNashi";
    public static string walkAnime = "PlayerWalk";
    public static string uruuruAnime = "PlayerUruuru";
    public static string namidaAnime = "PlayerNamida";

    string nowAnime = standAnime;
    string oldAnime = standAnime;

    /// <summary> ドラッグ操作開始（移動用） </summary>
    private void OnBeginDragMove(PointerEventData eventData)
    {
        // タッチ開始位置を保持
        _movePointerPosBegin = eventData.position;

        //// 歩き始めた
        //audioSourceSeWalk.Play();
        nowAnime = walkAnime;

        //// 移動を中断
        //transform.DOKill();
    }

    /// <summary> ドラッグ操作中（移動用） </summary>
    private void OnDragMove(PointerEventData eventData)
    {
        // タッチ開始位置からのスワイプ量を移動ベクトルにする
        var vector = eventData.position - _movePointerPosBegin;
        //_moveVector = new Vector3(vector.x, 0f, 0f); // 左右だけに移動できる

        if (vector.x < 0)
        {
            dragHorizontal = -1f;
        }
        else if(0 < vector.x)
        {
            dragHorizontal = 1f;
        }

        //// キャラの向きを変える
        //fitPlayerDirection(vector);
    }

    //private void fitPlayerDirection(Vector2 vector)
    //{
    //    if (vector.x > 0)
    //    {
    //        // 右に移動して左向いているときは反転
    //        if (transform.localScale.x < 0)
    //        {
    //            reversePlayerDirection();
    //        }
    //    }
    //    else if (vector.x < 0)
    //    {
    //        // 左に移動して右向いているときは反転
    //        if (transform.localScale.x > 0)
    //        {
    //            reversePlayerDirection();
    //        }
    //    }
    //}

    //private void reversePlayerDirection()
    //{
    //    // キャラの向きを反転
    //    Vector3 localScale = transform.localScale;
    //    localScale.x = localScale.x * -1;
    //    transform.localScale = localScale;
    //}

    private void UpdateMove(Vector3 vector)
    {
        // 現在向きを基準に、入力されたベクトルに向かって移動
        transform.position += transform.rotation * vector.normalized * _movePerSecond * Time.deltaTime;

        // 画面外にはみでないようにする
        if (transform.position.x < MinMoveX)
        {
            transform.position = new Vector3(MinMoveX, transform.position.y, transform.position.z);
        }

        if (MaxMoveX < transform.position.x)
        {
            transform.position = new Vector3(MaxMoveX, transform.position.y, transform.position.z);
        }
    }

    /// <summary> ドラッグ操作終了（移動用） </summary>
    private void OnEndDragMove(PointerEventData eventData)
    {
        // 移動ベクトルを解消
        //_moveVector = Vector3.zero;
        dragHorizontal = 0;

        // 歩き終わった
        //audioSourceSeWalk.Stop();
        nowAnime = standAnime;
    }
    #endregion
}
