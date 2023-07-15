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
    private const string sandBonusKey = "sandBonusCollected";
    private const string waterBonusKey = "waterBonusCollected";
    private const string fireBonusKey = "fireBonusCollected";
    private const string snowBonusKey = "snowBonusCollected";

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

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
        PlayerPrefs.SetInt(sandBonusKey, playerController.SandBonusCount);
        PlayerPrefs.SetInt(waterBonusKey, playerController.WaterBonusCount);
        PlayerPrefs.SetInt(fireBonusKey, playerController.FireBonusCount);
        PlayerPrefs.SetInt(snowBonusKey, playerController.SnowBonusCount);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        attackScript.fire = PlayerPrefs.GetInt("elementFire");
        attackScript.snow = PlayerPrefs.GetInt("elementSnow");
        attackScript.water = PlayerPrefs.GetInt("elementWater");
        attackScript.sand = PlayerPrefs.GetInt("elementSand");
        playerController.SandBonusCount = PlayerPrefs.GetInt("sandBonusCollected");
        playerController.FireBonusCount = PlayerPrefs.GetInt("fireBonusCollected");
        playerController.WaterBonusCount = PlayerPrefs.GetInt("waterBonusCollected");
        playerController.SnowBonusCount = PlayerPrefs.GetInt("snowBonusCollected");
    }

    public void ResetProgress()
    {
        // Rester all saves
        PlayerPrefs.DeleteAll();
    }
}