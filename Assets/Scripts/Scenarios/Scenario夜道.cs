using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Scenario夜道 : MonoBehaviour
{
    public GameObject player;
    public float minX;
    public float maxX;
    public GameObject globalLight;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        // ライトを点ける or 消す
        yield return new WaitForSeconds(0.8f);
        SeManager.Instance.Play("カーソル移動8");
        globalLight.SetActive(true);
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
