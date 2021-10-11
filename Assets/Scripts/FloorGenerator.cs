using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [SerializeField]
    public GameObject[] floorPrefabsTop;
    [SerializeField]
    public GameObject[] floorPrefabsBottom;

    [HideInInspector]
    public GameObject pairTop;

    [SerializeField]
    private GameObject End;
    private GameObject player;
    [SerializeField]
    private float[] howMuchItGoesDownTop;
    [SerializeField]
    private float[] howMuchItGoesDownBottom;
    


    private int lengthBot;
    private int lengthTop;
    private bool playerEntered;



    public void CreateTop()
    {
        pairTop = Instantiate(floorPrefabsTop[Random.Range(0, lengthTop-1)], transform.position + new Vector3(0f,5f,0f), Quaternion.identity);
    }
    void InitGameObjects()
    {

    }

    void Start()
    {
        lengthBot = floorPrefabsBottom.Length;
        lengthTop = floorPrefabsTop.Length;

        player = GameObject.FindGameObjectWithTag("Player");
        if (!pairTop)
        {
            InitGameObjects();

            CreateTop();
        }
    }
    private void Update()
    {

        
        if(player.transform.position.x - transform.position.x > 75f)
        {

            Destroy(pairTop);
            Destroy(gameObject);
        }
        if(player.transform.position.x > transform.position.x)
        {
            SpawnNextChunk();
        }
    }
    public void SpawnNextChunk()
    {
        if (playerEntered == false)
        {
            
            playerEntered = true;

            EndPosition pairScript = pairTop.GetComponent<EndPosition>();

            int ind1 = 0;
            int ind2 = 0;


            if(pairScript.End.transform.position.y - End.transform.position.y < 6f)
            {
                print("finds special");
                //the top need to go up somewhat.
                //gets a top and a bottom that go away from each other
                int iterations = 0;
                while (iterations < 50)
                {
                    iterations++;
                    //I really shouldn't do this but I have no idea right now
                    ind1 = Random.Range(0, lengthBot - 1);
                    ind2 = Random.Range(0, lengthTop - 1);
                    if(howMuchItGoesDownBottom[ind1] > howMuchItGoesDownTop[ind2])
                    {
                        break;
                    }
                }
                //It gets some non random pair
                for(int i = 0; i <= lengthBot - 1; i++)
                {
                    for(int j = 0; j <= lengthTop - 1; j++)
                    {
                        ind1 = i;
                        ind2 = j;
                        if (howMuchItGoesDownBottom[ind1] > howMuchItGoesDownTop[ind2])
                        {
                            goto A;
                        }
                    }
                }
                A:
                print("broken from loop");
            }
            else
            {
                ind1 = Random.Range(0, lengthBot - 1);
                ind2 = Random.Range(0, lengthTop - 1);
            }

            GameObject g1 = Instantiate(floorPrefabsBottom[ind1], End.transform.position, Quaternion.identity);
            GameObject g2 = Instantiate(floorPrefabsTop[ind2], pairScript.End.transform.position , Quaternion.identity);
            g1.GetComponent<FloorGenerator>().pairTop = g2;
            g1.GetComponent<FloorGenerator>().floorPrefabsBottom = floorPrefabsBottom;
            g1.GetComponent<FloorGenerator>().floorPrefabsTop = floorPrefabsTop;
        }
    }
}
