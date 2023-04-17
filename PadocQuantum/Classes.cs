namespace PatdocQuantum {
    /*internal sealed class Users {
        public static List<User> users = new List<User>();

        static Users() {
            users.Add(new User() {
                username = "qc",
                password = "qc",
                fullname = "Quinten",
                UserRoles = new() {
                    Roles.get("superuser")
                }
            });

            users.Add(new User() {
                username = "SV",
                password = "sv",
                fullname = "Steven",
                UserRoles = new() {
                    Roles.get("admin")
                }
            });
            users.Add(new User() {
                username = "P",
                password = "",
                fullname = "Productie",
                UserRoles = new() {
                    Roles.get("Productie")
                }
            });
            users.Add(new User() {
                username = "s",
                password = "",
                fullname = "Claims",
                UserRoles = new() {
                    Roles.get("Claims")
                }
            });
        }

        public static User get(string username) {
            return users.Single(user => user.username == username);
        }
        public static User? validate(string username, string password) {
            return users.FirstOrDefault(
                user => user.username.ToLower().Trim() == username.ToLower().Trim() &&
                user.password.ToLower().Trim() == password.ToLower().Trim()
            );
        }
    }


    internal class User {
        public string username;
        public string password;
        public string fullname;

        public List<Role> UserRoles = new List<Role>();
    }

    internal sealed class Roles {
        public static List<Role> roles = new List<Role>();

        static Roles() {
            roles.Add(new Role() { RoleName = "superuser" });
            roles.Add(new Role() { RoleName = "Admin" });
            roles.Add(new Role() { RoleName = "Productie" });
            roles.Add(new Role() { RoleName = "Claims" });
        }

        public static Role? get(string rolename) {
            return roles.FirstOrDefault(user => user.RoleName.ToLower().Trim() == rolename.ToLower().Trim());
        }
    }

    public class Role {
        public string RoleName;
    }



    internal sealed class Policies {
        public static List<Policy> policies = new List<Policy>();

        static Policies() {
            policies.Add(new Policy() { PolicyName = "Auto Insurance", PolicyNumber = "P0002", Holder = People.get("2"), StartDate = new DateTime(2022, 1, 1), EndDate = new DateTime(2023, 1, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Home Insurance", PolicyNumber = "P0003", Holder = People.get("3"), StartDate = new DateTime(2022, 2, 1), EndDate = new DateTime(2023, 2, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Life Insurance", PolicyNumber = "P0004", Holder = People.get("4"), StartDate = new DateTime(2022, 3, 1), EndDate = new DateTime(2023, 3, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Pet Insurance", PolicyNumber = "P0005", Holder = People.get("5"), StartDate = new DateTime(2022, 4, 1), EndDate = new DateTime(2023, 4, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Travel Insurance", PolicyNumber = "P0006", Holder = People.get("6"), StartDate = new DateTime(2022, 5, 1), EndDate = new DateTime(2023, 5, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Dental Insurance", PolicyNumber = "P0007", Holder = People.get("7"), StartDate = new DateTime(2022, 6, 1), EndDate = new DateTime(2023, 6, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Disability Insurance", PolicyNumber = "P0008", Holder = People.get("8"), StartDate = new DateTime(2022, 7, 1), EndDate = new DateTime(2023, 7, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Vision Insurance", PolicyNumber = "P0009", Holder = People.get("9"), StartDate = new DateTime(2022, 8, 1), EndDate = new DateTime(2023, 8, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Boat Insurance", PolicyNumber = "P0010", Holder = People.get("10"), StartDate = new DateTime(2022, 9, 1), EndDate = new DateTime(2023, 9, 1), Status = "Active" });
            policies.Add(new Policy() { PolicyName = "Flood Insurance", PolicyNumber = "P0011", Holder = People.get("11"), StartDate = new DateTime(2022, 10, 1), EndDate = new DateTime(2023, 10, 1), Status = "Active" });
        }

        public static Policy? get(string PolicyNumber) {
            return policies.FirstOrDefault(user => user.PolicyNumber.ToLower().Trim() == PolicyNumber.ToLower().Trim());
        }
    }

    public class Policy {
        public string PolicyNumber { get; set; }
        public string PolicyName { get; set; }
        public Person Holder { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
    }


    internal sealed class People {
        public static List<Person> people = new List<Person>();

        static People() {
            people.Add(new Person() { personID = "2", FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1980, 1, 1), Email = "john.doe@example.com", Phone = "555-1234" });
            people.Add(new Person() { personID = "3", FirstName = "Jane", LastName = "Doe", DateOfBirth = new DateTime(1985, 6, 30), Email = "jane.doe@example.com", Phone = "555-5678" });
            people.Add(new Person() { personID = "4", FirstName = "Bob", LastName = "Smith", DateOfBirth = new DateTime(1975, 12, 15), Email = "bob.smith@example.com", Phone = "555-9876" });
            people.Add(new Person() { personID = "5", FirstName = "Sara", LastName = "Johnson", DateOfBirth = new DateTime(1990, 4, 20), Email = "sara.johnson@example.com", Phone = "555-5555" });
            people.Add(new Person() { personID = "6", FirstName = "David", LastName = "Lee", DateOfBirth = new DateTime(1982, 9, 10), Email = "david.lee@example.com", Phone = "555-4444" });
            people.Add(new Person() { personID = "7", FirstName = "Emily", LastName = "Wang", DateOfBirth = new DateTime(1995, 2, 28), Email = "emily.wang@example.com", Phone = "555-3333" });
            people.Add(new Person() { personID = "8", FirstName = "Alex", LastName = "Davis", DateOfBirth = new DateTime(1987, 7, 4), Email = "alex.davis@example.com", Phone = "555-2222" });
            people.Add(new Person() { personID = "9", FirstName = "Jessica", LastName = "Liu", DateOfBirth = new DateTime(1983, 3, 12), Email = "jessica.liu@example.com", Phone = "555-1111" });
            people.Add(new Person() { personID = "10", FirstName = "Kevin", LastName = "Nguyen", DateOfBirth = new DateTime(1992, 11, 5), Email = "kevin.nguyen@example.com", Phone = "555-9999" });
            people.Add(new Person() { personID = "11", FirstName = "Maria", LastName = "Garcia", DateOfBirth = new DateTime(1978, 8, 20), Email = "maria.garcia@example.com", Phone = "555-7777" });

        }

        public static Person? get(string personID) {
            return people.FirstOrDefault(user => user.personID.ToLower().Trim() == personID.ToLower().Trim());
        }
    }

    public class Person {
        public string personID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }*/
}
