/*
 * IPWorks ZIP 2022 Java Edition- Demo Application
 *
 * Copyright (c) 2023 /n software inc. - All rights reserved. - www.nsoftware.com
 *
 */

import java.io.*;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import ipworkszip.*;

public class openzip {
	Zip zip;
	String buffer;
	public openzip(){
		zip = new Zip();
		try{
			System.out.print("Please enter the name of the zip file to extract [samplezip.zip]: ");
			buffer = input();
		  if (buffer.length() > 0) zip.setArchiveFile(buffer);
		  else zip.setArchiveFile("samplezip.zip");
		  				
      System.out.print("Please enter the path for extraction [./extractedfiles/]: ");
		  buffer = input();
		  if (buffer.length() > 0) zip.setExtractToPath(buffer);
		  else zip.setExtractToPath("./extractedfiles/");
		  		  
		  zip.extractAll();
		  System.out.println("Archive extracted.");
			System.out.println("\r\nPress enter to continue...");
			input();
			System.exit(0);			  
		}catch(IPWorksZipException ex){
			System.out.println("IPWorksZIP exception thrown: " + ex.getCode() + " [" + ex.getMessage() + "].");
		}
		catch(Exception ex){
			System.out.println(ex.getMessage());
		}
	}
	
	public static void main(String[] args) {
	new openzip();
	}
	
	private String input() throws IOException
  {
     BufferedReader bf = new BufferedReader(new InputStreamReader(System.in));
     return bf.readLine();
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




