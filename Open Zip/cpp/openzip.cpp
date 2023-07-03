/*
 * IPWorks ZIP 2022 C++ Edition - Sample Project
 *
 * This sample project demonstrates the usage of IPWorks ZIP in a 
 * simple, straightforward way. This is not intended to be a complete 
 * application. Error handling and other checks are simplified for clarity.
 *
 * Copyright (c) 2023 /n software inc. www.nsoftware.com
 */

#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "../../include/ipworkszip.h"
#define LINE_LEN 100

int main()
{

	int			ret_code = 0;
	Zip zip;
	char		buffer[LINE_LEN+1];

	printf("**********************************************************\n");
	printf("* This is a demo of the Zip component                    *\n");
	printf("**********************************************************\n\n");

	printf("Please enter the name of the archive to extract [samplezip.zip]: " );
	fgets(buffer,LINE_LEN,stdin);
	buffer[strlen(buffer)-1] = '\0';
	if (strlen(buffer) == 0) strcat(buffer,"samplezip.zip");
	zip.SetArchiveFile(buffer);

	printf("Please enter the path to which the files will be extracted [. (current dir)]: ");
	fgets(buffer,LINE_LEN,stdin);
	buffer[strlen(buffer)-1] = '\0';
	if (strlen(buffer) == 0) strcat(buffer,".");
	zip.SetExtractToPath(buffer);

	printf("\nExtracting ...\n\n");
	ret_code = zip.ExtractAll();

	if(!ret_code)
		printf("Completed.\n\n");
	else
		printf("Error extracting: [%i] %s\n",ret_code,zip.GetLastError());


	fprintf(stderr, "\npress <return> to continue...\n");
	getchar();

	return ret_code;
}


