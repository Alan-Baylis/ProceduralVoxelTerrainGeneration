using UnityEngine;
using System.Collections;

public class Block
{
    public int blockType;
    public bool _blockvis;

    public Block(int blockType, bool _blockvis)
    {
        this.blockType = blockType;
        this._blockvis = _blockvis;
    }
}

public class LandScapeGenerator : MonoBehaviour {

    public static int width = 128;
    public static int depth = 128;
    public static int height = 128;
    public int heightScale = 25;
    public float detailScale = 25;
    GameObject player;
    public GameObject[] terrainBlocks;
    public GameObject[] explosions;
    Block[,,] worldBlocks;
    public GameObject treeObject;
    public GameObject bushObject;
    public GameObject[] flowerObjects;
    public float bushDensity;
    public float treeDensity;
    public float flowerDensity;
    public int curBlockValue = 0;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        curBlockValue = 0;
        Cursor.visible = false;
        worldBlocks = new Block[width, height, depth];
        //Random.seed = Network.time;
        int seed =(int) Network.time * 10;
        for (int z = 0; z<depth;z++)
        {
            for (int x = 0; x<width;x++)
            {
                int y = (int)(Mathf.PerlinNoise((x+ seed) /detailScale,(z+ seed)/detailScale)*heightScale);
                Vector3 blockPos = new Vector3(x,y,z);

                CreateBlock(y,blockPos,true);
                while (y>0)
                {
                    y--;
                    blockPos = new Vector3(x,y,z);
                    CreateBlock(y,blockPos,false);
                }
            }
        }
	
	}

    void CreateBlock(int yHeight, Vector3 blockPos,bool _visibility)
    {
        if (yHeight > heightScale*0.7f) //(yHeight > Random.Range(12,10))
        {
            if(_visibility)
                Instantiate(terrainBlocks[0], blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(0,_visibility);
        }
        else if (yHeight > heightScale*0.5f) //(yHeight > Random.Range(11, 10))
        {
            if (_visibility)
                Instantiate(terrainBlocks[1], blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(1, _visibility);
        }
        else if (yHeight > heightScale*0.2f)//(yHeight > Random.Range(9, 5))
        {
            if (_visibility)
            {
                Instantiate(terrainBlocks[2], blockPos, Quaternion.identity);
                if (treeDensity/3 >(int) Random.Range(0,100))
                {
                    Instantiate(treeObject, blockPos + Vector3.up + new Vector3(0,treeObject.transform.localScale.y/2f,0), Quaternion.identity);
                }
                else if (bushDensity / 3 > (int)Random.Range(0, 100))
                {
                    Instantiate(bushObject, blockPos + Vector3.up + new Vector3(0, bushObject.transform.localScale.y / 2f, 0) + new Vector3(0,-0.75f,0), Quaternion.identity);
                }
                else if (flowerDensity / 3 > (int)Random.Range(0, 100))
                {
                    Instantiate(flowerObjects[(int)Random.Range(0,flowerObjects.Length)], blockPos + Vector3.up + new Vector3(0, bushObject.transform.localScale.y / 2f, 0) + new Vector3(0, -0.75f, 0), Quaternion.identity);
                }
            }
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(2, _visibility);
        }
        else if (yHeight > heightScale*0.1) //(yHeight > Random.Range(5, 4))
        {
            if (_visibility)
                Instantiate(terrainBlocks[3], blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(3, _visibility);
        }
        else
        {
            if (_visibility)
                Instantiate(terrainBlocks[4], blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(4, _visibility);
        }
    }

    void CreateBlock(int yHeight, Vector3 blockPos, bool _visibility,int blockValue)
    {
        if (_visibility)
        {
            Instantiate(player.GetComponent<SpawnControl>().blockInventory[blockValue], blockPos, Quaternion.identity);
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = new Block(blockValue, _visibility);
        }
    }
    void DrawBlock(Vector3 blockPos)
    {
        if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] == null) return;

        if (!worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z]._blockvis)
        {
            worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z]._blockvis = true;                    //Testing purposes Remove Later

            if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].blockType == 0)
            {
                Instantiate(terrainBlocks[0], blockPos, Quaternion.identity);
            }
            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].blockType == 1)
            {
                Instantiate(terrainBlocks[1], blockPos, Quaternion.identity);

            }
            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].blockType == 2)
            {
                Instantiate(terrainBlocks[2], blockPos, Quaternion.identity);

            }
            else if (worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].blockType == 3)
            {
                Instantiate(terrainBlocks[3], blockPos, Quaternion.identity);

            }
            else
            {
                Instantiate(terrainBlocks[4], blockPos, Quaternion.identity);

            }
        }
    }
    public void DeleteBlock(Collider hit)
    {
        Debug.Log(hit.name);
        if (hit.gameObject != null)
        {
            if (hit.transform.tag == "Vegetation")
            {
                Destroy(Instantiate(explosions[2], hit.transform.position, Quaternion.identity), 1.5f);
                Destroy(hit.transform.gameObject);
            }
            else if(hit.transform.tag == "Block")
            {
                Vector3 blockPos = hit.transform.position;
                if ((int)blockPos.y == 0) return;           //BottomGround
                Debug.Log("You are fucked" + hit.tag);
                // var clone = ;
                Destroy(Instantiate(explosions[worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z].blockType], blockPos, Quaternion.identity), 1.5f);
                worldBlocks[(int)blockPos.x, (int)blockPos.y, (int)blockPos.z] = null;

                Destroy(hit.transform.gameObject);

                for (int x = -1; x <= 1; x++)
                {
                    for (int y = -1; y <= 1; y++)
                    {
                        for (int z = -1; z <= 1; z++)
                        {
                            if (!(x == 0 && y == 0 && z == 0))
                            {
                                Vector3 neighbouringBlock = new Vector3(blockPos.x + x, blockPos.y + y, blockPos.z + z);
                                DrawBlock(neighbouringBlock);
                            }
                        }
                    }
                }
            }
        }

    }
	// Update is called once per frame
	void Update () {
        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0));
            if (Physics.Raycast(ray, out hit, 10))
            {
                if (hit.collider.tag == "Creeper")
                {
                    hit.collider.gameObject.GetComponent<CreeperExplosion>().TriggerExplosion();
                }
                else
                    DeleteBlock(hit.collider);
            }

        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0));
            if (Physics.Raycast(ray, out hit, 10) )
            {
                if (hit.transform.tag!= "Vegetation") {
                    Vector3 blockPos = hit.transform.position;
                    Vector3 hitVector = blockPos - hit.point;

                    hitVector.x = Mathf.Abs(hitVector.x);
                    hitVector.y = Mathf.Abs(hitVector.y);
                    hitVector.z = Mathf.Abs(hitVector.z);

                    if (hitVector.x > hitVector.z && hitVector.x > hitVector.y)
                        blockPos.x -= (int)Mathf.RoundToInt(ray.direction.x);
                    else if (hitVector.y > hitVector.z && hitVector.y > hitVector.z)
                        blockPos.y -= (int)Mathf.RoundToInt(ray.direction.y);
                    else
                        blockPos.z -= (int)Mathf.RoundToInt(ray.direction.z);

                    CreateBlock((int)blockPos.y, blockPos, true,player.GetComponent<SpawnControl>().curValue);
                }
            }
        }

     }
}
