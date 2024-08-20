using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Crafting
{
    /// <summary>
    /// Class that represents a recipe button in the UI
    /// </summary>
    public class RecipeButton : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Image _recipeIcon;
        [SerializeField] private TextMeshProUGUI _recipeName;

        private CraftingManager _craftingManager;
        private CraftingPanel _craftingPanel;

        #endregion

        #region Properties
        
        public Recipe RecipeLoaded { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Load the recipe into the button
        /// </summary>
        /// <param name="recipe"></param>
        public void LoadRecipe(Recipe recipe, CraftingManager craftingManager, CraftingPanel craftingPanel)
        {
            RecipeLoaded = recipe;
            _craftingManager = craftingManager;
            _craftingPanel = craftingPanel;
            _recipeIcon.sprite = recipe.Result.Icon;
            _recipeName.text = recipe.Result.ItemName;
        }

        /// <summary>
        /// Show the loaded recipe in the crafting manager
        /// </summary>
        public void ShowRecipe()
        {
            _craftingManager.ShowCraftingRecipe(RecipeLoaded);
            _craftingPanel.ShowCraftingPanelInfo();
        }
        
        #endregion
    }
}
