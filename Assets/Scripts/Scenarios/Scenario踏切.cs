using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenario踏切 : MonoBehaviour
{
    public GameObject player;
    public GameObject train;
    public float minX;
    public float maxX;

    // Start is called before the first frame update
    void Start()
    {
        //var position = train.transform.position;
        //train.transform.DOMove(new Vector3(25f, position.y, position.z), 20f).SetEase(Ease.OutQuad).SetDelay(3f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.transform.position.x < minX)
        //{
        //    SceneManager.LoadScene("よじまScene");
        //}
        if (maxX < player.transform.position.x)
        {
            SceneManager.LoadScene("駅ホームScene");
        }
    }
}
