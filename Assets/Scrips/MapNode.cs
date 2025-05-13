using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MapNode : Node<GameObject>
{
    public bool CollisionWhitPlayer;
    public int NextLVLID;
    public CustomGraph _CustomGraph;
    public MapNode(GameObject _key) : base(_key) { }
    public void SetCollisionWhitPlayer(bool itsTrue)
    {
        CollisionWhitPlayer = itsTrue;
    }
    public bool CanNodeMakeConection()
    { 
        if (NextLVLID > 0)
        {
            _CustomGraph.SetNewConection(NextLVLID - 1, NextLVLID);
            return true;
        }
        return false;
    }
}
