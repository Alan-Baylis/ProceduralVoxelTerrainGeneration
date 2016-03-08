using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnControl : MonoBehaviour {

    public List<GameObject> blockInventory = new List<GameObject>();
    float prevValue;
    int length;
    public int curValue = 0;
    GameObject display;

	// Use this for initialization
	void Start () {
        display = GameObject.Find("Display");
        prevValue = Input.GetAxis("Mouse ScrollWheel");
        length = blockInventory.Count;
        SetDisplay();
        //Debug.Log("ll " + length + "gg" + blockInventory.Count);
    }
	
    void SetDisplay()
    {
        if (display.transform.childCount!=0) {
            for (int i = 0; i < display.transform.childCount; i++)
            {
                Destroy(display.transform.GetChild(0).gameObject);
            }
        }
        else
        {
            GameObject clone = Instantiate(blockInventory[curValue], display.transform.position, Quaternion.identity) as GameObject;
            //clone.GetComponent<MeshRenderer>().receiveShadows = false;
            //clone.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            clone.transform.localScale = display.transform.localScale;
            clone.transform.SetParent(display.transform);
        }
    }
	// Update is called once per frame
	void Update () {

        //if (prevValue!= Input.GetAxis("Mouse ScrollWheel"))
        //{
        //    prevValue = Input.GetAxis("Mouse ScrollWheel");

        //    curValue += Mathf.Abs((int)Input.GetAxis("Mouse ScrollWheel") * 10);
        //    //curValue = Mathf.Abs(curValue % length);
        //    Debug.Log(" Current Axis value: " + Input.GetAxis("Mouse ScrollWheel"));

        //}
        if (prevValue != Input.GetAxis("Mouse ScrollWheel"))
        {
            prevValue = Input.GetAxis("Mouse ScrollWheel");
            if (prevValue > 0)
            {
                curValue += (int)(prevValue * 10);
            }
            else
            {
                curValue += (int)(prevValue * 10);
                curValue += length;
            }

            if (curValue >= length || curValue < 0)
            {
                curValue = Mathf.Abs(curValue % length);
            }
            SetDisplay();
        }
    }
}
