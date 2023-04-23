using PatdocQuantum;

namespace PadocQuantum {
    internal static class RequestHandler {
        public static void Handle(string input) {
            if (input is null)
                return;

            input = input.ToLower().Trim();
            foreach (string protocol in Program.protocols) {
                input = input.Replace(protocol.ToLower() + "://", "");
                input = input.Replace("\\", "/");
            }
            string[] dataPoints = input.Split("/");

            if (dataPoints.Length == 2 &&
                    (dataPoints[0] == "policies" ||
                        dataPoints[0] == "policy"
                    )
                ) {
                MessageBox.Show($"Open polis met ID: {dataPoints[1]}");
            }

            if (dataPoints.Length == 2 &&
                    (
                        input.StartsWith("claims") ||
                        input.StartsWith("claim")
                    )
                ) {
                MessageBox.Show($"Open schadegeval met ID: {dataPoints[1]}");
            }

            if (dataPoints.Length == 2 &&
                    (
                        input.StartsWith("users") ||
                        input.StartsWith("user")
                    )
                ) {
                MessageBox.Show($"Open gebruiker met ID: {dataPoints[1]}");
            }
        }
    }
}
