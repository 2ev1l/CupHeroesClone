namespace Game.DataBase
{
    [System.Serializable]
    public class AttackSpeed : Stat
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override StatType GetStatType() => StatType.AttackSpeed;
        public AttackSpeed(float value) : base(value) { }
        #endregion methods
    }
}