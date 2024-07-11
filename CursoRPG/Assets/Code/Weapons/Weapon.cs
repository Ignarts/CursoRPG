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
        [ShowIf("_weaponType", WeaponType.Magic)]
        [SerializeField] protected Sprite _skillIcon;
        [SerializeField] protected WeaponType _weaponType;
        [SerializeField] protected int _weaponDamage;

        [ShowIf("_weaponType", WeaponType.Magic)]
        [Header("Magic Weapon Configuration")]
        [SerializeField] private int _manaRequired;
        [ShowIf("_weaponType", WeaponType.Magic)]
        [SerializeField] private Projectile _projectilePrefab;

        [Header("Stats")]
        [SerializeField] protected float _critChance;
        [SerializeField] protected float _blockChance;

        #endregion

        #region Properties

        public Sprite WeaponIcon => _weaponIcon;
        public Sprite SkillIcon => _skillIcon;
        public WeaponType WeaponType => _weaponType;
        public int WeaponDamage => _weaponDamage;
        public float WeaponCritChance => _critChance;
        public float WeaponBlockChance => _blockChance;
        public int ManaRequired => _manaRequired;
        public Projectile ProjectilePrefab => _projectilePrefab;
        
        #endregion
    }
}