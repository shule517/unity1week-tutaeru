using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenario日記 : MonoBehaviour
{
    public Text text;
    public GameObject kamihikouki;
    public GameObject nikki;

    IEnumerator Nikki()
    {
        // 紙ひこうきは、後から登場
        kamihikouki.SetActive(false);

        text.text = "";
        yield return new WaitForSeconds(0.8f);

        var writeText = @"ひさびさに日記を書く。
仕事について
考えぐちゃぐちゃになっていた。
その後いろいろなゴタゴタがあって
何も進んでいない。
最近死んでいた
自分と向きあわないと
本当の意味で死んでしまう。";
//         var writeText = @"9/20(月) 22:56〜
// ひさびさに日記を書く。
// やく１ヶ月ぶりの日記。
// 6月っくらいから
// 仕事について考えぐちゃぐちゃになっていた。
// その後いろいろなゴタゴタがあって
// 何も進んでいない。
// 最近死んでいた
// 自分と向きあわないと
// 本当の意味で死んでしまう。";

// ひとに合わせすぎなくても いいんだよ。
// ぜんぶ みんなに合わせなくて いいんだよ。


        foreach (char str in writeText.ToCharArray())
        {
            text.text += str;
            if (str != ' ' && str != '\n')
            {
                SeManager.Instance.Play("voice1", Random.Range(0.95f, 1f));
            }
            yield return new WaitForSeconds(0.15f);
        }
    
    
        yield return new WaitForSeconds(15000f);
    }

    IEnumerator Kamihikouki()
    {
        // 紙ひこうきは、後から登場
        kamihikouki.SetActive(false);

        text.text = "";
        yield return new WaitForSeconds(0.8f);

        var writeText = @"あの頃の自分へ

ひとりでいることは
わるいことじゃないんだよ。
      
でも、大丈夫！
      
おたがいのこと 分かり合える人に
これから 出会えるから";

// ひとに合わせすぎなくても いいんだよ。
// ぜんぶ みんなに合わせなくて いいんだよ。


        foreach (char str in writeText.ToCharArray())
        {
            text.text += str;
            if (str != ' ' && str != '\n')
            {
                SeManager.Instance.Play("voice1", Random.Range(0.95f, 1f));
            }
            yield return new WaitForSeconds(0.15f);
        }
        // float time = 0.15f * writeText.Length;
        // text.DOText(writeText, time).SetEase(Ease.Linear);

        // yield return new WaitForSeconds(time + 0.8f);

        SeManager.Instance.Stop();
        yield return new WaitForSeconds(0.8f);

        SeManager.Instance.Play("紙を破く1");
        
        // 紙ひこうきを折る
        yield return new WaitForSeconds(1f);
        SeManager.Instance.Play("紙を広げる2");
        yield return new WaitForSeconds(1f);


        SeManager.Instance.Play("紙を広げる1");

        yield return new WaitForSeconds(0.7f - 0.15f); // 一文字増やしたので+0.15f
        var ottaText = @"
      

わるいことじゃないんだよ。
      
でも、大丈夫！
      
おたがいのこと 分かり合える人に
これから 出会えるから";
        text.text = ottaText;

        yield return new WaitForSeconds(2f);

        SeManager.Instance.Play("紙を広げる1");
        yield return new WaitForSeconds(2f);
        
        // 紙ひこうき完成
        SeManager.Instance.Play("紙を広げる1");
        text.text = ottaText = "";
        nikki.SetActive(false);
        kamihikouki.SetActive(true);

        yield return new WaitForSeconds(2f);

        yield return TextManager.Instance.Speech2("\n… … …");
        yield return TextManager.Instance.Speech2("\nあの頃の自分に 届いたらいいのにな…");
        // yield return TextManager.Instance.Speech2("\… … … そろそろ寝よ");

        GameManager.Instance.hasKamihikouki = true; // 紙ひこうきを作った
        SceneManager.LoadScene("部屋Scene");

        yield return null;    }

    IEnumerator Start()
    {
        yield return Nikki();
        yield return Kamihikouki();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
