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

	printf("Please enter the name of the archive to create [test.zip]: " );
	fgets(buffer,LINE_LEN,stdin);
	buffer[strlen(buffer)-1] = '\0';
	if (strlen(buffer) == 0) strcat(buffer,"test.zip");
	zip.SetArchiveFile(buffer);

	printf("Recurse Sub-Directories? [N]: ");
	fgets(buffer,LINE_LEN,stdin);
	buffer[strlen(buffer)-1] = '\0';
	if (strlen(buffer) == 0) strcat(buffer,"N");

	if(strcmp("n",buffer) == 0 || strcmp("N",buffer) == 0)
		zip.SetRecurseSubdirectories(false);

	printf("Please enter the path and filemask of the directory to compress [*.cpp]: ");
	fgets(buffer,LINE_LEN,stdin);
	buffer[strlen(buffer)-1] = '\0';
	if (strlen(buffer) == 0) strcat(buffer,"*.cpp");
	zip.IncludeFiles(buffer);

	printf("\nCompressing ...\n\n");
	ret_code = zip.Compress();

	if(!ret_code)
		printf("Completed.\n\n");
	else
		printf("Error compressing: [%i] %s\n",ret_code,zip.GetLastError());


	fprintf(stderr, "\npress <return> to continue...\n");
	getchar();

	return ret_code;
}


