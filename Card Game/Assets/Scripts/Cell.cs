using UnityEngine;
public class Cell : MonoBehaviour
{
    public bool alive;
    public bool highlighted = false;
    private Sprite cellAlive, cellDead;
    public GridPosition gridPosition;
    private SpriteRenderer _renderer;
    private void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        cellAlive = Resources.Load<Sprite>("Images/cellAlive");
        cellDead = Resources.Load<Sprite>("Images/cellDead");
    }
    // private void OnMouseDown() {
    //     alive = !alive;
    //     Redraw();
    // }
    private void OnMouseOver() {
        GameManager.HighlightCard(gridPosition);
        GameManager.mouseOnGrid = true;
    }
    private void OnMouseExit()
    {
        GameManager.mouseOnGrid = false;
    }
    public void Redraw(){
        _renderer.sprite = alive || highlighted ? cellAlive : cellDead;        
        float alpha = highlighted ? 0.3f : 1f;
        _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, alpha);
    }
    public void SetGridPosition(int row, int col){
        gridPosition = new GridPosition(row,col);
    }
    // public bool CheckGridPosition(int row, int col){
    //     return gridPosition.row == row && gridPosition.col == col;
    // }
    public void SetHighlight(bool l){
        highlighted = l;
        Redraw();
    }
}