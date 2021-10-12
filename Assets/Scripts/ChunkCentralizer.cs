using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkCentralizer : MonoBehaviour
{
    public GameObject[] floorPrefabsTop;
    public GameObject[] floorPrefabsBottom;

    public float[] howMuchItGoesDownTop;
    public float[] howMuchItGoesDownBottom;

    public GameObject player;

    private void Start()
    {
        howMuchItGoesDownBottom = new float[floorPrefabsBottom.Length ];
        howMuchItGoesDownTop = new float[floorPrefabsTop.Length ];

        Vector3 pos = new Vector3(500f, 0f, 0f);
        for(int i = 0; i <= floorPrefabsTop.Length - 1; i++)
        {
            GameObject gameob = Instantiate(floorPrefabsTop[i] ,pos ,Quaternion.identity);
            GameObject ep = gameob.transform.Find("End").gameObject;
            howMuchItGoesDownTop[i] = gameob.transform.position.y - ep.transform.position.y;
            Destroy(gameob);
        }

        for (int i = 0; i <= floorPrefabsBottom.Length - 1; i++)
        {
            GameObject gameob = Instantiate(floorPrefabsBottom[i], pos, Quaternion.identity);
            GameObject ep = gameob.transform.Find("End").gameObject;
            howMuchItGoesDownBottom[i] = gameob.transform.position.y - ep.transform.position.y;
            Destroy(gameob);
        }
    }

}
