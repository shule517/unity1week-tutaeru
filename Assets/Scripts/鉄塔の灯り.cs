using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using DG.Tweening;
using UnityEngine;

public class 鉄塔の灯り : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var spriteChildren = GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in spriteChildren)
        {
            var color = spriteRenderer.color;
            DOTween.Sequence().Append(spriteRenderer.DOColor(new UnityEngine.Color(color.r, color.g, color.b, 0f), 2f).SetDelay(0.5f)).SetLoops(-1, LoopType.Yoyo);
        }

        var lightChildren = GetComponentsInChildren<UnityEngine.Rendering.Universal.Light2D>();
        foreach (var light2D in lightChildren)
        {
            DOTween.Sequence().Append(DOTween.To(() => 1, (float x) => light2D.intensity = x, 0f, 2f).SetDelay(0.5f)).SetLoops(-1, LoopType.Yoyo);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
