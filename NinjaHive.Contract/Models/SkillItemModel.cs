namespace NinjaHive.Contract.Models
{
    public class SkillItemModel: GameItemModel
    {
        public SkillItemModel(): base()
        {
            StatInfo = new StatInfoModel();
            Skill = new SkillModel();
        }

        public StatInfoModel StatInfo { get; set; }

        public SkillModel Skill { get; set; }
    }
}
