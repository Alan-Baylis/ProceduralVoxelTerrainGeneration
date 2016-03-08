using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject tntObject;
    public float throwPower;
    public float explosionTimer = 5;
    public float explosionRadius = 2;
    LandScapeGenerator genObject;
    public GameObject tntExplosion;

    // Use this for initialization
    void Start () {
        genObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<LandScapeGenerator>();

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("f"))
        {
            ExplosionThrow();
        }
	
	}
    IEnumerator StartExplosion(GameObject dynamite)
    {
        yield return new WaitForSeconds(explosionTimer);
        Debug.Log("WTF ");
        if (dynamite != null)
        {
            Debug.Log("WTF EXPLOSION");
            Collider[] hitColliders = Physics.OverlapSphere(dynamite.transform.position + Vector3.down * 0.5f, explosionRadius);
            Debug.Log(hitColliders.Length);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if ((hitColliders[i].tag == "Block")||(hitColliders[i].tag == "Vegetation")) {
                    Debug.Log("Deleting" + GetComponent<Collider>().name);
                    Debug.Log("posiy"  + i);
                    genObject.DeleteBlock(hitColliders[i]);
                }

            }
            Destroy(Instantiate(tntExplosion, dynamite.transform.position, Quaternion.Euler(-90, 0, 0)), 1.5f);
            //Destroy(this.gameObject);
            dynamite.gameObject.SetActive(false);
        }
    }
    void ExplosionThrow()
    {
        GameObject clone = Instantiate(tntObject, transform.position + Camera.main.transform.up * (transform.localScale.y / 2f) + Camera.main.transform.forward, Quaternion.identity) as GameObject;
        Physics.IgnoreCollision(clone.GetComponent<Collider>(),transform.GetComponent<Collider>());

            Debug.Log("WTF??? ");
          clone.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * throwPower*100f);
          StartCoroutine(StartExplosion(clone.gameObject));
        }
    }
