using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace account_ms.Services
{
public class PassCrip
{
    public string hashPass(string password)
    {
        string hashed = string.Empty;
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[20]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        
        byte[] hashBytes = new byte[40];
        Array.Copy(salt, 0, hashBytes, 0,20);
        Array.Copy(hash, 0, hashBytes, 20, 20);

        // 4.-Turn the combined salt+hash into a string for storage
        hashed = Convert.ToBase64String(hashBytes);
        return hashed;
    }


    public bool rehash(string password, string hashed)
    {
        bool aut = true;
        byte[] hashBytes = Convert.FromBase64String(hashed);
        byte[] salt = new byte[20];
        Array.Copy(hashBytes, 0, salt, 0, 20);
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000);
        byte[] hash = pbkdf2.GetBytes(20);
        for (int i = 0; i < 20; i++)
        {
            if (hashBytes[i + 20] != hash[i])
            {
                return false;
            }
        }
        return aut;
    }
}

}