using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerLevelController : MonoBehaviour
{
    public Slider levelSlider;
    public int hitsToLevelUp = 10;
    public TextMeshProUGUI levelText;
    public int hardnessIncreaser = 5;

    private int currentHits = 0;
    private int currentLevel = 1;

    private void Start(){
        levelSlider.maxValue = hitsToLevelUp;
        levelSlider.value = 0;
        UpdateLevelText();
    }

    public void RegisterHit(){
        currentHits++;
        levelSlider.value = currentHits;

        if(currentHits >= hitsToLevelUp){
            LevelUp();
        }
        Debug.Log(currentHits);
    }

    private void LevelUp(){
        currentLevel++;
        currentHits=0;
        levelSlider.value = currentHits;

        hitsToLevelUp+=hardnessIncreaser;
        levelSlider.maxValue = hitsToLevelUp;

        UpdateLevelText();
    }

    private void UpdateLevelText(){
        if(levelText !=null){
            levelText.text = "Level: "+ currentLevel.ToString();
        }
    }
}
