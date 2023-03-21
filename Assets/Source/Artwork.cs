//using UnityEngine;
//using System.Collections;
//using UnityEngine.Networking;
//using UnityEngine.UI;
//using DG.Tweening;

//public class Artwork : MonoBehaviour
//{
//    public string mediaUrl;
//    public API.Artworks artworkInfo;

//    public int height;

//    [SerializeField] private RawImage _image;

//    // Start is called before the first frame update
//    IEnumerator Start()
//    {
//        var seq = DOTween.Sequence();
//        seq.Append(transform.DOLocalMoveY(0.5f, 2.5f).SetEase(Ease.InOutSine).SetRelative());//上へ
//        seq.Append(transform.DOLocalMoveY(-0.5f, 2.5f).SetEase(Ease.InOutSine).SetRelative());//.SetDelay(2.0f));//2秒止まって下へ
//        seq.SetLoops(-1);//繰り返し

//        Debug.Log("mediaUrl: " + mediaUrl);
//        UnityWebRequest www = UnityWebRequestTexture.GetTexture(mediaUrl);

//        //画像を取得できるまで待つ
//        yield return www.SendWebRequest();

//        if (www.isNetworkError || www.isHttpError)
//        {
//            Debug.Log("error");
//            Debug.Log(www.error);
//        }
//        else
//        {
//            // //取得した画像のテクスチャをRawImageのテクスチャに張り付ける
//            // _image.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

//            Debug.Log("success");
//            var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
//            // spriteRenderer.sprite = sprite;

//            var texture = ((DownloadHandlerTexture)www.downloadHandler).texture;

//            // 新しいスプライトのメッシュタイプをFullRectにして、9スライスに対応できるようにする
//            // 9スライスとはいってもborderはデフォルトの(0, 0, 0, 0)なので、画像全体がサイズに合わせて伸縮することになる
//            Sprite sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height / 900.0f * 700.0f, 0, SpriteMeshType.FullRect);


//            height = texture.height;


//            // Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
//            // Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
//            spriteRenderer.sprite = sprite;

//            // GetComponent<Renderer>().material.mainTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
