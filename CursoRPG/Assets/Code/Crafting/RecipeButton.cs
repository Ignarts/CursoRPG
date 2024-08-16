using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crafting
{
    public class RecipeButton : MonoBehaviour
    {
        #region Private Attributes

        [SerializeField] private Image _recipeIcon;
        [SerializeField] private TextMeshProUGUI _recipeName;

        #endregion

        #region Properties
        
        public Recipe RecipeLoaded { get; private set; }

        #endregion

        #region Methods

        public void LoadRecipe(Recipe recipe)
        {
            RecipeLoaded = recipe;
            _recipeIcon.sprite = recipe.Result.Icon;
            _recipeName.text = recipe.Result.ItemName;
        }
        
        #endregion
    }
}
