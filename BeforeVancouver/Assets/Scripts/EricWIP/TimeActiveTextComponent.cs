using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeActiveTextComponent : MonoBehaviour
{
    public TextMeshPro textMesh;
    public float startAlphaValue;
    public float endAlphaValue;
    public float fadeDurationSeconds;

    private void OnEnable()
    {
        Missive.AddListener<YearDataMissive>(OnYearData);
    }

    private void OnDisable()
    {
        Missive.RemoveListener<YearDataMissive>(OnYearData);
    }

    private void OnYearData(YearDataMissive missive)
    {
        string displayYearText = missive.data.year.ToString();
        if (displayYearText == "1899") displayYearText = "Combined\n1858 & 2018";
        textMesh.text = displayYearText;
        LeanTween.value(gameObject, startAlphaValue, endAlphaValue, fadeDurationSeconds).setOnUpdate((float alpha) => {
            textMesh.alpha = alpha;
        });
    }
}
