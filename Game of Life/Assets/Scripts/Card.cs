using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Transform _cells;
    public List<GridPosition> drawCoords = new List<GridPosition>();

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _cells = transform.Find("Cells").GetComponent<Transform>();
    }
    private void OnMouseDown()
    {
        GameManager.cardInHand = this;
    }
    private void OnMouseDrag() {
        _renderer.color = new Color(255,255,255,0.5f);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;    
    }
    private void OnMouseUp()
    {
        _renderer.color = new Color(255,255,255,1f);
        GameManager.cardInHand = null;
        GameManager.DrawCard();
    }
}
