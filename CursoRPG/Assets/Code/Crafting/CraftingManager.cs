using UnityEngine;

namespace Crafting
{
    public class CraftingManager : MonoBehaviour
    {
        #region Private Attributes

        [Header("Configuration")]
        [SerializeField] private RecipeButton _recipeButtonPrefab;
        [SerializeField] private Transform _recipeButtonContainer;

        [Header("Recipes")]
        [SerializeField] private RecipeList _recipes;
        
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
                recipeButton.LoadRecipe(recipe);
            }
        }
        
        #endregion
    }
}
