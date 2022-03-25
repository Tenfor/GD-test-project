using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int cols;
    public GameObject cellPrefab;
    public float cellSize;
    public bool playing = false;
    [SerializeField] private float timeStep = 0.3f;
    private List<List<Cell>> cells;
    private List<Cell> markedCells;
    private List<Cell> savedCells;
    private List<Cell> highlightedCells;
    private Vector3 originalPosition;
    private int simulationFrame = 0;
    private Button _playButton;
    private TextMeshProUGUI _playButtonText;
    void Start()
    {
        GameManager.RegisterGridManager(this);
        cells = new List<List<Cell>>();
        markedCells = new List<Cell>();
        savedCells = new List<Cell>();
        highlightedCells = new List<Cell>();
        originalPosition = transform.position;
        _playButton = transform.Find("Canvas/PlayButton").GetComponent<Button>();
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _playButtonText = transform.Find("Canvas/PlayButton/Text").GetComponent<TextMeshProUGUI>();
        DrawGrid();
    }
    void DrawGrid(){
        for (int i = 0; i < rows; i++)
        {
            List<Cell> cellRow = new List<Cell>();
            for (int j = 0; j < cols; j++)
            {
                GameObject cellGO = Instantiate(cellPrefab,new Vector3(j*(cellSize),i*(cellSize),0),Quaternion.identity);
                cellGO.transform.localScale = new Vector3(cellSize,cellSize,1);
                cellGO.transform.SetParent(transform);
                Cell cell = cellGO.GetComponent<Cell>();
                cell.SetGridPosition(i,j);
                cellRow.Add(cell);
            }
            cells.Add(cellRow);
        }
        CenterGrid();
    }
    void CenterGrid(){
        float gridW = cols*cellSize;
        float gridH = rows*cellSize;
        transform.position = new Vector3(-gridW/2,-gridH/2.5f);
    }
    bool isAlive(int row, int col){
        if (row < 0 || row>rows-1 || col<0 || col>cols-1) return false;
        return cells[row][col].alive; 
    }
    public Cell GetCellAtPosition(int row, int col){
        if (row < 0 || row>rows-1 || col<0 || col>cols-1) return null;
        return cells[row][col];
    }
    public int CountNeighbours(int row, int col){
        int neighbours = 0;
        for (int dx = -1; dx <= 1; ++dx) {
            for (int dy = -1; dy <= 1; ++dy) {
                if (((dx != 0) || (dy != 0)) && isAlive(row + dx, col + dy))
                    neighbours += 1;
            }
        }
        return neighbours;
    }
    public void SimulateStep(){
        markedCells.Clear();
        _playButtonText.text = simulationFrame.ToString();
        for (int row = 0; row < cells.Count; row++)
        {
            for (int col = 0; col < cells[row].Count; col++)
            {
                int neighbours = CountNeighbours(row,col);
                if(cells[row][col].alive){
                    if(neighbours<=1){
                        markedCells.Add(cells[row][col]);
                    }
                    else if(neighbours>=4){
                        markedCells.Add(cells[row][col]);
                    } 
                }else{
                    if(neighbours == 3){
                        markedCells.Add(cells[row][col]);
                    }
                }
            }
        }
        ChangeMarkedCells();
        simulationFrame++;
        if(simulationFrame == 100) StopSimulation();
    }
    public void PlaySimulation(){
        playing = true;
        InvokeRepeating("SimulateStep", 0f, timeStep);
    }
    public void StopSimulation(){
        playing = false;
        CancelInvoke("SimulateStep");
        GameManager.NextTurn();
        _playButtonText.text = "End Turn - "+GameManager.currentTurn+"/"+GameManager.maxTurn;
        _playButton.interactable = true;
        simulationFrame = 0;      
    }
    public void ChangeMarkedCells(){
        markedCells.ForEach(cell=>{
            cell.alive = !cell.alive;
            cell.Redraw();
        });
    }
    // public void SaveGrid(){
    //     savedCells.Clear();
    //     cells.ForEach(cellRow=>{
    //         cellRow.ForEach(cell=>{
    //             if(cell.alive) savedCells.Add(cell);
    //         });
    //     });
    // }
    // public void LoadGrid(){
    //     if(savedCells.Count == 0) return;
    //     ClearGrid();
    //     savedCells.ForEach(cell=>{
    //         cell.alive = true;
    //         cell.Redraw();
    //     });
    // }
    public void ClearGrid(){
        cells.ForEach(cellRow=>{
            cellRow.ForEach(cell=>{
                if(cell.alive){
                    cell.alive = false;
                    cell.Redraw();
                }
            });
        });
    }
    public void HighLightCard(Card card,GridPosition highlightPosition){
        ClearHighlights();
        card.drawCoords.ForEach(position=>{
            Cell cell = GetCellAtPosition(highlightPosition.row+position.row,highlightPosition.col+position.col);
            if(cell != null) highlightedCells.Add(cell);
        });
        HighlightCells();
    }
    void HighlightCells(){
        highlightedCells.ForEach(cell=>{
            cell.SetHighlight(true);
        });
    }
    public void ClearHighlights(){
        highlightedCells.ForEach(cell=>{
            cell.SetHighlight(false);
        });
        highlightedCells.Clear();
    }
    public void ActivateHighlights(){
        highlightedCells.ForEach(cell=>{
            cell.SetHighlight(false);
            cell.alive = true;
            cell.Redraw();
        });
        highlightedCells.Clear();
    }
    // public void SetGrid(int r, int c){
    //     DestroyGrid();
    //     rows = r;
    //     cols = c;
    //     DrawGrid();
    // }
    // public void DestroyGrid(){
    //     cells.Clear();
    //     markedCells.Clear();
    //     savedCells.Clear();
    //     highlightedCells.Clear();
    //     foreach (Transform childCell in transform){
    //         Destroy(childCell.gameObject);
    //     }
    //     transform.position = originalPosition;
    // }
    // public void RandomizeGrid(){
    //     markedCells.Clear();
    //     cells.ForEach(cellRow=>{
    //         cellRow.ForEach(cell=>{
    //             int rand = Random.Range(0,2);
    //             cell.alive = rand == 0; 
    //             cell.Redraw();
    //         });
    //     });
    // }
    void OnPlayButtonClick(){
        if(GameManager.currentTurn>GameManager.maxTurn) return;
        _playButton.interactable = false;
        PlaySimulation();
    }
    public int CountAliveCells(){
        int count = 0;
        cells.ForEach(cellRow=>{
            count+=cellRow.FindAll(cell=>cell.alive).Count;
        });
        return count;
    }
}