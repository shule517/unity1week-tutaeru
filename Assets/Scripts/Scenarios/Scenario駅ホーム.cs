using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenario駅ホーム : MonoBehaviour
{
    public GameObject player;
    public float minX;
    public float maxX;

    public GameObject doorLeft;
    public GameObject doorRight;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f); // BaseScene読み込み待ち

        BgmManager.Instance.Play("audiostock_electronica");

        yield return new WaitForSeconds(2f);

        // TODO: 電車のドアの前まで行ったら ドアが開くようにしたい
        SeManager.Instance.Play("電車のドアが開く1");
        var sequence_left = DOTween.Sequence();
        sequence_left.Append(doorLeft.transform.DOLocalMove(new Vector3(0.135f, 0f, 0f), 1.3f).SetEase(Ease.Linear));
        sequence_left.Append(doorLeft.transform.DOLocalMove(new Vector3(0.18f, 0f, 0f), 1.2f).SetEase(Ease.Linear));

        var sequence_right = DOTween.Sequence();
        sequence_right.Append(doorRight.transform.DOLocalMove(new Vector3(-0.135f, 0f, 0f), 1.3f).SetEase(Ease.Linear));
        sequence_right.Append(doorRight.transform.DOLocalMove(new Vector3(-0.18f, 0f, 0f), 1.2f).SetEase(Ease.Linear));
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.transform.position.x < minX)
        //{
        //    SceneManager.LoadScene("踏切Scene");
        //}

        //if (maxX < player.transform.position.x)
        //{
        //    SceneManager.LoadScene("しみじみエンディングScene");
        //}
    }
}
