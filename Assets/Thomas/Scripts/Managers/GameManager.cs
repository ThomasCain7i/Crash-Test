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

    public void SaveElements()
    {
        PlayerPrefs.SetInt(fireKey, attackScript.fire);
        PlayerPrefs.SetInt(snowKey, attackScript.snow);
        PlayerPrefs.SetInt(waterKey, attackScript.water);
        PlayerPrefs.SetInt(sandKey, attackScript.sand);
    }

    public void SaveCollectables()
    {
        PlayerPrefs.SetInt(bonusKey, playerController.bonusCount);
    }

    public void ResetProgress()
    {
        // Rester all saves
        PlayerPrefs.DeleteAll();
    }
}
