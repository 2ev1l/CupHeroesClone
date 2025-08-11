namespace Game.DataBase
{
    [System.Serializable]
    public class Attack : Stat
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override StatType GetStatType() => StatType.Attack;
        public Attack(float value) : base(value) { }
        #endregion methods
    }
}