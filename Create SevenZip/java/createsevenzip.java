/*
 * IPWorks ZIP 2022 Java Edition - Sample Project
 *
 * This sample project demonstrates the usage of IPWorks ZIP in a 
 * simple, straightforward way. This is not intended to be a complete 
 * application. Error handling and other checks are simplified for clarity.
 *
 * Copyright (c) 2023 /n software inc. www.nsoftware.com
 */

import java.io.*;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import ipworkszip.*;

public class createsevenzip extends ConsoleDemo {
	Sevenzip sevenzip;
	String buffer;

	public createsevenzip() {
		sevenzip = new Sevenzip();

		try {
			sevenzip.setArchiveFile(prompt("Please enter the name of the 7z file to create", ":", "test.7z"));
			sevenzip.includeFiles(prompt("Please enter the path of the directory to compress", ":", "./*"));
			char ch = ask("Recurse Subdirectories");
			if (ch == 'n' || ch == 'N')
				sevenzip.setRecurseSubdirectories(false);

			System.out.println("Compressing...");
			sevenzip.compress();
			System.out.println("Directory compressed.");
		} catch (IPWorksZipException ex) {
			System.out.println("IPWorksZup exception thrown: " + ex.getCode() + " [" + ex.getMessage() + "].");
		} catch (Exception ex) {
			System.out.println(ex.getMessage());
		}
	}

	public static void main(String[] args) {
		new createsevenzip();
	}
}


class ConsoleDemo {
  private static BufferedReader bf = new BufferedReader(new InputStreamReader(System.in));

  static String input() {
    try {
      return bf.readLine();
    } catch (IOException ioe) {
      return "";
    }
  }
  static char read() {
    return input().charAt(0);
  }

  static String prompt(String label) {
    return prompt(label, ":");
  }
  static String prompt(String label, String punctuation) {
    System.out.print(label + punctuation + " ");
    return input();
  }

  static String prompt(String label, String punctuation, String defaultVal)
  {
	System.out.print(label + " [" + defaultVal + "] " + punctuation + " ");
	String response = input();
	if(response.equals(""))
		return defaultVal;
	else
		return response;
  }

  static char ask(String label) {
    return ask(label, "?");
  }
  static char ask(String label, String punctuation) {
    return ask(label, punctuation, "(y/n)");
  }
  static char ask(String label, String punctuation, String answers) {
    System.out.print(label + punctuation + " " + answers + " ");
    return Character.toLowerCase(read());
  }

  static void displayError(Exception e) {
    System.out.print("Error");
    if (e instanceof IPWorksZipException) {
      System.out.print(" (" + ((IPWorksZipException) e).getCode() + ")");
    }
    System.out.println(": " + e.getMessage());
    e.printStackTrace();
  }
}




