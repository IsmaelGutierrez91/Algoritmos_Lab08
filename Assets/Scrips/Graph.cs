using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class Graph<TKey, TNodeValue> : MonoBehaviour
{
    public Dictionary<TKey, Node<TNodeValue>> Nodes { get; set; }

    public Graph()
    {
        Nodes = new Dictionary<TKey, Node<TNodeValue>>();
    }

    public void AddNode(TKey key, TNodeValue value)
    {
        if (!Nodes.ContainsKey(key))
        {
            Node<TNodeValue> newNode = new Node<TNodeValue>(value);
            //Nodes[key] = newNode;
            Nodes.Add(key, newNode);
        }
    }
    public virtual void AddEdge(TKey key1, TKey key2)
    {
        if (Nodes.ContainsKey(key1) && Nodes.ContainsKey(key2))
        {
            Node<TNodeValue> n1 = Nodes[key1];
            Node<TNodeValue> n2 = Nodes[key2];

            n1.AddNeighbor(n2);
            n2.AddNeighbor(n1);
        }
        else
        {
            Debug.Log("Uno o ambos nodos no existen en el grafo.");
        }
    }
    public void DisplayGraphAsMatrix()
    {
        int size = Nodes.Count;
        if (size == 0)
        {
            Debug.Log("El grafo est� vac�o.");
            return;
        }
        int[,] adjacencyMatrix = new int[size, size];
        Dictionary<TKey, int> nodeIndexMap = new Dictionary<TKey, int>();
        TKey[] keys = new TKey[size];
        // Mapear claves a �ndices
        int index = 0;
        foreach (var key in Nodes.Keys)
        {
            nodeIndexMap[key] = index;
            keys[index] = key;
            index++;
        }
        // Construir matriz de adyacencia
        foreach (var kvp in Nodes)
        {
            int nodeIndex = nodeIndexMap[kvp.Key];
            foreach (var neighbor in kvp.Value.Neighbors)
            {
                // Necesitamos encontrar la clave correspondiente al nodo vecino
                foreach (var entry in Nodes)
                {
                    if (EqualityComparer<Node<TNodeValue>>.Default.Equals(entry.Value, neighbor))
                    {
                        int neighborIndex = nodeIndexMap[entry.Key];
                        adjacencyMatrix[nodeIndex, neighborIndex] = 1;
                        adjacencyMatrix[neighborIndex, nodeIndex] = 1; // sim�trico
                        break;
                    }
                }
            }
        }
        #region Console Draw
        Debug.Log("Matriz de adyacencia:");

        // Encabezado
        string header = " ";
        for (int i = 0; i < size; i++)
        {
            header += $"{keys[i].ToString().PadRight(10)}";
        }
        Debug.Log(header);

        // Filas
        for (int i = 0; i < size; i++)
        {
            string row = $"{keys[i].ToString().PadRight(5)}";
            for (int j = 0; j < size; j++)
            {
                row += $"{(adjacencyMatrix[i, j] == 1 ? "Si" : "No").PadRight(10)}";
            }
            Debug.Log(row);
        }
        #endregion
    }
    public void DisplayGraphAsList()
    {
        Debug.Log("Grafo");
        if (Nodes.Count == 0)
        {
            Debug.Log("El grafo esta vac�o");
            return;
        }
        Debug.Log("Lista de adyecencia: \n");
        foreach (var node in Nodes)
        {
            string NodeValues = "";
            NodeValues += "Nodo " + node.Key + " : ";

            foreach (var neighbor in node.Value.Neighbors)
            {
                foreach (var entry in Nodes)
                {
                    if (EqualityComparer<Node<TNodeValue>>.Default.Equals(entry.Value, neighbor))
                    {
                        NodeValues += "," + entry.Key;
                        break;
                    }
                }
            }
            Debug.Log(NodeValues);
        }
    }
    public List<TKey> BFS(TKey startKey)
    {
        var visitados = new HashSet<Node<TNodeValue>>();
        var resultado = new List<TKey>();

        if (!Nodes.ContainsKey(startKey))
            return resultado;

        var cola = new Queue<Node<TNodeValue>>();
        var StartNode = Nodes[startKey];
        cola.Enqueue(StartNode);
        visitados.Add(StartNode);

        var nodeToKey = Nodes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        while (cola.Count > 0)
        {
            var actual = cola.Dequeue();
            resultado.Add(nodeToKey[actual]);

            foreach (var vecino in actual.neighbors)
            {
                if (!visitados.Contains(vecino))
                {
                    visitados.Add(vecino);
                    cola.Enqueue(vecino);
                }
            }
        }
        return resultado;
    }
    public List<TKey> DFS(TKey startkey)
    {
        var visitados = new HashSet<Node<TNodeValue>>();
        var resultado = new List<TKey>();

        if (!Nodes.ContainsKey(startkey))
            return resultado;

        var nodeToKey = Nodes.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

        void DFSRecursivo(Node<TNodeValue> actual)
        {
            visitados.Add(actual);
            resultado.Add(nodeToKey[actual]);

            foreach (var vecino in actual.Neighbors)
            {
                if (!visitados.Contains(vecino))
                {
                    DFSRecursivo(vecino);
                }
            }
        }

        DFSRecursivo(Nodes[startkey]);
        return resultado;
    }
}