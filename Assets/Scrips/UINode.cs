using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINode : MonoBehaviour
{
    public GameObject GameObjectCanvas;
    public MapNode _MapNode;

    private Transform target;
    private LineRenderer line;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        if (line == null)
            line = gameObject.AddComponent<LineRenderer>();

        line.positionCount = 2;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.yellow;
        line.endColor = Color.yellow;

        GameObjectCanvas.SetActive(false);
    }
    void Update()
    {
        if (target != null)
        {
            line.SetPosition(0, transform.position);
            line.SetPosition(1, target.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObjectCanvas.SetActive(true);
            _MapNode.SetCollisionWhitPlayer(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObjectCanvas.SetActive(false);
            _MapNode.SetCollisionWhitPlayer(false);
        }
    }
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    public void OnInteractionWhitPlayer()
    {
        if (_MapNode.CollisionWhitPlayer == true && _MapNode.CanNodeMakeConection() == true)
        {
            SetTarget(_MapNode._CustomGraph._Nodes[_MapNode.NextLVLID].gameObject.transform);
        }
    }
    private void OnEnable()
    {
        PlayerController.OnNodeInteraction += OnInteractionWhitPlayer;
    }
    private void OnDisable()
    {
        PlayerController.OnNodeInteraction -= OnInteractionWhitPlayer;
    }

}
