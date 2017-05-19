using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGrid : MonoBehaviour
{

	public static WorldGrid instance;

	public static List<Grid> grids = new List<Grid> ();

	public Transform rootPrefab;

	public int maxLayer = 2;
	
	public static string MaxLayer {
		get {
			return instance.maxLayer.ToString ();
		}
		set {
			instance.maxLayer = int.Parse (value);
		}
	}

	// Use this for initialization
	void Awake ()
	{
		RootPrefab = rootPrefab;
		instance = this;
	}
	
	internal static void AddGrid (Grid grid)
	{
		grids.Add (grid);
	}

	/*internal static void GetHit(GridItem gridItem) useless really script and function, unless you want central control
    {
        gridItem.DamageStructure();
    }*/

	public static Transform RootPrefab { get; set; }
}
