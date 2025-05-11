using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LvlNodeController : MonoBehaviour
{
    public GameManager _GameManager;
    public GameObject goCanvas;
    bool collisionWhitPlayer;
    public int NextLvlID;
    private void Start()
    {
        goCanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            goCanvas.SetActive(true);
            collisionWhitPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            goCanvas.SetActive(false);
            collisionWhitPlayer = false;
        }
    }
    public void OnInteractionWhitPlayer()
    {
        if (collisionWhitPlayer == true && NextLvlID < _GameManager.LetterNodes.Count && NextLvlID > 0)
        {
            _GameManager.ConnectNodes((NextLvlID - 1), NextLvlID);
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
