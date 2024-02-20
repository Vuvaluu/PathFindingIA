using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinding : MonoBehaviour
{

    List<GameObject> pathTransform = new List<GameObject>();
    List<Vector3> pathV3 = new List<Vector3>();
    [SerializeField] Transform seeker, target;
    [SerializeField] GameObject emptyGO;
    [SerializeField] Transform parent;
    public Grid grid;
    int oldPathCount = 0;

     /* 
        Cada frame se busca la mejor ruta hacía el player.
    */
    void Update()
    {
        FindPath(seeker.position, target.position);
    }

     /* 
        Este método se utiliza para encontrar la mejor ruta desde el
        Start position hasta el target position.
    */
    void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        //Mientras openSet no este vacio, compara los costos de los nodos y guarda el mas barato.
        while (openSet.Count > 0)
        {
            Node node = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                {
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];
                }
            }

            openSet.Remove(node);
            closedSet.Add(node);
            //Si el currentNode es el jugador recuerda el path y sale del metodo.
            if (node == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }
            //Se itera sobre todos los vecinos del currentNode
            foreach (Node neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }
                //Se saca el nuevo costo del vecino usando la heuristica.
                int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }

    //Volver a trazar el path desde el final al inicio para colocar GameObjects en el path, para hacer pathFollowing.
    void RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;

        // Si path cambio, entonces clearear pathv3 para actualizar la info
        if (oldPathCount != path.Count) {
            // Es diferente
            oldPathCount = path.Count;
            pathV3.Clear();
            pathTransform.Clear();
            foreach(Transform child in parent) {
                Destroy(child.gameObject);
            }
            foreach (Node tmp in path) {
                if (!pathV3.Contains(tmp.worldPosition)){
                    pathV3.Add(tmp.worldPosition);
                    pathTransform.Add(Instantiate(emptyGO, tmp.worldPosition, Quaternion.identity, parent));
                }
            }
        }
            
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstZ = Mathf.Abs(nodeA.gridZ - nodeB.gridZ);

        if (dstX > dstZ)
            return 14 * dstZ + 10 * (dstX - dstZ);
        return 14 * dstX + 10 * (dstZ - dstX);
    }

    public List<GameObject> GetPathTransform(){
        return pathTransform;
    }
}
