using UnityEngine;
using System.Collections;

public class WaterAnimationTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        gameObject.transform.GetComponent<MeshRenderer>().materials[0].mainTextureOffset += new Vector2(0.05f,0);
	}
}
