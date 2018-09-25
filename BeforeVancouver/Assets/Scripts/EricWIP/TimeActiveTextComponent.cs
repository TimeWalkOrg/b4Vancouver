using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeActiveTextComponent : MonoBehaviour
{
    public TextMeshPro textMesh;

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
        textMesh.text = missive.data.year.ToString();
    }
}
