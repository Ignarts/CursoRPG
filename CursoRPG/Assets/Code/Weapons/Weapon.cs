using Sirenix.OdinInspector;
using UnityEngine;

namespace Weapons
{
    public enum WeaponType
    {
        Melee,
        Magic
    }

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon")]
    public class Weapon : ScriptableObject
    {
        #region Private Attributes

        [Header("Weapon Configuration")]
        [SerializeField] protected Sprite _weaponIcon;
        [SerializeField] protected Sprite _skillIcon;
        [SerializeField] protected WeaponType _weaponType;
        [SerializeField] protected int _weaponDamage;

        [ShowIf("_weaponType", WeaponType.Magic)]
        [Header("Magic Weapon Configuration")]
        public float _manaRequired;

        [Header("Stats")]
        [SerializeField] protected float _critChance;
        [SerializeField] protected float _blockChance;

        #endregion
    }
}