using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyController : MonoBehaviour {

    public Sprite engineerSprite;


    public CompanyModel company;
    Dictionary<EngineerModel, GameObject> engineerGameObjectMap;

	// Use this for initialization
	void Start () {
        engineerGameObjectMap = new Dictionary<EngineerModel, GameObject>();
        company = new CompanyModel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateEngineerGameObjects()
    {
        foreach (EngineerModel engineer in company.engineers)
        {
            GameObject engineerGameObject = new GameObject();

            engineerGameObject.name = engineer.Name;
            //engineerGameObject.transform.position = new Vector3(obj.tile.X, obj.tile.Y, 0); // FIXME
            engineerGameObject.transform.SetParent(this.transform, true);
            engineerGameObject.AddComponent<SpriteRenderer>().sprite = engineerSprite; 

            engineerGameObjectMap.Add(engineer, new GameObject());
        }
    }
}
