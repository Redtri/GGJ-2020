using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ButtonGear : MonoBehaviour, IPointerClickHandler
{
    private bool rightClick = false;
    private int index = 0;
    public void OnPointerClick(PointerEventData eventData)
    {
        index = transform.GetSiblingIndex();
        if(eventData.button == PointerEventData.InputButton.Right){
            rightClick = true;

            LensDistortion lens = null;            

            EffectManager.instance.postProcessVolume.profile.TryGet(out lens);

            DOVirtual.Float(0, -0.3f, 0.2f, (float value) => UpdateLens(value, lens)).SetLoops(2, LoopType.Yoyo);


        }
        else if (eventData.button == PointerEventData.InputButton.Left) {
            rightClick = false;
            EffectManager.instance.screenShake.Shake(0, 0.05f);
        }
        CharacterManager.instance.UpdateActorGearValues(index, rightClick);

    }

    private void UpdateLens(float value, LensDistortion lens)
    {
        lens.intensity.value = value;
    }
}
