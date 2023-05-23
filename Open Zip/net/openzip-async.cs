/*
 * IPWorks ZIP 2022 .NET Edition - Demo Application
 * Copyright (c) 2023 /n software inc. - All rights reserved. - www.nsoftware.com
 */

using System.Collections.Generic;
﻿using System;
using System.Text;
using System.Threading.Tasks;
using nsoftware.async.IPWorksZip;

class openzipDemo
{
  private static Zip openzip = new nsoftware.async.IPWorksZip.Zip();

  static async Task Main(string[] args)
  {
    if (args.Length < 2)
    {
      Console.WriteLine("usage: openzip /f inputfile\n");
      Console.WriteLine("  inputfile       the path to the zip, gzip, tar, or jar archive");
      Console.WriteLine("\nExample: openzip /f ..\\..\\..\\samplezip.zip\n");
    }
    else
    {
      try
      {
        Dictionary<string, string> myArgs = ConsoleDemo.ParseArgs(args);

        openzip.ArchiveFile = myArgs["f"];
        await openzip.Config("UseIBM437ASDefaultEncoding=false");

        // Process user commands.
        Console.WriteLine("Type \"?\" or \"help\" for a list of commands.");
        Console.Write("openzip> ");
        string command;
        string[] arguments;

        while (true)
        {
          command = Console.ReadLine();
          arguments = command.Split();

          if (arguments[0] == "?" || arguments[0] == "help")
          {
            Console.WriteLine("Commands: ");
            Console.WriteLine("  ?                                      display the list of valid commands");
            Console.WriteLine("  help                                   display the list of valid commands");
            Console.WriteLine("  scan                                   scan the compressed archive");
            Console.WriteLine("  extract <files> <path>                 extract the specified files to the specified path (separate multiple files with |)");
            Console.WriteLine("  extractall <path>                      extract all files in the archive to the specified path");
            Console.WriteLine("  quit                                   exit the application");
          }
          else if (arguments[0] == "scan")
          {
            await openzip.Scan();
            foreach (ZIPFile file in openzip.Files)
            {
              Console.WriteLine(file.CompressedName);
            }
          }
          else if (arguments[0] == "extract")
          {
            if (arguments.Length > 2)
            {
              openzip.ExtractToPath = arguments[2];
              await openzip.Extract(arguments[1]);
            }
          }
          else if (arguments[0] == "extractall")
          {
            if (arguments.Length > 1)
            {
              openzip.ExtractToPath = arguments[1];
              await openzip.ExtractAll();
            }
          }
          else if (arguments[0] == "quit" || arguments[0] == "exit")
          {
            break;
          }
          else if (arguments[0] == "")
          {
            // Do nothing.
          }
          else
          {
            Console.WriteLine("Invalid command.");
          } // End of command checking.

          Console.Write("openzip> ");
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Error: " + ex.Message);
      }
    }
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
        if (i + 1 < args.Length & !args[i + 1].StartsWith("/"))
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