using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("ScriptableObject")]
    [SerializeField] private TreesSO _treesSo;
    [Space(10)]
    [SerializeField] private int cutCount;
    [SerializeField] private float growTime;
    [SerializeField] private int woodAmount;


    
    private void GetTreeSO()
    {
        cutCount = _treesSo.cutCount;
        growTime = _treesSo.growTime;
        woodAmount = _treesSo.woodAmount;
    }
    private void Start()
    {
        GetTreeSO();
    }
}
