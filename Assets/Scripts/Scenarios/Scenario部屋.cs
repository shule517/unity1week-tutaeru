using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Scenario部屋 : MonoBehaviour
{
    public GameObject player;
    public float minX;
    public float maxX;
    public GameObject globalLight;
    public Light2D light2D;
    public SpriteRenderer playerSpriteRender;
    public GameObject kamihikouki;
    public GameObject deskLight;
    public GameObject camera;
    public GameObject hatenaDesk;
    public GameObject hatenaKamihikouki;
    public GameObject hatenaMadoAkarui;
    public GameObject hatenaMadoKurai;
    public GameObject hatenaIllustAkarui;
    public GameObject hatenaIllustKurai;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // // TODO: デバッグ フラグいじってます
        GameManager.Instance.hasKamihikouki = true;
        // GameManager.Instance.watchedEnding1 = true;
        // // TODO: 後で消す
        yield return new WaitForSeconds(0.4f);

        // 暗い時の窓のメッセージを有効
        hatenaMadoAkarui.SetActive(false);
        hatenaMadoKurai.SetActive(true);
        // 暗い時の絵のメッセージを有効
        hatenaIllustAkarui.SetActive(false);
        hatenaIllustKurai.SetActive(true);

        if (!GameManager.Instance.hasKamihikouki)
        {
            // 初日
            kamihikouki.SetActive(false);
            StartCoroutine(Shonichi());
        }
        else
        {
            // 紙ひこうきエンディング
            Player.Instance.IsPlayable = false; // キャラの操作NG
            kamihikouki.SetActive(true);
            deskLight.SetActive(true);
            Player.Instance.gameObject.transform.position = new Vector3(-29f, -3.68f, 0f); // 机の前まで移動
            Player.Instance.NowAnime = Player.ushiroAnime;
            playerSpriteRender.color = new Color(1f, 1f, 1f, 1f); // 登場させる
            camera.transform.position = new Vector3(-29f, -1.17f, -10f); // カメラの位置をあわせる ゆれると酔うので
            hatenaDesk.SetActive(false); // つくえの ! を消す
            hatenaKamihikouki.SetActive(true); // 紙ひこうき ! をつける

            StartCoroutine(KamihikoukiEnding());
        }

        yield return null;
    }

    IEnumerator Shonichi()
    {
        // キャラを操作させない
        Player.Instance.IsPlayable = false;

        // 暗転から復帰
        playerSpriteRender.color = new Color(1f, 1f, 1f, 0f);
        yield return DOTween.Sequence().Append(DOTween.To(() => 0f, (float x) => light2D.intensity = x, 1f, 3f).SetEase(Ease.InQuad)).WaitForCompletion();

        // ドアを閉める
        playerSpriteRender.DOFade(1.0f, 1f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(0.4f);
        SeManager.Instance.Play("ドアを閉める2");

        yield return new WaitForSeconds(2.2f);

        // キャラの操作OK
        Player.Instance.IsPlayable = true;

        if (!GameManager.Instance.watchedEnding1)
        {
            // 初日だけライトを点ける。二日目は部屋が暗いまま 進む。
            SeManager.Instance.Play("電源ON-Air_Conditioner01-01");
            globalLight.SetActive(true);

            // 明るい時の窓のメッセージを有効
            hatenaMadoAkarui.SetActive(true);
            hatenaMadoKurai.SetActive(false);

            // 明るい時の絵のメッセージを有効
            hatenaIllustAkarui.SetActive(true);
            hatenaIllustKurai.SetActive(false);

            yield return new WaitForSeconds(0.4f);
        }

        // BGMスタート
        BgmManager.Instance.Play("audiostock_electronica");

        yield return null;
    }

    IEnumerator KamihikoukiEnding()
    {
        // BGMスタート
        BgmManager.Instance.Play("audiostock_electronica");

        yield return new WaitForSeconds(1.3f);

        yield return TextManager.Instance.Speech2("やっぱり ひとりの時間って 大切。");
        yield return TextManager.Instance.Speech2("自分と向き合うことで 前に進めてる気がする");
        yield return TextManager.Instance.Speech2("… … …");
        yield return TextManager.Instance.Speech2("…そろそろ寝よ");

        // ライトを消す
        yield return new WaitForSeconds(0.8f);
        SeManager.Instance.Play("カーソル移動8");
        deskLight.SetActive(false);

        // キャラの操作OK
        yield return new WaitForSeconds(0.8f);
        Player.Instance.IsPlayable = true;
        Player.Instance.NowAnime = Player.standAnime;

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
