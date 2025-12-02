using UnityEngine;
using TMPro;  

public class FishCount : MonoBehaviour
{
    public static FishCount Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI fishCountText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void UpdateFishCount(int count)
    {
        if (fishCountText != null)
        {
            fishCountText.text = "Fish Score: " + count;
        }
    }
}