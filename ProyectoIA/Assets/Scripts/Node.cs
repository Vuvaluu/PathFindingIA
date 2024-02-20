using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node 
{
    public bool walkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridZ;

    public int gCost;
    public int hCost;
    public Node parent;

     /* 
        Constructor del nodo(Parametros: Si el nodo puede ser caminable, la position 
        del nodo en el espacio 3D, la coordenada X en la grid y la coordenada z en la grid.)
    */
    public Node(bool t_walkable, Vector3 t_worldPosition, int _gridX, int _gridZ)
    {
        walkable = t_walkable;
        worldPosition = t_worldPosition;
        gridX = _gridX;
        gridZ = _gridZ;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    
}
