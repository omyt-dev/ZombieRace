using UnityEngine;

namespace ZombieRace
{
    public static class StylizedLog
    {
        public static readonly Color Red = new(0.75f, 0.05f, 0.05f, 1f);
        public static readonly Color Blue = new(0.00f, 0.30f, 0.75f, 1f);
        public static readonly Color Yellow = new(1.00f, 0.90f, 0.35f, 1f); 
        public static readonly Color Green = new(0.00f, 0.40f, 0.08f, 1f);
        public static readonly Color Purple = new(0.48f, 0.40f, 0.85f, 1f);
        public static readonly Color Orange = new(0.85f, 0.25f, 0.02f, 1f);

        public static Color DefaultColor { get; set; } = Blue;

        public static bool BoldMessage { get; set; } = false;
        public static bool ColorizedMessage { get; set; } = false;

        public static bool BoldContext { get; set; } = true;
        public static bool ColorizedContext { get; set; } = true;

        public static char? ContextPrefix { get; set; } = '[';
        public static char? ContextPostfix { get; set; } = ']';

        public static char? MessagePrefix { get; set; } = null;
        public static char? MessagePostfix { get; set; } = null;

        public static void Log(string context, string message) => Log(context, message, DefaultColor);
        public static void Log(string context, string message, Color color)
        {
            context = Stylize(context, BoldContext, ColorizedContext ? color : null, ContextPrefix, ContextPostfix);
            message = Stylize(message, BoldMessage, ColorizedMessage ? color : null, MessagePrefix, MessagePostfix);

            Debug.Log($"{context} {message}");
        }
        

        public static string Stylize(string text, bool bold = false, Color? color = default, char? prefix = default, char? postfix = default)
        {
            string result = text;

            if (prefix.HasValue)
                result = $"{prefix.Value}{result}";
            if (postfix.HasValue)
                result = $"{result}{postfix.Value}";

            if (bold)
                result = GetBold(result);
            if (color.HasValue)
                result = GetColorized(result, color.Value);

            return result;
        }

        public static string GetColorized(string message, Color color)
        {
            return $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>";
        }
        public static string GetBold(string message)
        {
            return $"<b>{message}</b>";
        }

    }
}
