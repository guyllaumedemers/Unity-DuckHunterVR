    using System;
    using System.ComponentModel;
    using System.Reflection;

    public static class GameMode {
        public enum Mode {
            [Description("RegularMode")]
            REGULARMODE,
            [Description("TimedRound")]
            TIMEDROUND,
            [Description("ChallengeMode")]
            CHALLENGEMODE,
            [Description("TargetPractice")]
            TARGETPRACTICE
    };
        
        public static T ToEnum<T>(this string value, bool ignoreCase = true) {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
                
        public static string GetDescription(this Enum value) {
            
            Type type = value.GetType();
            string name = Enum.GetName(type, value);
            
            if (name != null) {
                FieldInfo field = type.GetField(name);
                if (field != null) {
                    if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attr)
                        return attr.Description;
                }
            }
            
            return null;
        }
    }