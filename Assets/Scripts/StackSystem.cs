using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class StackSystem : MonoBehaviour
{
    public static StackSystem Instance;
    [SerializeField] public List<GameObject> stackedPapers;
    [SerializeField] private Transform stackPoint;
    [SerializeField] private float yAxis;

    private bool isTakingPaper = false;
    private bool isDropingPaper = false;
    private bool isTakingMoney = false;
    private bool isDropingMoney = false;

    public RectTransform upgradePanel;

    public GameObject paperPrefab;
    
    private void Awake()
    {
        Instance = this;
    }

    IEnumerator TakePaper(PrintSystem printSystem)
    {
        if (isTakingPaper) yield break;
        isTakingPaper = true;

        if (printSystem.spawnedPapers.Count >= 1)
        {
            var stackablePaper = printSystem.spawnedPapers[printSystem.spawnedPapers.Count - 1];
            printSystem.spawnedPapers.Remove(stackablePaper);
            //stackedPapers.Add(stackablePaper);
            //Debug.Log("qq");
            //stackablePaper.transform.parent = stackPoint;

            //stackablePaper.transform.position = new Vector3(0, yAxis, 0);
            //stackablePaper.transform.DOLocalJump(new Vector3(0F, 0F, 0F),1,1,.1F);
            // yAxis += .03f;

            //Destroy(stackablePaper);
            GameObject newPaper = Instantiate(paperPrefab, stackPoint.transform);
            newPaper.transform.position = new Vector3(stackPoint.position.x, stackPoint.position.y + yAxis,
                stackPoint.position.z);

            yAxis += .03f;

            stackedPapers.Add(newPaper);
            
            PlayerMovement.Instance.anim.SetBool("carryingIdle",true);

            printSystem.pointIndex--;
            if (printSystem.pointIndex < 0)
            {
                printSystem.pointIndex = 0;
                printSystem.yAxis = 0;
            }
        }

        yield return new WaitForSeconds(GameManager.Instance.stackSpeed); //.2

        isTakingPaper = false;
    }

    IEnumerator DropPaper(WorkerSystem workerSystem)
    {
        if (isDropingPaper) yield break;
        isDropingPaper = true;
        
        if(stackedPapers.Count == 0) PlayerMovement.Instance.anim.SetBool("carryingIdle",false);

        if (stackedPapers.Count >= 1 && workerSystem.unsolvedPapers.Count < GameManager.Instance.deskLimit)
        {
            var dropablePaper = stackedPapers[stackedPapers.Count - 1];
            stackedPapers.Remove(dropablePaper);
            dropablePaper.transform.SetParent(workerSystem.dropPoint);

            // dropablePaper.transform.DOJump(new Vector3(workerSystem.emptyPaperPoint.position.x,
            //     workerSystem.emptyPaperPoint.position.y + yAxis, workerSystem.emptyPaperPoint.position.z), 1f, 1, .5f);
            // yAxis += .03f;

            Destroy(dropablePaper);
            GameObject newPaper = Instantiate(paperPrefab, workerSystem.dropPoint);
            newPaper.transform.position = new Vector3(workerSystem.dropPoint.position.x,
                workerSystem.dropPoint.position.y + workerSystem.yAxis, workerSystem.dropPoint.position.z);

            workerSystem.yAxis += .03f;
            yAxis -= .03f;
            workerSystem.unsolvedPapers.Add(newPaper);
        }

        yield return new WaitForSeconds(.02f);

        isDropingPaper = false;
    }

    IEnumerator TakeMoney(WorkerSystem workerSystem)
    {
        if (isTakingMoney) yield break;
        isTakingMoney = true;

        if (workerSystem.Moneys.Count >= 1)
        {
            var money = workerSystem.Moneys[workerSystem.Moneys.Count - 1];
            workerSystem.Moneys.Remove(money);

            money.transform
                .DOMove(new Vector3(transform.position.x, transform.position.y + .7f, transform.position.z), .2f)
                .OnComplete(() => { Destroy(money); });

            workerSystem.moneyAxis -= .15f;
            MoneyManager.Instance.EarnMoney(5);
        }

        yield return new WaitForSeconds(.2f);

        isTakingMoney = false;
    }

    IEnumerator CreateNewProduct(BuyingSystem buyingSystem)
    {
        if (isDropingMoney) yield break;
        isDropingMoney = true;

        if (MoneyManager.Instance.moneyCount >= 5)
        {
            buyingSystem.CreateProduct(5);
            MoneyManager.Instance.SpendMoney(5);
        }

        yield return new WaitForSeconds(.1f);

        isDropingMoney = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Printer") && stackedPapers.Count < GameManager.Instance.stackLimit && !isTakingPaper)
        {
            StartCoroutine(TakePaper(other.gameObject.GetComponent<PrintSystem>()));
        }

        if (other.CompareTag("Desk") && stackedPapers.Count >= 1)
        {
            StartCoroutine(DropPaper(other.gameObject.GetComponent<WorkerSystem>()));
        }

        if (other.CompareTag("Money"))
        {
            StartCoroutine(TakeMoney(other.gameObject.transform.parent.GetComponent<WorkerSystem>()));
        }

        if (other.CompareTag("NewDesk"))
        {
            StartCoroutine(CreateNewProduct(other.gameObject.GetComponent<BuyingSystem>()));
        }

        if (other.CompareTag("NewPrinter"))
        {
            StartCoroutine(CreateNewProduct(other.gameObject.GetComponent<BuyingSystem>()));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MN"))
        {
            upgradePanel.DOScale(Vector3.one, .25F);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MN"))
        {
            upgradePanel.DOScale(Vector3.zero, .25F);
        }
    }
}