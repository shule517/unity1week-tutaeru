using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Speech
{
    public string[] texts;
    public float audioPitch;
}

public class TextManager : SingletonMonoBehaviour<TextManager>
{
    [SerializeField] public Text text;
    public List<Speech> speechTexts = new List<Speech>();
    public bool IsTalking = false;

    void Start()
    {
        // 次のシーンでも破棄しない
        DontDestroyOnLoad(transform.parent.gameObject);
        DontDestroyOnLoad(gameObject);

        text = GameObject.FindWithTag("BaseSceneText").GetComponent<Text>();
        StartCoroutine(xxxx());
    }

    private IEnumerator xxxx()
    {
        //foreach (var text in speechTexts)
        while(true)
        {
            Debug.Log("xxxx - start");
            Debug.Log("WaitUntil speechTexts.Count:" + speechTexts.Count);
            IsTalking = false;
            yield return new WaitUntil(() => speechTexts.Count > 0);
            IsTalking = true;

            var speech = speechTexts.First();
            speechTexts.RemoveAt(0);

            foreach (var text in speech.texts)
            {
                yield return TalkText(text, speech.audioPitch);
                yield return new WaitUntil(() => Input.GetButtonDown("決定"));
                yield return null;
                TextManager.Instance.Assign("");
                yield return new WaitForSeconds(0.8f);
            }
        }
    }

    public IEnumerator Speech2(string str, float audioPitch = 1f)
    {
        IsTalking = true;

        yield return TalkText(str, audioPitch);
        yield return new WaitUntil(() => Input.GetButtonDown("決定"));
        yield return null;
        TextManager.Instance.Assign("");
        yield return new WaitForSeconds(0.8f);
        IsTalking = false;
    }

    public IEnumerator Speech2(string[] strs, float audioPitch = 1f)
    {
        IsTalking = true;
        foreach (string str in strs)
        {
            yield return TalkText(str, audioPitch);
        }
        IsTalking = false;
    }

    public void Speech(string str, float audioPitch = 1f)
    {
        speechTexts.Add(new Speech() { texts = new string[] { str }, audioPitch = audioPitch });
        //StartCoroutine(TalkText(str, audioPitch));
    }

    public void Assign(string str)
    {
        text.text = str;
    }

    void Append(string str)
    {
        text.text += str;
    }

    public IEnumerator TalkText(string talkingText, float audioPitch = 1f)
    {
        int messageCount = 0;
        Assign("");

        float minPitch = audioPitch - 0.02f;
        float maxPitch = audioPitch + 0f;

        foreach (var charactor in talkingText)
        {
            var str = charactor.ToString();
            Append(str);

            // 空文字は音を鳴らさない
            if (String.IsNullOrEmpty(str.Trim()))
            {
                yield return new WaitForSeconds(0.08f);
                continue;
            }

            messageCount++;
            if (audioPitch > 0.01f && messageCount % 2 == 0)
            {
                SeManager.Instance.Play("voice1", Random.Range(minPitch, maxPitch), 2);
            }

            yield return new WaitForSeconds(0.04f);
        }
    }
}
