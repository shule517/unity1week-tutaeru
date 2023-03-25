using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class DeskLight : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D deskLight;

    IEnumerator Start()
    {
        var inital_value = deskLight.intensity;
        yield return DOTween.Sequence()
            .Append(DOTween.To(() => inital_value, (float x) => deskLight.intensity = x, inital_value - 0.3f, 0.1f).SetEase(Ease.Linear))
            // .Append(DOTween.To(() => 0f, (float x) => deskLight.intensity = x, 0.2f, 3f).SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Yoyo);
            // .SetLoops(-1, LoopType.Restart);
    }

    void Update()
    {
    }
}
