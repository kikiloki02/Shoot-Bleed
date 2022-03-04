using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EffectorSpawner : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        BoundsInt bounds = tilemap.cellBounds;
        Debug.Log(bounds);
        for(int rows = 0; rows < bounds.size.x; rows++)
        {
            for(int cols = 0; cols < bounds.size.y; cols++)
            {
                Vector3 pos = new Vector3(bounds.x + cols, bounds.y + rows, 0f);
                TileBase tile = tilemap.GetTile(new Vector3Int((int)pos.x, (int)pos.y, (int)pos.z));
                if (tile != null)
                {
                    if(tile.name != "Test_tilemap_29")
                        Instantiate(prefab, pos + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, null);
                }
                    
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
