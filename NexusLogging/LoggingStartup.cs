namespace NexusLogging {
    public class LoggingStartup {
        private static void Main(string[] args) {
        }


    }

    public class Logger {
        public static void ApplicationStarted() {
            LogInfo(1, "Application started");
        }

        public static void ApplicationEnded() {
            LogDebug(2, "Application ended");
        }

        public static void ApplicationCrashed() {
            LogError(-3, "Application crashed");
        }

        public static void ApplicationProtocolCreated() {
            LogInfo(4, "Application Protocol created (pa://, Nexus:// & patdoc://)");
        }

        public static void RequestHandlerStarted(string input) {
            LogInfo(5, $"Request Handler started {( input == null ? "" : $"(input: {input})" )}");
        }

        public static void RequestHandlerHit() {
            LogInfo(6, $"Request Handler hit");
        }

        public static void RequestHandlerEnded() {
            LogDebug(7, "Request Handler ended");
        }

        public static void FormControllerStarted(Type formController, int ID) {
            LogInfo(8, $"{formController.Name} started with ID: {ID}");
        }

        public static void FormControllerStartedBy(Type startFormController, int startID, Type endFormController, int endID) {
            LogInfo(9, $"{startFormController.Name} with ID: {startID} started {endFormController.Name} with ID: {endID}");
        }

        public static void FormControllerClosed(Type formController, int ID) {
            LogInfo(10, $"{formController.Name} closed with ID: {ID}");
        }

        public static void DatabaseManagerError(string msg) {
            LogError(-11, $"Loading data caused an error: \n{msg}");
        }

        public static void OpenForbiddenTypeError(string typeName) {
            LogError(-12, $"User tried to open the type {typeName} in typeViewer");
        }

        public static void TEMPLATE() {
            LogInfo(0, $"");
        }

        public static void ViewerPacketHasNoEntitiesError() {
            LogError(-13, $"Viewer Packet Has No Entities");
        }

        public static void logHeader(string text) { LogDebug(text); }

        private const ConsoleColor debug = ConsoleColor.Green;
        private const ConsoleColor info = ConsoleColor.Blue;
        private const ConsoleColor error = ConsoleColor.Red;

        private static void Log(ConsoleColor consoleColor, string msg, char firstChar, int eventID = 0) {
            ConsoleColor oldForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;



            if (eventID == 0) {
                Console.WriteLine($"{firstChar} {DateTime.Now:yyyy-MM-dd hh:mm:ss:fff}\t {msg}");
            } else {
                Console.WriteLine($"{firstChar} {DateTime.Now:yyyy-MM-dd hh:mm:ss:fff}[{eventID}]\t {msg}");
            }

            Console.ForegroundColor = oldForegroundColor;
        }


        public static void LogDebug(string msg) {
#if DEBUG
            Log(debug, msg, 'D');
#endif
        }

        public static void LogInfo(string msg) {
            Log(info, msg, 'I');
        }

        public static void LogError(string msg) {
            Log(error, msg, 'E');
        }

        private static void LogDebug(int eventID, string msg) {
#if DEBUG
            Log(debug, msg, 'D', eventID);
#endif
        }

        private static void LogInfo(int eventID, string msg) {
            Log(info, msg, 'I', eventID);
        }

        private static void LogError(int eventID, string msg) {
            Log(error, msg, 'E', eventID);
        }
    }
}
