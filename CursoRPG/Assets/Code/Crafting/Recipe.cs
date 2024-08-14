using System;
using Items;
using UnityEngine;

namespace Crafting
{
    [Serializable]
    public class Recipe
    {
        #region Private Attributes

        [SerializeField] private string _recipeName;

        [Header("First Ingredient")]
        [SerializeField] private InventoryItems _firstIngredient;
        [SerializeField] private int _firstIngredientAmount;

        [Header("Second Ingredient")]
        [SerializeField] private InventoryItems _secondIngredient;
        [SerializeField] private int _secondIngredientAmount;

        [Header("Result")]
        [SerializeField] private InventoryItems _result;
        [SerializeField] private int _resultAmount;

        #endregion

        #region Properties

        public string RecipeName => _recipeName;
        public InventoryItems FirstIngredient => _firstIngredient;
        public int FirstIngredientAmount => _firstIngredientAmount;
        public InventoryItems SecondIngredient => _secondIngredient;
        public int SecondIngredientAmount => _secondIngredientAmount;
        public InventoryItems Result => _result;
        public int ResultAmount => _resultAmount;

        #endregion
    }
}
