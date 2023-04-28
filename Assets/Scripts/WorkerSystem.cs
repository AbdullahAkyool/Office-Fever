using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerSystem : MonoBehaviour
{
    public List<GameObject> unsolvedPapers;
    public List<GameObject> Moneys;
    public GameObject moneyPrefab;
    public Transform dropPoint;
    public Transform moneyPoint;
    public float yAxis;
    public float moneyAxis;

    void Start()
    {
        StartCoroutine(SolvePaper());
    }

    IEnumerator SolvePaper()
    {
        while (true)
        {
            if (unsolvedPapers.Count >= 1)
            {
                var unsolvedPaper = unsolvedPapers[unsolvedPapers.Count - 1];
                unsolvedPapers.Remove(unsolvedPaper);
                Destroy(unsolvedPaper);
                yAxis -= .03f;
                if (yAxis < 0) yAxis = 0;

                GameObject money = Instantiate(moneyPrefab, moneyPoint);
                money.transform.position = new Vector3(moneyPoint.position.x, moneyPoint.position.y + moneyAxis,
                    moneyPoint.position.z);
                moneyAxis += .15f;
                Moneys.Add(money);
            }
            yield return new WaitForSeconds(GameManager.Instance.workerSpeed); //1.5
        }
    }
}
