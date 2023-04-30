using PadocEF;
using PadocQuantum.Logging;
using PatdocQuantum;

namespace PadocQuantum {
    internal static class RequestHandler {
        public static void Handle(string input) {
            Logger.RequestHandlerStarted(input);
            if (input is null) {
                Logger.RequestHandlerEnded();
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
                Logger.RequestHandlerHit();
                new FormControllers.PolicyFormController(
                    PadocEF.Extentions.PolicyExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "claims" || dataPoints[0] == "claim")) {
                Logger.RequestHandlerHit();
                new FormControllers.ClaimFormController(
                    PadocEF.Extentions.ClaimExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "clients" || dataPoints[0] == "client")) {
                Logger.RequestHandlerHit();
                new FormControllers.ClientFormController(
                    PadocEF.Extentions.ClientExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "users" || dataPoints[0] == "user")) {
                Logger.RequestHandlerHit();
                new FormControllers.UserFormController(
                    PadocEF.Extentions.UserExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "roles" || dataPoints[0] == "role")) {
                Logger.RequestHandlerHit();
                new FormControllers.RoleFormController(
                    PadocEF.Extentions.RoleExtention.getQueryable(id)
                ).loadGrid();
            }
            if (dataPoints.Length == 2 && (dataPoints[0] == "permissions" || dataPoints[0] == "permission")) {
                Logger.RequestHandlerHit();
                new FormControllers.PermissionFormController(
                    PadocEF.Extentions.PermissionExtention.getQueryable(id)
                ).loadGrid();
            }

            Logger.RequestHandlerEnded();
        }
    }
}
