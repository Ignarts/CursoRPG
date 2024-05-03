using System;
using Items;

namespace Quests
{
    [Serializable]
    public class QuestRewardItem
    {
        public InventoryItems InventoryItemRewarded;
        public int Amount;
    }
}