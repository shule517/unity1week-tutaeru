using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Scenario部屋 : MonoBehaviour
{
    public GameObject player;
    public float minX;
    public float maxX;
    public GameObject globalLight;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.8f);

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
