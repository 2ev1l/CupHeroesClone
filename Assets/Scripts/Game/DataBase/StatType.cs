namespace Game.DataBase
{
    #region enum
    public enum StatType
    {
        Attack,
        AttackSpeed,
        Health
    }
    #endregion enum

    public static class StatTypeExtension
    {
        #region methods
        public static string ToDigitText(this StatType statType, float value) => statType switch
        {
            StatType.Attack => $"{value:F1}",
            StatType.AttackSpeed => $"{value:F1}",
            StatType.Health => $"{value:F0}",
            _ => throw new System.NotImplementedException($"digit text for {statType}"),
        };

        public static string ToLanguage(this StatType statType) => statType switch
        {
            StatType.Attack => LanguageInfo.GetTextByType(TextType.Game, 1),
            StatType.AttackSpeed => LanguageInfo.GetTextByType(TextType.Game, 2),
            StatType.Health => LanguageInfo.GetTextByType(TextType.Game, 10),
            _ => throw new System.NotImplementedException($"language for {statType}"),
        };
        #endregion methods
    }
}