// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class CanvasManager : MonoBehaviour
// {
//     private Button _playButton;
//     void Start()
//     {
//         _playButton = transform.Find("PlayButton").GetComponent<Button>();       
//         _playButton.onClick.AddListener(OnPlayButtonClick);
//     }
//     void OnPlayButtonClick(){
//         GameManager.SimulationToggle();
//     }
//     void OnSaveButtonClick(){
//         GameManager.SaveGrid();
//     }
//     void OnLoadButtonClick(){
//         GameManager.LoadGrid();
//     }
//     void OnClearButtonClick(){
//         GameManager.ClearGrid();
//     }
//     void OnRandomizeButtonClick(){
//         print("randomize");
//         GameManager.RandomizeGrid();
//     }
// }
