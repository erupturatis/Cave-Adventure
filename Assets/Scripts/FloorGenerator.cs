using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    private GameObject[] floorPrefabsTop;
    private GameObject[] floorPrefabsBottom;

    [HideInInspector]
    public GameObject pairTop;
    private GameObject player;

    private float[] howMuchItGoesDownTop;
    private float[] howMuchItGoesDownBottom;

    private int lengthBot;
    private int lengthTop;

    private bool playerEntered;

    ChunkCentralizer cc;
    EndPosition selfEp;
    public void CreateTop()
    {

        pairTop = Instantiate(floorPrefabsTop[Random.Range(0, lengthTop-1)], transform.position + new Vector3(0f,12f,0f), Quaternion.identity);

    }
    void InitGameObjects()
    {
        floorPrefabsBottom = cc.floorPrefabsBottom;
        floorPrefabsTop = cc.floorPrefabsTop;

        howMuchItGoesDownTop = cc.howMuchItGoesDownTop;
        howMuchItGoesDownBottom = cc.howMuchItGoesDownBottom;

    }

    void Start()
    {
        cc = GameObject.FindObjectOfType<ChunkCentralizer>();
        selfEp = GetComponent<EndPosition>();
        InitGameObjects();

        lengthBot = floorPrefabsBottom.Length;
        lengthTop = floorPrefabsTop.Length;
        player = cc.player;
        
        if (!pairTop)
        {
            CreateTop();
        }
    }
    private void Update()
    {
        //cc = GameObject.FindObjectOfType<ChunkCentralizer>();

        if (player.transform.position.x - transform.position.x > 140f)
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


            if(pairScript.End.transform.position.y - selfEp.End.transform.position.y < 8f)
            {
                //print("finds special pt lower");
                //the top need to go up somewhat.
                //gets a top and a bottom that go away from each other
                int iterations = 0;
                while (iterations < 100)
                {
                    iterations++;
                    //I really shouldn't do this but I have no idea right now
                    ind1 = Random.Range(0, lengthBot );
                    ind2 = Random.Range(0, lengthTop );
                    if(howMuchItGoesDownBottom[ind1] > howMuchItGoesDownTop[ind2])
                    {
                        goto F;
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
                ;
            }
            else
            {
                ind1 = Random.Range(0, lengthBot );
                ind2 = Random.Range(0, lengthTop );
            }

            if (pairScript.End.transform.position.y - selfEp.End.transform.position.y > 20f)
            {
                //print("finds special pt higher");
                //the top need to go down somewhat.
                //gets a top and a bottom that go away from each other
                int iterations = 0;
                while (iterations < 50)
                {
                    iterations++;
                    //I really shouldn't do this but I have no idea right now
                    ind1 = Random.Range(0, lengthBot);
                    ind2 = Random.Range(0, lengthTop);
                    if (howMuchItGoesDownBottom[ind1] < howMuchItGoesDownTop[ind2])
                    {
                        goto F;
                    }
                }
                //It gets some non random pair
                for (int i = 0; i <= lengthBot - 1; i++)
                {
                    for (int j = 0; j <= lengthTop - 1; j++)
                    {
                        ind1 = i;
                        ind2 = j;
                        if (howMuchItGoesDownBottom[ind1] < howMuchItGoesDownTop[ind2])
                        {
                            goto A;
                        }
                    }
                }
                A:
                ;
            }
            else
            {
                ind1 = Random.Range(0, lengthBot);
                ind2 = Random.Range(0, lengthTop);
            }

            F:
            ;

            //print(ind1 + "   " + ind2);
            //print(howMuchItGoesDownBottom.Length);
            float p2 = pairScript.End.transform.position.y - howMuchItGoesDownTop[ind2];
            float p1 = selfEp.End.transform.position.y - howMuchItGoesDownBottom[ind1];
            while(p2 - p1<3f)
            {

                ind1 = Random.Range(0, lengthBot);
                ind2 = Random.Range(0, lengthTop);

                p2 = pairScript.End.transform.position.y - howMuchItGoesDownTop[ind2];
                p1 = selfEp.End.transform.position.y - howMuchItGoesDownBottom[ind1];
            }

            GameObject g1 = Instantiate(floorPrefabsBottom[ind1], selfEp.End.transform.position, Quaternion.identity);
            GameObject g2 = Instantiate(floorPrefabsTop[ind2], pairScript.End.transform.position , Quaternion.identity);
            g1.GetComponent<FloorGenerator>().pairTop = g2;
            g1.GetComponent<FloorGenerator>().floorPrefabsBottom = floorPrefabsBottom;
            g1.GetComponent<FloorGenerator>().floorPrefabsTop = floorPrefabsTop;
        }
    }
}
