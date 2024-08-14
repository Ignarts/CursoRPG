using UnityEngine;

namespace Crafting
{
    [CreateAssetMenu(fileName = "RecipeList", menuName = "Crafting/Recipe List")]
    public class RecipeList : ScriptableObject
    {
        #region Private Attributes
        
        [SerializeField] private Recipe[] _recipes;

        #endregion

        #region Properties
        
        public Recipe[] Recipes => _recipes;

        #endregion
    }
}
