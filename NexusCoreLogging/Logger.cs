namespace NexusCore.Logging {
    public static class Logger {
        public static void ApplicationStarted() => LogInfo(1, "Application started");
        public static void ApplicationEnded() => LogDebug(2, "Application ended");
        public static void ApplicationCrashed() => LogError(-3, "Application crashed");
        public static void ApplicationProtocolCreated() => LogInfo(4, "Application Protocol created (pa://, Nexus:// & patdoc://)");
        public static void RequestHandlerStarted(string input) => LogInfo(5, $"Request Handler started {(input == null ? "" : $"(input: {input})")}");
        public static void RequestHandlerHit() => LogInfo(6, $"Request Handler hit");
        public static void RequestHandlerEnded() => LogDebug(7, "Request Handler ended");
        public static void FormControllerStarted(Type formController, int ID) => LogInfo(8, $"{formController.Name} started with ID: {ID}");
        public static void FormControllerStartedBy(Type startFormController, int startID, Type endFormController, int endID) => LogInfo(9, $"{startFormController.Name} with ID: {startID} started {endFormController.Name} with ID: {endID}");
        public static void FormControllerClosed(Type formController, int ID) => LogInfo(10, $"{formController.Name} closed with ID: {ID}");
        public static void DatabaseManagerError(string msg) => LogError(-11, $"Loading data caused an error: \n{msg}");
        public static void OpenForbiddenTypeError(string typeName) => LogError(-12, $"User tried to open the type {typeName} in typeViewer");
        public static void ViewerPacketHasNoEntitiesError() => LogError(-13, $"...");
        public static void ConnectionToDatabaseFailedError() => LogInfo(-14, $"Cannot connect to database");

        public static void TEMPLATE() => LogInfo(0, $"");

        public static void logTitle(string title) { Console.Title = title; }
        public static void logHeader(string text) { Write(text, ' ', ConsoleColor.Black, ConsoleColor.White); }
        public static void debug(string text) { Write(text.ToString() + "\n", 'D', ConsoleColor.Green); }
        public static void info(string text) { Write(text + "\n", 'I', ConsoleColor.Blue); }
        public static void error(string text) { Write(text + "\n", 'E', ConsoleColor.Red); }
        private static void LogDebug(int v1, string v2) {
            debug($"[{v1}] {v2}");
        }
        private static void LogInfo(int v1, string v2) {
            info($"[{v1}] {v2}");
        }
        private static void LogError(int v1, string v2) {
            error($"[{v1}] {v2}");
        }

        static void Write(string text, char letter = '?', ConsoleColor ForegroundColor = ConsoleColor.Gray, ConsoleColor BackgroundColor = System.ConsoleColor.Black) {
            ConsoleColor startingForegroundColor = System.Console.ForegroundColor;
            ConsoleColor startingBackgroundColor = System.Console.BackgroundColor;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" " + letter + " ");
            Console.ForegroundColor = ForegroundColor;
            Console.BackgroundColor = BackgroundColor;
            Console.Write("  ");
            Console.Write(text);
            Console.ForegroundColor = startingForegroundColor;
            Console.BackgroundColor = startingBackgroundColor;
        }

    }
}
