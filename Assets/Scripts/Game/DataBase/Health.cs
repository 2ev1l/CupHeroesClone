namespace Game.DataBase
{
    [System.Serializable]
    public class Health : Stat
    {
        #region fields & properties

        #endregion fields & properties

        #region methods
        public override StatType GetStatType() => StatType.Health;
        public Health(float value) : base(value) { }
        #endregion methods
    }
}