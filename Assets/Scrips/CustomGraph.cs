using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGraph : Graph<int, MapNode>
{
    public MapNode LVL1;
    public MapNode LVL2;
    public MapNode LVL3;

    int currentNodes = 0;
    public List <MapNode> _Nodes = new List <MapNode>();

    void Start()
    {
        AddNewNodes(LVL1);
        AddNewNodes(LVL2);
        AddNewNodes(LVL3);
    }
    public void SetNewConection(int MN1, int MN2)
    {
        AddEdge(MN1, MN2);
    }
    public void AddNewNodes(MapNode newNode)
    {
        AddNode(currentNodes, newNode);
        _Nodes.Add(newNode);
        currentNodes = currentNodes + 1;
    }
    [Button("Show Matrix")]
    public void ShowMatrix()
    {
       DisplayGraphAsMatrix();
    }
}
