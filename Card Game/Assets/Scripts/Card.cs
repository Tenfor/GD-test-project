using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Transform _cells;
    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool moveEnabled = false;
    public List<GridPosition> drawCoords = new List<GridPosition>();
    public float moveSpeed = 10f;
    public bool clickEnabled = false;
    public int handIndex;
    public int actionCost;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _cells = transform.Find("Cells").GetComponent<Transform>();
        originalPosition = _cells.position;
    }
    void Update()
    {
        if(moveEnabled){
            float step =  moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                originalPosition = targetPosition;
                moveEnabled = false;
                clickEnabled = true;
            }    
        }
    }
    private bool IsEnabled(){
        return GameManager.actions>= actionCost && clickEnabled && !GameManager.IsPlaying();
    }
    private void OnMouseDown()
    {
        if(!IsEnabled()) return;
        GameManager.cardInHand = this;
    }
    private void OnMouseDrag() {
        if(!IsEnabled() || GameManager.cardInHand == null) return;
        _renderer.color = new Color(255,255,255,0.5f);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = 0f;    
    }
    private void OnMouseUp()
    {
        if(!IsEnabled() || GameManager.cardInHand == null) return;
        _renderer.color = new Color(255,255,255,1f);
        GameManager.cardInHand = null;
        GameManager.ActivateHighlights();
       
        if(GameManager.mouseOnGrid){
            GameManager.RetractActionPoints(actionCost);
            GameManager.PlayCard(handIndex);
        }
    }
    public void MoveTowards(Vector3 position){
        targetPosition = position;
        moveEnabled = true;
    }
    public void SetColor(){
        Color red = new Color(1f,.5f,.5f);
        Color normal = new Color(1f,1f,1f);
        _renderer.color = GameManager.actions>=actionCost ? normal : red;
    }
}
