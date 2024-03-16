using System;
using System.Linq;

namespace KTPS.Model.Helpers;

public static class RandomString
{
    public static string GenerateRandomString()
    {
        var magickNumber = 6;
        const string chars = "ABCDEFLEAGUEOFLEGENDSGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, magickNumber).Select(s => s[random.Next(s.Length)]).ToArray());
    }
}