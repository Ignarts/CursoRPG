using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Crafting
{
    /// <summary>
    /// Class that represents the crafting manager
    /// </summary>
    public class CraftingManager : MonoBehaviour
    {
        #region Private Attributes

        [Header("Configuration")]
        [SerializeField] private RecipeButton _recipeButtonPrefab;
        [SerializeField] private Transform _recipeButtonContainer;
        [SerializeField] private CraftingPanel _craftingPanel;

        [Header("Materials Configuration")]
        [SerializeField] private IngredientAttributes _firstIngredientAttributes;
        [SerializeField] private IngredientAttributes _secondIngredientAttributes;

        [Header("Crafting Result Configuration")]
        [SerializeField] private Image _resultIcon;
        [SerializeField] private TextMeshProUGUI _resultName;
        [SerializeField] private TextMeshProUGUI _resultDescription;

        [Header("Crafting Conditions")]
        [SerializeField] private GameObject _notEnoughtManterialButton;

        [Header("Recipes")]
        [SerializeField] private RecipeList _recipes;
        
        #endregion

        #region Properties

        public Recipe RecipeToCraft { get; private set; }
        
        #endregion

        #region MonoBehaviour Methods

        private void Start()
        {
            LoadRecipes();
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Load all recipes into the UI
        /// </summary>
        private void LoadRecipes()
        {
            foreach (Recipe recipe in _recipes.Recipes)
            {
                RecipeButton recipeButton = Instantiate(_recipeButtonPrefab, _recipeButtonContainer);
                recipeButton.LoadRecipe(recipe, this, _craftingPanel);
            }
        }

        /// <summary>
        /// Show the crafting recipe in the UI with the given recipe
        /// </summary>
        /// <param name="recipe"></param>
        public void ShowCraftingRecipe(Recipe recipe)
        {
            RecipeToCraft = recipe;
            
            _firstIngredientAttributes.Icon.sprite = recipe.FirstIngredient.Icon;
            _firstIngredientAttributes.Name.text = recipe.FirstIngredient.ItemName;
            _firstIngredientAttributes.Amount.text = $"{Inventory.Instance.GetItemAmount(recipe.FirstIngredient.Id)}/{recipe.FirstIngredientAmount}";
            
            _secondIngredientAttributes.Icon.sprite = recipe.SecondIngredient.Icon;
            _secondIngredientAttributes.Name.text = recipe.SecondIngredient.ItemName;
            _secondIngredientAttributes.Amount.text = $"{Inventory.Instance.GetItemAmount(recipe.SecondIngredient.Id)}/{recipe.SecondIngredientAmount}";

            _notEnoughtManterialButton.SetActive(!CanCraftRecipe(recipe));

            _resultIcon.sprite = recipe.Result.Icon;
            _resultName.text = recipe.Result.ItemName;
            _resultDescription.text = recipe.Result.Description;
        }

        public void CraftItem()
        {
            if(!CanCraftRecipe(RecipeToCraft))
                return;

            for(int i = 0; i < RecipeToCraft.FirstIngredientAmount; i++)
            {
                Inventory.Instance.RemoveItemOnCrafting(RecipeToCraft.FirstIngredient.Id);
            }

            for(int i = 0; i < RecipeToCraft.SecondIngredientAmount; i++)
            {
                Inventory.Instance.RemoveItemOnCrafting(RecipeToCraft.SecondIngredient.Id);
            }

            Inventory.Instance.AddItem(RecipeToCraft.Result, RecipeToCraft.ResultAmount);

            Debug.Log($"Item <color=green>{RecipeToCraft.Result.ItemName}</color> crafted x{RecipeToCraft.ResultAmount}");

            ShowCraftingRecipe(RecipeToCraft);
        }

        /// <summary>
        /// Return if the player can craft the recipe, that is, if he has enough ingredients
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public bool CanCraftRecipe(Recipe recipe)
        {
            return Inventory.Instance.GetItemAmount(recipe.FirstIngredient.Id) >= recipe.FirstIngredientAmount &&
                    Inventory.Instance.GetItemAmount(recipe.SecondIngredient.Id) >= recipe.SecondIngredientAmount;
        }

        #endregion
    }

    [Serializable]
    public struct IngredientAttributes
    {
        public Image Icon;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Amount;
    }
}
