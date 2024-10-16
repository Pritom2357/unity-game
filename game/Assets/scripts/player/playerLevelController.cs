using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerLevelController : MonoBehaviour
{
    public Slider levelSlider;
    public int hitsToLevelUp = 10;
    private int previousLevelPoints=10;
    public TextMeshProUGUI levelText;
    public int hardnessIncreaser = 5;
    public int pointsDecreaseOnHit = 1;

    private int currentHits = 0;
    private int currentLevel = 1;
    public delegate void LevelUpAction(int newLevel);

    public event LevelUpAction onLevelUp;

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
        // Debug.Log(currentHits);
    }

    public void RegisterHitByObstacle()
    {
        if (currentHits > 0)
        {
            currentHits -= pointsDecreaseOnHit;
            levelSlider.value = currentHits;
        }
        else if (currentHits <= 0 && currentLevel > 1)
        {
            currentLevel--;
            hitsToLevelUp = previousLevelPoints;
            currentHits = hitsToLevelUp; 
            previousLevelPoints -= hardnessIncreaser; 

            levelSlider.maxValue = hitsToLevelUp;
            levelSlider.value = currentHits;
    
            UpdateLevelText();
        }
    }

    private void LevelUp(){
        currentLevel++;
        previousLevelPoints=hitsToLevelUp;
        currentHits = 0;
        levelSlider.value = currentHits;

        hitsToLevelUp += hardnessIncreaser;
        levelSlider.maxValue = hitsToLevelUp;

        UpdateLevelText();

        onLevelUp?.Invoke(currentLevel);
    }

    private void LevelDown(){
        currentLevel--;
        currentHits = hitsToLevelUp - 5;
        levelSlider.value = currentHits;

        levelSlider.maxValue = hitsToLevelUp;

        UpdateLevelText();
        onLevelUp?.Invoke(currentLevel);
    }

    private void UpdateLevelText(){
        if(levelText != null){
            levelText.text = "Level: " + currentLevel.ToString();
        }
    }
}
