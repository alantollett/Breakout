using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField] private GameObject blockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for (int y = 4; y < 6; y += 1)
        {
            for (int x = -9; x < 9; x += 2)
            {
                Instantiate(blockPrefab, new Vector3(x, y, 0), Quaternion.identity);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
