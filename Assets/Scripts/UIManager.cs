using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject healthManager;
    public GameObject hungerManager;
    public GameObject armourManager;
    public GameObject[] armourBar;
    public GameObject[] hungerBar;
    public GameObject[] healthBar;
    GameObject player;
    float fullHealth = 80;
    public float health = 80;
    public float hunger = 80;
    public float armour = 80;
    public bool _updateHealth = false;
    public float hungerRate = 60;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < healthManager.transform.childCount; i++)
        {
            healthBar[i] = null;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < healthManager.transform.childCount; i++)
        {
            healthBar[i] = healthManager.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < hungerManager.transform.childCount; i++)
        {
            hungerBar[i] = hungerManager.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < armourManager.transform.childCount; i++)
        {
            armourBar[i] = armourManager.transform.GetChild(i).gameObject;
        }
        InvokeRepeating("HungerDecay", hungerRate, hungerRate);
    }
	

    void UpdateStuff()
    {
        float healthPercent = ((health / fullHealth) * (healthManager.transform.childCount ));
        float hungerPercent = ((hunger / fullHealth) * (healthManager.transform.childCount));
        float armourPercent = ((armour / fullHealth) * (armourManager.transform.childCount));

        //health
        Debug.Log(healthPercent );
        for (int i = (int)healthPercent; i < healthManager.transform.childCount; i++)
        {
            healthManager.transform.GetChild(i).gameObject.SetActive(false);
            Debug.Log("Deleting" + i + " percent is " + healthPercent + "count is " + (healthManager.transform.childCount -1).ToString());
        }
        for (int i = 0; i < healthPercent; i++)
        {
            healthManager.transform.GetChild(i).gameObject.SetActive(true);
        }

        //Hunger
        for (int i = (int)hungerPercent; i < hungerManager.transform.childCount; i++)
        {
            hungerManager.transform.GetChild(i).gameObject.SetActive(false);
            Debug.Log("Deleting" + i + " percent is " + hungerPercent + "count is " + (hungerManager.transform.childCount - 1).ToString());
        }
        for (int i = 0; i < hungerPercent; i++)
        {
            hungerManager.transform.GetChild(i).gameObject.SetActive(true);
        }

        //Armour
        for (int i = (int)armourPercent; i < armourManager.transform.childCount; i++)
        {
            armourManager.transform.GetChild(i).gameObject.SetActive(false);
            Debug.Log("Deleting" + i + " percent is " + armourPercent + "count is " + (armourManager.transform.childCount - 1).ToString());
        }
        for (int i = 0; i < armourPercent; i++)
        {
            armourManager.transform.GetChild(i).gameObject.SetActive(true);
        }

    }

    void HungerDecay()
    {
        hunger -= 10;
        UpdateStuff();
    }
    // Update is called once per frame
    void Update () {

        if (_updateHealth == true)
        {
            _updateHealth = false;
            UpdateStuff();
        }
	
	}
}
