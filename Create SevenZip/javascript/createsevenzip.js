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

  prompt('archivefile', 'Please enter the name of the 7z file to create', ':', 'test.7z');

  rl.on('line', async function(line) {
    switch (lastPrompt) {
      case 'archivefile': {
        sevenzip.setArchiveFile(line === '' ? lastDefault : line);
        prompt('recurse', 'Recurse subdirectories?', ':', 'n');
        break;
      }
      case 'recurse': {
        sevenzip.setRecurseSubdirectories(line.startsWith('y') || line.startsWith('Y'));
        prompt('folder', 'Please enter the path of the directory to compress', ':', './*');
        break;
      }
      case 'folder': {
        sevenzip.includeFiles(line === '' ? lastDefault : line);

        console.log('Compressing...');
        try {
          await sevenzip.compress();
          console.log('Compressed directory');
          process.exit();
        }
        catch (e) {
          console.log(e);
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
