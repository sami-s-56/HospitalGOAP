using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Holds Worlds States i.e. list of all world states as a dictionary and single instance of it*/
public sealed class GWorld 
{
    private static readonly GWorld instance = new GWorld();

    private static WorldStates world;

    public Queue<GameObject> WaitingPatients = new Queue<GameObject>();
    
    public static Queue<GameObject> FreeCubicals = new Queue<GameObject>();

    public static Queue<GameObject> FreeOffices = new Queue<GameObject>();

    public static Queue<GameObject> FreeToilets = new Queue<GameObject>();

    public static List<GameObject> Puddles = new List<GameObject>();

    static GWorld()
    {
        world = new WorldStates();
        
        GameObject[] cubicals = GameObject.FindGameObjectsWithTag("Cubicle");
        foreach (GameObject g in cubicals)
        {
            AddCubical(g);
        }

        GameObject[] offices = GameObject.FindGameObjectsWithTag("Office");
        foreach (GameObject g in offices)
        {
            AddOffice(g);
        }

        GameObject[] toilets = GameObject.FindGameObjectsWithTag("Toilet");
        foreach (GameObject g in toilets)
        {
            AddToilet(g);
        }
    }

    private GWorld()
    {

    }

    public static GWorld Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }

    public static GameObject RemoveCubicle()
    {
        world.ModifyState("FreeCubicle", -1);
        return FreeCubicals.Dequeue();
    }

    public static void AddCubical(GameObject g)
    {
        FreeCubicals.Enqueue(g);
        world.ModifyState("FreeCubicle", 1);
    }

    public static GameObject RemoveOffice()
    {
        world.ModifyState("FreeOffices", -1);
        return FreeOffices.Dequeue();
    }

    public static void AddOffice(GameObject g)
    {
        FreeOffices.Enqueue(g);
        world.ModifyState("FreeOffices", 1);
    }

    public static GameObject RemoveToilet()
    {
        world.ModifyState("FreeToilets", -1);
        return FreeToilets.Dequeue();
    }

    public static void AddToilet(GameObject g)
    {
        FreeToilets.Enqueue(g);
        world.ModifyState("FreeToilets", 1);
    }

    public static void AddPuddle(GameObject g)
    {
        Puddles.Add(g);
        world.ModifyState("Puddles", 1);
    }

    public static GameObject RemovePuddle(Vector3 playerPos)
    {

        if (Puddles.Count == 0)
            return null;

        GameObject puddleToRemove = null;
        float lastDistance = 1000;

        foreach (GameObject p in Puddles)
        {
            float d = Vector3.Distance(p.transform.position, playerPos);
            if (d < lastDistance)
            {
                lastDistance = d;
                puddleToRemove = p;
            }
        }

        Puddles.Remove(puddleToRemove);

        world.ModifyState("Puddles", -1);

        return puddleToRemove;
    }

}
