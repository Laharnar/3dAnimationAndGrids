using System;
using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    int strength = 1;

    protected Action onDestroy = null;

    internal void DamageStructure() { DamageStructure(1); }

    public int Strength
    {
        get { return strength; }
        set
        {
            strength = value;
            if (strength == 0)
            {
                onDestroy();
            }
        }
    }

    protected virtual void Start()
    {//call this start at THE END OF FUNCTION, if you override it
        onDestroy += Deconstruct;
        onDestroy += Destruction;
    }

    internal void DamageStructure(int value)
    {
        //reduce strength
        Strength -= value;
    }

    void Deconstruct()
    {//reduces object to dust by creating another smaller grid
        //precomp
        int level = transform.parent.GetComponent<Grid>().layerLevel;

        if (level == WorldGrid.instance.maxLayer)
        {
            return;
        }
        /*Vector3 dimensions = new Vector3(3, levelOfseparation, 2),
            scale = new Vector3(1, 1, 1)/levelOfseparation,
            margin = new Vector3(0, 0, 0),
            position = new Vector3(0,0,0);*/


        //creates empty at parent location
        Grid t = (Instantiate(WorldGrid.RootPrefab, transform.position, transform.rotation) as Transform).GetScript<Grid>();
        t.transform.parent = transform.parent;

        Vector3 dimensions = t.dimensions,
            scale = new Vector3(t.scale.x / dimensions.x, t.scale.y / dimensions.y, t.scale.z / dimensions.z),
            margin = t.margin;

        t.transform.localScale = scale;

        //and assigns new grid to it
        t.Init(dimensions, scale, margin);

        Vector3 size = transform.Size();

        //move an empty parent to lower left location
        t.transform.position = transform.position - size / 2 + ParentMargin();

        t.layerLevel = level + 1;
    }

    public Vector3 ParentMargin()
    {
        Vector3 v = new Vector3(0,0,0);
        if (transform.parent != null && transform.parent.parent != null)
        {
            v = transform.parent.parent.GetComponent<Grid>().ParentMargin() / 2 - transform.lossyScale / 2;//this is problem
        }
        return v;
    }

    void Destruction()
    {//Warning: if this doesnt execute last you might get null reference exception
        Destroy(gameObject);
    }
}