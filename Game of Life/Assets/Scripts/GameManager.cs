using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GridManager gridManager;
    private static GameManager gm;
    public static Card cardInHand;
    public static bool mouseOnGrid;
    void Awake(){
         if(gm == null){
            gm = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }    
    }
    void Update()
    {
        if(!mouseOnGrid) gridManager.ClearHighlights();
    }
    public static void RegisterGridManager(GridManager gM){
        if(gm == null) return;
        gridManager = gM;
    }
    public static void SimulationToggle(){
        if(gridManager.playing) gridManager.StopSimulation();
        else gridManager.PlaySimulation();
    }
    public static void SaveGrid(){
        gridManager.SaveGrid();
    }
    public static void LoadGrid(){
        gridManager.LoadGrid();
    }
    public static void ClearGrid(){
        gridManager.ClearGrid();
    }
    public static bool IsPlaying(){
        return gridManager.playing;
    }
    public static void HighlightCard(GridPosition position){
        if(cardInHand == null) return;
        gridManager.HighLightCard(cardInHand,position);
    }
    public static void DrawCard(){
        gridManager.ActivateHighlights();
    }
    public static void SetGrid(int row, int col){
        gridManager.SetGrid(row,col);
    }
    public static void RandomizeGrid(){
        gridManager.RandomizeGrid();
    }
}
