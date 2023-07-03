# 
# IPWorks ZIP 2022 Python Edition - Sample Project
# 
# This sample project demonstrates the usage of IPWorks ZIP in a 
# simple, straightforward way. This is not intended to be a complete 
# application. Error handling and other checks are simplified for clarity.
# 
# Copyright (c) 2023 /n software inc. www.nsoftware.com
# 

import sys
import string
from ipworkszip import *

input = sys.hexversion<0x03000000 and raw_input or input


def fireError(e):
  print("Error %i: %s\n" %(e.code, e.message))

zip = Zip()
try:
  print("Please enter the name of the zip file to extract [samplezip.zip]: "),
  buffer = input()
  if buffer == '':
    zip.set_archive_file("samplezip.zip")
  else:
    zip.set_archive_file(buffer)

  print("Please enter the path for extraction [./extractedfiles/]: "),
  buffer = input()
  if buffer == '':
    zip.set_extract_to_path("./extractedfiles/")
  else:
    zip.set_extract_to_path(buffer)

  zip.extract_all()

  print("Archive extracted.")
  print("\r\nPress enter to continue...")
  input()
  sys.exit(1)
except IPWorksZipError as e:
  fireError(e)

