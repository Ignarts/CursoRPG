using System;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance;

    #region Private Attributes
    
    [SerializeField] private int _startGold = 1000;
    private int _totalGold;

    #endregion

    #region Properties

    public int TotalGold => _totalGold;

    #endregion

    #region Const

    private const string GOLD_KEY = "GOLD";
    
    #endregion

    #region Events

    public static event Action<int> OnGoldChanged;
    
    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
#if UNITY_EDITOR
        // Reset the gold to test
        PlayerPrefs.DeleteKey(GOLD_KEY);
        
        if(_startGold > 0)
        {
            _totalGold = _startGold;
            OnGoldChanged?.Invoke(_totalGold);
            return;
        }
#endif
        
        LoadGold();
    }
    
    #endregion

    #region Methods

    /// <summary>
    /// Load the gold from the player prefs
    /// </summary>
    /// <param name="gold"></param>
    public void AddGold(int gold)
    {
        _totalGold += gold;
        PlayerPrefs.SetInt(GOLD_KEY, _totalGold);

        SaveGold();

        Debug.Log($"Gold added: <color=yellow>{gold}</color>");
    }

    /// <summary>
    /// Remove given gold amount. Use this if we don't need to check if we have enough gold
    /// </summary>
    /// <param name="gold"></param>
    public void RemoveGold(int gold)
    {
        _totalGold -= gold;
        
        if(_totalGold < 0)
            _totalGold = 0;

        SaveGold();
        Debug.Log($"Gold removed: <color=yellow>{gold}</color>");
    }

    /// <summary>
    /// Remove given gold amount. Use this if we need to check if we have enough gold
    /// </summary>
    /// <param name="gold"></param>
    /// <param name="success"></param>
    public void RemoveGold(int gold, out bool success)
    {
        if(gold > _totalGold)
        {
            success = false;
            return;
        }

        _totalGold -= gold;
        SaveGold();
        success = true;
    }
    
    #endregion

    #region Save/Load Methods

    private void SaveGold()
    {
        PlayerPrefs.SetInt(GOLD_KEY, _totalGold);

        OnGoldChanged?.Invoke(_totalGold);
    }

    private void LoadGold()
    {
        _totalGold = PlayerPrefs.GetInt(GOLD_KEY, 0);
    }
    
    #endregion
}
