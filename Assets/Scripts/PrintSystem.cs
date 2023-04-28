using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PrintSystem : MonoBehaviour
{
    [SerializeField] private Transform[] tablePoints = new Transform[14];
    public List<GameObject> spawnedPapers;
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private Transform spawnPoint;
    public float spawnTime;
    public float yAxis;
    private bool canSpawn;
    public int spawnCapacity;
    public int pointIndex;
    void Start()
    {
        for (int i = 0; i < tablePoints.Length; i++)
        {
            tablePoints[i] = transform.GetChild(2).GetChild(i);
        }
        StartCoroutine(SpawnPaper());
    }
    void Update()
    {
        
    }
    IEnumerator SpawnPaper()
    {
        while (true)
        {
            if (spawnedPapers.Count < spawnCapacity)
            {
                canSpawn = true;
            }
            else if (spawnedPapers.Count >= spawnCapacity)
            {
                canSpawn = false;
            }
            
            if (canSpawn)
            {
                GameObject newPaper = Instantiate(paperPrefab, spawnPoint.position, Quaternion.identity,transform.GetChild(0).transform);

                newPaper.transform.DOJump(new Vector3(tablePoints[pointIndex].position.x, tablePoints[pointIndex].position.y + yAxis,
                    tablePoints[pointIndex].position.z), 1f, 1, .5f).SetEase(Ease.OutQuad);
            
                spawnedPapers.Add(newPaper);

                if (pointIndex < 13)
                {
                    pointIndex++;
                }
                else
                {
                    pointIndex = 0;
                    yAxis += .02f;
                }
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }
}
