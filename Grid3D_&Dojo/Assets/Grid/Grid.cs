
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Grid : MonoBehaviour {

    public List<GridItem> grid = new List<GridItem>();

    public Transform gridPref;

    public Transform gridParent;//if null, apply to transform
    public Vector3 dimensions;//number of items in 1 direction
    public Vector3 scale;//item size in unity units
    public Vector3 margin;//how much are items apart

    public int layerLevel = 0;

    //how much should grid item move to be completly by this one. its values are defined by mesh size

    public bool runtimeCall = false;

    void Awake()
    {
        WorldGrid.AddGrid(this);

        //fire runtime script to create grid for main manager.
        if (runtimeCall)
        {
            Init(dimensions, scale, margin);
        }
    }

    public void Init(Vector3 dimensions, Vector3 scale, Vector3 margin)
    {
        this.dimensions = dimensions;
        this.scale = scale;
        this.margin = margin;

        grid = StartGrid(dimensions, scale, margin);
	}

    List<GridItem> StartGrid(Vector3 gridDimensions, Vector3 gridScale, Vector3 gridMargin)
    {
        List<GridItem> grid = new List<GridItem>();

        Transform gridParent = 
            this.gridParent == null ?  transform : this.gridParent;

        Vector3 saveScale = Vector3.one;

        //start grid - instantiate
        for (int i = 0; i < gridDimensions.x * gridDimensions.y * gridDimensions.z; ++i)
        {
            Transform gridI;

            //**create mesh & parent
            gridI = (Transform)Instantiate(gridPref, Vector3.zero, new Quaternion());
            saveScale = gridI.localScale;
            gridI.parent = gridParent;

            grid.Add(gridI.GetComponent<GridItem>());
        }

        // move to correct locations
        int x = 0, y = 0, z = 0;
        for (int i = 0; i < grid.Count; ++i)
        {
            GridItem g = grid[i];
            Vector3 prevSize = grid[i].transform.lossyScale;

            g.transform.Scale(saveScale);//scale before calculating position, NOTE: dont scale children, scale empties

            Vector3 size = new Vector3(g.transform.lossyScale.x/g.transform.parent.lossyScale.x,g.transform.lossyScale.y/g.transform.parent.lossyScale.y,g.transform.lossyScale.z/g.transform.parent.lossyScale.z );
            Vector3 margin = gridMargin;

            g.transform.Move(new Vector3(CubeFormula(x, size.x, margin.x), CubeFormula(y, size.y, margin.y), CubeFormula(z, size.z, margin.z)));

            g.transform.rotation = g.transform.parent.rotation;
            if (++x  % (int)gridDimensions.x==0)
            {
                x = 0;
                if (++z % (int)gridDimensions.z == 0)
                {
                    z = 0;
                    if (++y % (int)gridDimensions.y == 0)
                    {
                        y = 0;
                    }
                }
            }
        }

        return grid;
    }

    float CubeFormula(int n, float previousSize, float margin)
    {
        return n * (previousSize) + n * margin+ previousSize /2;
    }

    internal Vector3 ParentMargin()
    {
        return transform.lossyScale + (transform.parent != null ? transform.parent.GetComponent<Grid>().ParentMargin() : Vector3.zero);
    }
}