using UnityEngine;

public class LootManager : MonoBehaviour
{
    public static LootManager Instance;

    #region MonoBehavior Methods

    #region Private Attributes

    [Header("Loot Manager Configuration")]
    [SerializeField] private GameObject lootPanel;
    
    #endregion

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        HideLootPanel();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Show the loot panel
    /// </summary>
    public void ShowLootPanel()
    {
        lootPanel.SetActive(true);
    }

    /// <summary>
    /// Hide the loot panel
    /// </summary>
    private void HideLootPanel()
    {
        lootPanel.SetActive(false);
    }
    
    #endregion
}
