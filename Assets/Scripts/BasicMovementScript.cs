using UnityEngine;
using System.Collections;

public class BasicMovementScript : MonoBehaviour {

    public float jumpHeight;
    public float moveSpeed;
    public float globalSpeed;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Jump",1,globalSpeed);
	}
	
	void Update () {
        
        
	
	}
    void Jump()
    {
        int seed = (int)Network.time * 10;
        transform.LookAt(new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x, transform.position.y, Random.rotation.z));
        transform.position += transform.forward * moveSpeed;
        transform.position += transform.up * jumpHeight;
    }
    //void OnCollisionEnter(Collision colEvent)
    //{
    //    if (colEvent.collider.tag == "Player")
    //    {
    //        gameObject.GetComponent<CreeperExplosion>()._explode = true;
    //    }
    //}
}
