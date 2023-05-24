/*
 * IPWorks ZIP 2022 JavaScript Edition - Demo Application
 *
 * Copyright (c) 2023 /n software inc. - All rights reserved. - www.nsoftware.com
 *
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

  const sevenzip = new ipworkszip.sevenzip();

  prompt('archiveFile', 'Please enter the name of the 7zip file to extract', ':', 'samplezip.7z');

  sevenzip.on('EndFile',(e) => {
    console.log('Extracted: ' + sevenzip.getFiles().get(e.index).getDecompressedName());
  });
  rl.on('line', async function(line) {
    switch (lastPrompt) {
      case 'archiveFile': {
        sevenzip.setArchiveFile(line === '' ? lastDefault : line);
        prompt('extractToPath', 'Please enter the path for extraction', ':', './');
        break;
      }
      case 'extractToPath': {
        sevenzip.setExtractToPath(line === '' ? lastDefault : line);
        console.log('Extracting...');;
        try {
          await sevenzip.extractAll();
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
