using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GridManager gridManager;
    public static Deck deck;
    private static GameManager gm;
    public static Card cardInHand;
    public static bool mouseOnGrid;
    public static int actions = 4;
    public static int currentTurn = 1;
    public static int maxTurn = 6;
    void Awake(){
        gm = this;
    }
    void Start()
    {
        NewGame();    
    }
    void Update()
    {
        if(!mouseOnGrid) gridManager.ClearHighlights();
    }
    public static void RegisterGridManager(GridManager gM){
        if(gm == null) return;
        gridManager = gM;
    }
    public static void RegisterDeck(Deck d){
        if(gm == null) return;
        deck = d;
    }
    public static void SimulationToggle(){
        if(gridManager.playing) gridManager.StopSimulation();
        else gridManager.PlaySimulation();
    }
    // public static void SaveGrid(){
    //     gridManager.SaveGrid();
    // }
    // public static void LoadGrid(){
    //     gridManager.LoadGrid();
    // }
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
    public static void ActivateHighlights(){
        gridManager.ActivateHighlights();
    }
    // public static void SetGrid(int row, int col){
    //     gridManager.SetGrid(row,col);
    // }
    // public static void RandomizeGrid(){
    //     gridManager.RandomizeGrid();
    // }
    public static void PlayCard(int index){
        deck.PlayCard(index);
    }
    public static void RetractActionPoints(int points){
        actions-=points;
    }
    public static void NextTurn(){
        if(currentTurn == maxTurn){
            GameOver();
            return;
        }
        actions = 4;
        currentTurn++;
        deck.SetCardsColor();
        deck.SetActionPointText();
        deck.DrawCard();
    }
    public static void GameOver(){
        int score = gridManager.CountAliveCells();
        int highScore = PlayerPrefs.HasKey("Highscore") ? PlayerPrefs.GetInt("Highscore") : 0;
        if(score > highScore) PlayerPrefs.SetInt("Highscore",score);
        PlayerPrefs.SetInt("Score",score);
        SceneManager.LoadScene("GameOverScene");
    }
    void NewGame(){
        actions = 4;
        currentTurn = 1;
        DrawCard();
        Invoke("DrawCard", 0.2f);
        Invoke("DrawCard", 0.4f);
    }
    void DrawCard()
    {
        deck.DrawCard();
    }
}
