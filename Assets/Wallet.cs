using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI coinText;
    private int coins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {coins}";
        }
    }
}