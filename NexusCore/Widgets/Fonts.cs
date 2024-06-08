using System.Drawing;

namespace NexusCore.Widgets {
    internal static class Fonts {

        /// <summary> Underlined text </summary>
        public static Font fontReference = new("Microsoft Sans Serif", 8.5f, FontStyle.Underline);
        /// <summary> Regular text </summary>
        public static Font fontDefault = new("Microsoft Sans Serif", 8.5f, FontStyle.Regular);
        /// <summary> Bold text </summary>
        public static Font fontSelected = new("Microsoft Sans Serif", 8.5f, FontStyle.Bold);
        /// <summary> Italic text </summary>
        public static Font fontNull = new("Microsoft Sans Serif", 8.5f, FontStyle.Italic);
        /// <summary> Italic text </summary>
        public static Font fontHeader = new("Microsoft Sans Serif", 8.5f, FontStyle.Bold);
    }
}
