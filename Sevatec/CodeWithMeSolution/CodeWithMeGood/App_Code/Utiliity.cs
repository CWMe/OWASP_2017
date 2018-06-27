using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Utiliity
/// </summary>
public class Utiliity
{
    public static void VerifyDir(string path)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                dir.Create();
            }
        }
        catch { }
    }

    public static void Logger(string lines)
    {
        string path = @"C:\Users\ahanif.SEVATEC\Documents\Visual Studio 2015\WebSites\CodeWithMe\Logs\";
        VerifyDir(path);
        string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
        try
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
            file.WriteLine(DateTime.Now.ToString() + ": " + lines);
            file.Close();
        }
        catch (Exception ex)
        {

        }
    }
}
