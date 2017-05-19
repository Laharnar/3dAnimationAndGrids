using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public float rate = 0.5f;
    float next = 0;

	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            //fire
            if (Time.time> next)
            {
                next = Time.time + rate;
                //shoot
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);//TODO: change to fps middle screen shooting
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1<<LayerMask.NameToLayer("Destructable") /*| LayerMask.NameToLayer("Destructable")*/))
                {
                    //found
                    //WorldGrid.GetHit(hit.transform.GetComponent<GridItem>());
                    hit.transform.GetComponent<GridItem>().DamageStructure();
                }
            }
        }
	}
}
