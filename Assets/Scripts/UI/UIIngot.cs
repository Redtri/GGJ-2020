using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIIngot : MonoBehaviour
{
    public Image fixedIngot;
    public GameObject ingotSystemPrefab;
    public Transform where;

    public void AddIngot()
    {
        ParticleSystem tmp = Instantiate(ingotSystemPrefab, where).GetComponent<ParticleSystem>();
        tmp.Play(true);
        if(fixedIngot.color.a == 1f){
            //transform.DOScale(0f, tmp.main.startLifetime.constant/2).SetLoops(2, LoopType.Yoyo);
            fixedIngot.DOFade(0f, tmp.main.startLifetime.constant/2).SetLoops(2, LoopType.Yoyo);
        }
        tmp.GetComponent<ParticleSystemRenderer>().material.DOFloat(0f, "_Heat", tmp.main.startLifetime.constant);
    }
}
