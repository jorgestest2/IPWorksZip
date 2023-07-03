# 
# IPWorks ZIP 2022 C++ Edition - Sample Project
# 
# This sample project demonstrates the usage of IPWorks ZIP in a 
# simple, straightforward way. This is not intended to be a complete 
# application. Error handling and other checks are simplified for clarity.
# 
# Copyright (c) 2023 /n software inc. www.nsoftware.com
# 

ifeq ($(shell uname -s), Darwin) # MACOS
R_PATH=-Wl,-rpath,../../lib64/,-rpath,.

FRAMEWORK = -framework Carbon -framework Security
LD_FLAGS = -lipworkszip.22.0 -L../../lib64/ -lresolv
LD_FLAGS_SRC = -lz -lresolv
OS_CFLAGS = -D UNIX -arch arm64
MACOS = "darwin% Darwin% macos%"

all:
	g++ $(OS_CFLAGS) -D UNIX -o createsevenzip createsevenzip.cpp  -I../../include/ $(LD_FLAGS) $(FRAMEWORK) $(R_PATH)

src: ../../src/ipworkszip.o
	g++ $(OS_CFLAGS) -o createsevenzip ../../src/ipworkszip.o createsevenzip.cpp  $(LD_FLAGS_SRC) $(FRAMEWORK)

../../src/ipworkszip.o:
	g++ -c -fno-exceptions -fno-rtti -fPIC -arch arm64 -o ../../src/ipworkszip.o ../../src/ipworkszip.cpp

else # LINUX
ifeq ($(shell uname -m), x86_64)
  LIB=lib64
else
  LIB=lib
endif

R_PATH=-Wl,-rpath,../../$(LIB)/,-rpath,.
LD_FLAGS = -lipworkszip -lcrypto -L../../$(LIB)/ -lcrypto
LD_FLAGS_SRC = -lz -lcrypto

all:
	g++ -D UNIX -o createsevenzip createsevenzip.cpp  -I../../include/ $(LD_FLAGS) $(R_PATH) 

src: ../../src/ipworkszip.o
	g++ -D UNIX -o createsevenzip ../../src/ipworkszip.o createsevenzip.cpp  -I../../include/ $(LD_FLAGS_SRC)

../../src/ipworkszip.o:
	g++ -c -fno-exceptions -fno-rtti -fPIC -o ../../src/ipworkszip.o ../../src/ipworkszip.cpp

clean:
	rm -f ../../src/*.o
	rm -f createsevenzip *.o
endif