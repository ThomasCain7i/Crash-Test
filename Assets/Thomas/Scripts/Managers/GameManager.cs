// Thomas

using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public AttackScript attackScript;
    private const string fireKey = "elementFire";
    private const string snowKey = "elementSnow";
    private const string waterKey = "elementWater";
    private const string sandKey = "elementSand";

    public PlayerController playerController;
    private const string bonusKey = "bonusCollected";

    public bool test =  false;

    public void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        attackScript = FindObjectOfType<AttackScript>();
    }

    public void SaveElements()
    {
        PlayerPrefs.SetInt(fireKey, attackScript.fire);
        PlayerPrefs.SetInt(snowKey, attackScript.snow);
        PlayerPrefs.SetInt(waterKey, attackScript.water);
        PlayerPrefs.SetInt(sandKey, attackScript.sand);
        PlayerPrefs.Save();
    }

    public void SaveCollectables()
    {
        PlayerPrefs.SetInt(bonusKey, playerController.SandBonusCount);
        PlayerPrefs.SetInt(bonusKey, playerController.WaterBonusCount);
        PlayerPrefs.SetInt(bonusKey, playerController.FireBonusCount);
        PlayerPrefs.SetInt(bonusKey, playerController.SnowBonusCount);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        attackScript.fire = PlayerPrefs.GetInt("elementFire");
        attackScript.fire = PlayerPrefs.GetInt("elementSnow");
        attackScript.fire = PlayerPrefs.GetInt("elementWater");
        attackScript.fire = PlayerPrefs.GetInt("elementSand");
        playerController.SandBonusCount = PlayerPrefs.GetInt("bonusCollected");
        playerController.FireBonusCount = PlayerPrefs.GetInt("bonusCollected");
        playerController.WaterBonusCount = PlayerPrefs.GetInt("bonusCollected");
        playerController.SnowBonusCount = PlayerPrefs.GetInt("bonusCollected");
    }

    public void ResetProgress()
    {
        // Rester all saves
        PlayerPrefs.DeleteAll();
    }
}