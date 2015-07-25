using System;

namespace NinjaHive.Contract.DTOs
{
    public class GameItem
    {
        public Guid ItemId                  { get; set; }

        public string Name                  { get; set; }
        public string Description           { get; set; }
        public string Category              { get; set; }

        public bool Craftable               { get; set; }
        public bool UpgradeElement          { get; set; }
        public bool CraftingElement         { get; set; }
        public bool QuestItem               { get; set; }

        public int Value                    { get; set; }
    }
}
