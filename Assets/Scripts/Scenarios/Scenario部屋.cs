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

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // 暗転から復帰
        playerSpriteRender.color = new Color(1f, 1f, 1f, 0f);
        yield return DOTween.Sequence().Append(DOTween.To(() => 0f, (float x) => light2D.intensity = x, 1f, 3f).SetEase(Ease.InQuad)).WaitForCompletion();

        // ドアを閉める
        playerSpriteRender.DOFade(1.0f, 1f).SetEase(Ease.InQuad);
        yield return new WaitForSeconds(0.4f);
        SeManager.Instance.Play("ドアを閉める2");

        yield return new WaitForSeconds(2.2f);

        // ライトを点ける
        SeManager.Instance.Play("電源ON-Air_Conditioner01-01");
        globalLight.SetActive(true);

        yield return new WaitForSeconds(0.4f);

        // BGMスタート
        BgmManager.Instance.Play("audiostock_electronica");

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.transform.position.x  < minX)
        //{
        //    SceneManager.LoadScene("よじまScene");
        //}
        // if (maxX < player.transform.position.x)
        // {
        //     SceneManager.LoadScene("よじまScene");
        // }
    }
}
