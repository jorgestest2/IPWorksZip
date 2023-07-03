/*
 * IPWorks ZIP 2022 .NET Edition - Sample Project
 *
 * This sample project demonstrates the usage of IPWorks ZIP in a 
 * simple, straightforward way. This is not intended to be a complete 
 * application. Error handling and other checks are simplified for clarity.
 *
 * Copyright (c) 2023 /n software inc. www.nsoftware.com
 */

using System.Collections.Generic;
﻿using System;
using System.Threading.Tasks;
using nsoftware.async.IPWorksZip;

class selfextractorDemo
{
  private static Zipsfx zipsfx = new nsoftware.async.IPWorksZip.Zipsfx();

  static async Task Main(string[] args)
  {
    if (args.Length < 4)
    {
      Console.WriteLine("usage: zipsfx /d dir /n name [/t title] [/b banner]");
      Console.WriteLine("  dir       the source directory");
      Console.WriteLine("  name      the name of the output exe to create");
      Console.WriteLine("  title     the extractor title (optional, default \"IPWorks Zip Self Extractor - www.nsoftware.com\")");
      Console.WriteLine("  banner    the extractor banner text (optional, default empty)");
      Console.WriteLine("\r\nExample: zipsfx /d C:/test /n myextractor.exe /t \"My Software Extractor\"");
    }
    else
    {
      zipsfx.OnProgress += zipsfx_OnProgress;

      try
      {
        Dictionary<string, string> myArgs = ConsoleDemo.ParseArgs(args);

        zipsfx.SourceDirectory = myArgs["d"];
        zipsfx.ArchiveFile = myArgs["n"];
        if (myArgs.ContainsKey("t")) zipsfx.CaptionText = myArgs["t"];
        if (myArgs.ContainsKey("b")) zipsfx.BannerText = myArgs["b"];

        // So that an entire directory tree is not mistakenly zipped.
        zipsfx.RecurseSubdirectories = false;

        await zipsfx.CreateSFX();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }

  private static void zipsfx_OnProgress(object sender, ZipsfxProgressEventArgs e)
  {
    Console.WriteLine(e.PercentProcessed + "% processed");
  }
}


class ConsoleDemo
{
  public static Dictionary<string, string> ParseArgs(string[] args)
  {
    Dictionary<string, string> dict = new Dictionary<string, string>();

    for (int i = 0; i < args.Length; i++)
    {
      // If it starts with a "/" check the next argument.
      // If the next argument does NOT start with a "/" then this is paired, and the next argument is the value.
      // Otherwise, the next argument starts with a "/" and the current argument is a switch.

      // If it doesn't start with a "/" then it's not paired and we assume it's a standalone argument.

      if (args[i].StartsWith("/"))
      {
        // Either a paired argument or a switch.
        if (i + 1 < args.Length && !args[i + 1].StartsWith("/"))
        {
          // Paired argument.
          dict.Add(args[i].TrimStart('/'), args[i + 1]);
          // Skip the value in the next iteration.
          i++;
        }
        else
        {
          // Switch, no value.
          dict.Add(args[i].TrimStart('/'), "");
        }
      }
      else
      {
        // Standalone argument. The argument is the value, use the index as a key.
        dict.Add(i.ToString(), args[i]);
      }
    }
    return dict;
  }
}