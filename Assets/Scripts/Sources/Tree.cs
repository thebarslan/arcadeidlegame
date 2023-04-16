using System.Collections;
using UnityEngine;
using DG.Tweening;
using Managers;
using Player;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;

public class Tree : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private Image treeDuration;
    [SerializeField] private GameObject treeGrowPanel;
    [SerializeField] private GameObject treeDurationPanel;
    [SerializeField] private Image growBar;
    [SerializeField] private TextMeshProUGUI growTimeText;
    [Header("ScriptableObject")]
    [SerializeField] private TreesSO _treesSo;

    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject wood;
    [SerializeField] private Transform logs;
    [Space(10)]
    public int cutCount;
    [SerializeField] private int growTime;
    public int _growTime;
    [SerializeField] private int woodAmount;
    [SerializeField] private int totalCutCount;
    private int cutWoodAmount;
    private bool isCutted, isCutting;
    private float time = 0f;
    private bool isGrowing = false;

    private void GetTreeSO()
    {
        cutCount = _treesSo.cutCount;
        growTime = _treesSo.growTime;
        woodAmount = _treesSo.woodAmount;
    }
    private void Update()
    {
        if(!isGrowing) return;
        GrowTimer();
    }
    private void Start()
    {
        isCutted = false;
        GetTreeSO();
        _growTime = growTime;
        cutWoodAmount = woodAmount / cutCount;
        totalCutCount = cutCount;
        treeDuration.fillAmount = cutCount / totalCutCount;
        growBar.fillAmount = (float)_growTime / growTime;
        growTimeText.text = _growTime.ToString() + "s";
    }

    public void CutTree()
    {
        isCutting = true;
        StartCoroutine(CutTreeCoroutine());
    }

    public void CutTreeStop()
    {
        isCutting = false;
    }
    private void GetWood()
    {
        if (cutCount < 1)
        {
            isCutted = true;
            isCutting = false;
            FindObjectOfType<PlayerCollisionHandler>().transform.GetComponent<Animator>().SetBool("isCutting", false);
            treeDurationPanel.SetActive(false);
            treeGrowPanel.SetActive(true);
            tree.SetActive(false);
            _growTime = growTime;
            isGrowing = true;
            return;
        };
        FindObjectOfType<Source_Manager>().GetComponent<Source_Manager>().UpdateWood(cutWoodAmount);
        cutCount--;
        treeDuration.fillAmount = (float)cutCount / (float)totalCutCount;
    }
    private void GrowTimer()
    {
        if (_growTime == 0)
        {
            isGrowing = false;
            ResetTree();
        }
        time += Time.deltaTime;
        if (time >= 1f)
        {
            time = 0f;
            _growTime--;
            growBar.fillAmount = (float)_growTime / growTime;
            growTimeText.text = _growTime.ToString() + "s";
        }
        
    }
    private void ResetTree()
    {
        GetTreeSO();
        tree.SetActive(true);
        treeDurationPanel.SetActive(true);
        treeGrowPanel.SetActive(false);
        isCutted = false;
        isCutting = false;
        isGrowing = false;
        _growTime = growTime;
        totalCutCount = cutCount;
        treeDuration.fillAmount = cutCount / totalCutCount;
    }
    private void SpawnWood()
    {
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-2f, 2f);
            float z = Random.Range(-3.5f, -2f);
            
            var woodObj = Instantiate(wood, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),new Quaternion(0f, Random.Range(-20f,20f), 0f, 0f), logs);
            // woodObj.transform.localRotation = new Quaternion(0f, 20f, 0f, 0f);
            woodObj.transform.DOJump(new Vector3(transform.position.x, 0.2f, transform.position.z) + new Vector3(x,0f, z), 2f, 1, 0.1f);
            // woodObj.transform.DOMove(new Vector3(transform.position.x, 0f, transform.position.z + 1), 0.1f);
            // woodObj.transform.DOFlip();
            Vector3 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
            woodObj.transform.DOMove(new Vector3(playerPos.x, playerPos.y + 1f, playerPos.z), 1f, false).SetDelay(0.2f);
            woodObj.transform.DOScale(Vector3.zero, 1f).SetDelay(0.3f);
            Destroy(woodObj, 5);

        }
    }
    private void ShakeTree()
    {
        tree.transform.DOShakePosition(0.5f,Vector3.right / 5, 2, 0.1f);
    }
    private IEnumerator CutTreeCoroutine()
    {
        while (!isCutted && isCutting)
        {
            GetWood();
            if (!isCutted)
            {
                SpawnWood();
                ShakeTree();
                if (cutCount >= 1)
                {
                    yield return new WaitForSeconds(1.06f);
                }
                else
                {
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
    }
}
