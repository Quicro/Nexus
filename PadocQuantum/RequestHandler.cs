using PadocEF;
using PadocQuantum.Logging;
using PatdocQuantum;

namespace PadocQuantum {
    internal static class RequestHandler {
        public static void Handle(string input) {
            LoggerBla.RequestHandlerStarted(input);
            if (input is null) {
                LoggerBla.RequestHandlerEnded();
                return;
            }


            input = input.ToLower().Trim();
            foreach (string protocol in QuantumStartup.protocols) {
                input = input.Replace(protocol.ToLower() + "://", "");
                input = input.Replace("\\", "/");
            }
            string[] dataPoints = input.Split("/");
            int id;
            int.TryParse(dataPoints[1].ToLower(), out id);

            if (dataPoints.Length == 2 && (dataPoints[0] == "policies" || dataPoints[0] == "policy")) {
                LoggerBla.RequestHandlerHit();
                new FormControllers.PolicyFormController(
                    PadocEF.Extentions.PolicyExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "claims" || dataPoints[0] == "claim")) {
                LoggerBla.RequestHandlerHit();
                new FormControllers.ClaimFormController(
                    PadocEF.Extentions.ClaimExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "clients" || dataPoints[0] == "client")) {
                LoggerBla.RequestHandlerHit();
                new FormControllers.ClientFormController(
                    PadocEF.Extentions.ClientExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "users" || dataPoints[0] == "user")) {
                LoggerBla.RequestHandlerHit();
                new FormControllers.UserFormController(
                    PadocEF.Extentions.UserExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "roles" || dataPoints[0] == "role")) {
                LoggerBla.RequestHandlerHit();
                new FormControllers.RoleFormController(
                    PadocEF.Extentions.RoleExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "permissions" || dataPoints[0] == "permission")) {
                LoggerBla.RequestHandlerHit();
                new FormControllers.PermissionFormController(
                    PadocEF.Extentions.PermissionExtention.getQueryable(id)
                ).loadGrid();
            }

            LoggerBla.RequestHandlerEnded();
        }
    }
}
