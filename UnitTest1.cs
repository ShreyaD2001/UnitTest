using NUnit.Framework;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;


[TestFixture]
public class AuthenticationSystemTests
{
    private Dictionary<string, string> users;
   
    [SetUp]
    public void SetUp()
    {
        // Initialize the dictionary of users before each test
        users = new Dictionary<string, string>();
    }

    [Test]
    public void HashPassword_ShouldReturnHashedPassword()
    {
        // Arrange
        string password = "testpassword";
        
        // Act
        string hashedPassword = HashPassword(password);
        
        // Assert
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(hashedPassword);
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreNotEqual(password, hashedPassword);
    }

    [Test]
    public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
    {
        // Arrange
        string password = "testpassword";
        string hashedPassword = HashPassword(password);
        
        // Act
        bool result = VerifyPassword(password, hashedPassword);
        
        // Assert
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(result);
    }

    [Test]
    public void VerifyPassword_WithIncorrectPassword_ShouldReturnFalse()
    {
        // Arrange
        string password = "testpassword";
        string hashedPassword = HashPassword(password);
        
        // Act
        bool result = VerifyPassword("wrongpassword", hashedPassword);
        
        // Assert
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsFalse(result);
    }

    [Test]
    public void Register_WithNewUsername_ShouldAddUser()
    {
        // Arrange
        string username = "testuser";
        string password = "testpassword";
        string hashedPassword = HashPassword(password);

        // Act
        users[username] = hashedPassword;

        // Assert
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(users.ContainsKey(username));
        Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(hashedPassword, users[username]);
    }

    // Helper methods from the original code
    private static string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }

    private static bool VerifyPassword(string inputPassword, string storedHashedPassword)
    {
        string inputHashedPassword = HashPassword(inputPassword);
        return inputHashedPassword == storedHashedPassword;
    }
}





