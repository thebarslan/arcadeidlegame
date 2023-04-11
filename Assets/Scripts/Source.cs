using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.UI;
using DG.Tweening;

public class Source : MonoBehaviour
{
    private bool isPlayerInside;
    [Header("Collider")] 
    [SerializeField] private SphereCollider sphereCollider;
    [Space(15)]
    [Header("Objects")] 
    [SerializeField] private GameObject sourceObject;
    [SerializeField] private GameObject canvas;
    [Space(15)]
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image progressImage;
    [Space(15)] 
    [Header("Buy")]
    [SerializeField] private int price;
    [SerializeField] private int currentPrice;
    [SerializeField] private bool isBought;
    [Space(15)]
    [Header("Nav Mesh Surface")] [SerializeField]
    private NavMeshSurface _navMeshSurface;
    
    
    
    private void Start()
    {
        currentPrice = price;
        UpdateUI();
        ChangeObject();
    }
    private void UpdateUI()
    {
        priceText.text = "$" + currentPrice.ToString();
        progressImage.fillAmount = (float)currentPrice / (float)price;
    }
    private void ChangeObject()
    {
        if (isBought)
        {
            sourceObject.SetActive(true);
            sourceObject.transform.localScale = Vector3.zero;
            sourceObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.2f).SetEase(Ease.InOutBounce);
            sourceObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f).SetEase(Ease.InOutBounce).SetDelay(0.2f);
            canvas.SetActive(false);
            sphereCollider.enabled = false;
        }
        else
        {
            sourceObject.SetActive(false);
            canvas.SetActive(true);
        }
        StartCoroutine(BuildNavMeshCoroutine());
    }
    private void ChangeProgress(int money)
    {
        currentPrice -= money;
        if (currentPrice < 0)
        {
            Buy();
            return;
        }
        UpdateUI();
    }
    private void Buy()
    {
        isBought = true;
        ChangeObject();
    }
    private IEnumerator LowerPrice()
    {
        // if(isBuyable) YOU WILL CHECK IF PLAYER CAN BUY THE PLACE AND IF THEY CAN YOU LOWER THE PRICE FASTER.
        while (!isBought && isPlayerInside)
        {
            ChangeProgress(4);
            yield return new WaitForSeconds(0f);
        }
        
        
    }
    private IEnumerator BuildNavMeshCoroutine()
    {
        yield return new WaitForSeconds(0.35f);
        BuildNavMesh();
    }
    private void BuildNavMesh()
    {
        _navMeshSurface.BuildNavMesh();
    }
    
    public void CoroutineStart()
    {
        isPlayerInside = true;
        StartCoroutine(LowerPrice());
    }
    
    public void CoroutineStop()
    {
        isPlayerInside = false;
    }
}