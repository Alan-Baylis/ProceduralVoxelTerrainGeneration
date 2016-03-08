using UnityEngine;
using System.Collections;

public class CreeperExplosion : MonoBehaviour {
    public float explosionRadius;
    public LayerMask blockLayer;
    public GameObject explosion;
    public GameObject genereator;
    LandScapeGenerator genObject;
    public bool _explode;
    public float explosionGrowthRate = 0.2f;
	// Use this for initialization
	void Start () {
        genereator = GameObject.FindGameObjectWithTag("GameController");
        if (genObject!=null) {
            genObject = genereator.GetComponent<LandScapeGenerator>();
        }
	}
	
	// Update is called once per frame
	void Update () {

        //if (_explode)
        //{
        //    _explode = false;
        //    TriggerExplosion();
        //}
	
	}


    IEnumerator StartExplosion(GameObject dynamite)
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("WTF ");
        if (dynamite != null)
        {
            Debug.Log("WTF EXPLOSION");
            Collider[] hitColliders = Physics.OverlapSphere(dynamite.transform.position + Vector3.down * 0.5f, explosionRadius);
            Debug.Log(hitColliders.Length);
            //for (int i = 0; i < hitColliders.Length; i++)
            //{
            //    Debug.Log("Colldiers : " + hitColliders[i].name);
            //}
                for (int i = 0; i < hitColliders.Length; i++)
            {
                if ((hitColliders[i].tag == "Block") || (hitColliders[i].tag == "Vegetation"))
                {
                    Debug.Log("Deleting + blah" + hitColliders[i].name);
                    Debug.Log("position "  + i);
                    genObject.DeleteBlock(hitColliders[i] as Collider);
                }


            }
            Destroy(Instantiate(explosion, dynamite.transform.position, Quaternion.Euler(-90, 0, 0)), 1.5f);
            //Destroy(this.gameObject);
            dynamite.gameObject.SetActive(false);
        }
    }

    public void TriggerExplosion()
    {
        StartCoroutine(StartExplosion(gameObject));
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.down*0.5f, explosionRadius);
        //Debug.Log(hitColliders.Length);
        //for (int i = 0; i < hitColliders.Length; i++)
        //{
        //    if ((hitColliders[i].tag == "Block") || (hitColliders[i].tag == "Vegetation"))
        //    {
        //        Debug.Log("Deleting" + hitColliders[i].name);
        //        genObject.DeleteBlock(hitColliders[i]);
        //    }
        //}
        //Destroy(Instantiate(explosion, transform.position, Quaternion.Euler(-90, 0, 0)),1.5f);
        ////Destroy(this.gameObject);
        //gameObject.SetActive(false);
        ////float startTime = Time.time;
        ////float currTime = startTime;
        ////while (currTime - startTime < explosionGrowthRate)
        ////{
        ////    float currRadius = Mathf.Lerp(0f, 5f, Mathf.Clamp01((currTime - startTime) / explosionGrowthRate));
        ////    hitColliders = Physics.OverlapSphere(transform.position, currRadius);
        ////    for (int i = 0; i < hitColliders.Length; i++)
        ////    {
        ////        genObject.DeleteBlock(hitColliders[i]);
        ////    }
        ////    currTime = Time.time;
        ////}
    }
    void OnCollisionEnter(Collision colEvent)
    {
        if (colEvent.collider.tag == "Player")
        {
            StartCoroutine(StartExplosion(gameObject));
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }

}
