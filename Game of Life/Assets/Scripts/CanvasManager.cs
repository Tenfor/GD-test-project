using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    private Button _playButton, _loadButton, _saveButton, _clearButton, _setButton, _randomizeButton;
    private Slider _rowSlider, _colSlider;
    private TextMeshProUGUI _rowText, _colText;
    private Sprite _playIcon, _stopIcon;
    void Start()
    {
        _playButton = transform.Find("PlayButton").GetComponent<Button>();       
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _saveButton = transform.Find("SaveButton").GetComponent<Button>();
        _saveButton.onClick.AddListener(OnSaveButtonClick);
        _loadButton = transform.Find("LoadButton").GetComponent<Button>();
        _loadButton.onClick.AddListener(OnLoadButtonClick);
        _clearButton = transform.Find("ClearButton").GetComponent<Button>();
        _clearButton.onClick.AddListener(OnClearButtonClick);
        _randomizeButton = transform.Find("RandomizeButton").GetComponent<Button>();
        _randomizeButton.onClick.AddListener(OnRandomizeButtonClick);
        _setButton = transform.Find("GridSettings/SetButton").GetComponent<Button>();
        _setButton.onClick.AddListener(OnSetButtonClick);
        _rowSlider = transform.Find("GridSettings/Row/Slider").GetComponent<Slider>();
        _rowSlider.onValueChanged.AddListener(OnRowSliderChange);
        _colSlider = transform.Find("GridSettings/Col/Slider").GetComponent<Slider>();
        _colSlider.onValueChanged.AddListener(OnColSliderChange);
        _rowText = transform.Find("GridSettings/Row/Text").GetComponent<TextMeshProUGUI>();
        _colText = transform.Find("GridSettings/Col/Text").GetComponent<TextMeshProUGUI>();

        _playIcon = Resources.Load<Sprite>("Images/playIcon");
        _stopIcon = Resources.Load<Sprite>("Images/stopIcon");
    }
    void OnPlayButtonClick(){
        GameManager.SimulationToggle();
        // string buttonText = GameManager.IsPlaying() ? "Stop" : "Play";
        _playButton.gameObject.GetComponent<Image>().sprite = GameManager.IsPlaying() ? _stopIcon : _playIcon;
        // _playButton.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
    }
    void OnSaveButtonClick(){
        GameManager.SaveGrid();
    }
    void OnLoadButtonClick(){
        GameManager.LoadGrid();
    }
    void OnClearButtonClick(){
        GameManager.ClearGrid();
    }
    void OnSetButtonClick(){
        _setButton.interactable = false;
        GameManager.SetGrid((int)_rowSlider.value,(int)_colSlider.value);
    }
    void OnRowSliderChange(float value){
        _rowText.text = "Rows: "+value;
        _setButton.interactable = true;
    }
    void OnColSliderChange(float value){
        _colText.text = "Cols: "+value;
        _setButton.interactable = true;
    }
    void OnRandomizeButtonClick(){
        print("randomize");
        GameManager.RandomizeGrid();
    }
}
