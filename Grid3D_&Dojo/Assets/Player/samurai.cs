using UnityEngine;
using System.Collections;


public class samurai : MonoBehaviour {

    MelleWeapon pole;

    Animator animator;

    int hash180;
    int hashJab;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        pole = GetComponentInChildren<MelleWeapon>();

        hash180 = Animator.StringToHash("180");
        hashJab = Animator.StringToHash("Jab");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger(hash180);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetTrigger(hashJab);
        }
	}

    public void Attack()
    {
        pole.Attack();
    }

    public void EndAttack()
    {
        pole.EndAttack();
    }
}