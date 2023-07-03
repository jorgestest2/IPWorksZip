/*
 * IPWorks ZIP 2022 JavaScript Edition - Sample Project
 *
 * This sample project demonstrates the usage of IPWorks ZIP in a 
 * simple, straightforward way. This is not intended to be a complete 
 * application. Error handling and other checks are simplified for clarity.
 *
 * Copyright (c) 2023 /n software inc. www.nsoftware.com
 */
 
const readline = require("readline");
const ipworkszip = require("@nsoftware/ipworkszip");

if(!ipworkszip) {
  console.error("Cannot find ipworkszip.");
  process.exit(1);
}
let rl = readline.createInterface({
  input: process.stdin,
  output: process.stdout
});

main();

async function main() {

  const zip = new ipworkszip.zip();

  prompt('archiveFile', 'Please enter the name of the zip file to extract', ':', 'samplezip.zip');

  zip.on('EndFile',(e) => {
    console.log('Extracted: ' + zip.getFiles().get(e.index).getDecompressedName());
  });
  rl.on('line', async function(line) {
      switch (lastPrompt) {
          case 'archiveFile': {
              zip.setArchiveFile(line === '' ? lastDefault : line);
              prompt('extractToPath', 'Please enter the path for extraction', ':', './');
              break;
          }
          case 'extractToPath': {
              zip.setExtractToPath(line === '' ? lastDefault : line);
              console.log('Extracting...');
              try {
                await zip.extractAll();
                console.log('Archive extracted');
                process.exit();
              }
              catch (e) {
                console.log(e);
                process.exit();
              }
          }
      }
  });
}

function prompt(promptName, label, punctuation, defaultVal)
{
  lastPrompt = promptName;
  lastDefault = defaultVal;
  process.stdout.write(`${label} [${defaultVal}] ${punctuation} `);
}
