namespace PadocAI {
    internal static class Cache {
        public static Dictionary<string, CacheItem> items = new();

        static Cache() {
            CacheItem currentCacheItem;

            currentCacheItem = new CacheItem() {
                question = "Ik wil graag de permissies zien van user WILLEM",
                answer = "In dit geval heeft de gebruiker 'WILLEM' toegang tot alle permissies omdat hij is toegewezen aan de \"\"ALL\"\" rol.\"",
                sql = @"SELECT p.*
FROM [pa].[User] u
JOIN [pa].[UserRole] ur ON ur.UserId = u.Id
JOIN [pa].[RolePermission] rp ON rp.RoleId = ur.RoleId
JOIN [pa].[Permission] p ON p.Id = rp.PermissionId
WHERE u.Name = 'WILLEM'
"
            };
            items.Add(currentCacheItem.question, currentCacheItem);

            currentCacheItem = new CacheItem() {
                question = "Welke permissies heeft de rol ADMIN?",
                answer = @"De permissies die bij de rol ADMIN horen zijn:

StartMenu.Admin (ID 2)
StartMenu.Dossier.Schade (ID 3)
StartMenu.Dossier.Polis (ID 4)",
                sql = @"SELECT p.*
FROM[pa].[Role] r
JOIN[pa].[RolePermission] rp ON rp.RoleId = r.Id
JOIN[pa].[Permission] p ON p.Id = rp.PermissionId
WHERE r.Name = 'Administrator'"
            };
            items.Add(currentCacheItem.question, currentCacheItem);

            currentCacheItem = new CacheItem() {
                question = "Welke claims heeft polis A",
                answer = @"Polis A heeft de volgende claims: Valongeval (nummer 23512345)",
                sql = @"SELECT * FROM Claim WHERE PolicyID = (SELECT ID FROM Policy WHERE Number = 'A')"
            };
            items.Add(currentCacheItem.question, currentCacheItem);


            currentCacheItem = new CacheItem() {
                question = "Ik wil graag de polissen zien van de klant A",
                answer = @"Het lijkt erop dat klant A drie polissen heeft:

Autoverzekering met polisnummer 485984868
Brandverzekering met polisnummer 484984894
Aansprakelijkheidsverzekering (BA) met polisnummer 959559489",
                sql = @"SELECT *
FROM Policy
WHERE ClientID = 'A'"
            };
            items.Add(currentCacheItem.question, currentCacheItem);
        }

    }

    public class CacheItem {
        public string question;
        public string answer;
        public string sql;
    }
}
