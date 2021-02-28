using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{

    [SerializeField] private GameObject blockPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int x = -10; x < 10; x += 2) { 
            for(int y = 5; y > 2; y--) {
                Instantiate(blockPrefab, new Vector3((float) (x + 0.75), y, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
