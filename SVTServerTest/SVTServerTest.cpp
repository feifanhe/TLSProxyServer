// SVTServerTest.cpp

#include "stdafx.h"
#include "Windows.h"
#include <string.h>
#include <exception>
#include <iostream>

using namespace std;

typedef void (*RunFunc)();
typedef void (*CommandFunc)( int, const char * );

HINSTANCE	hinstSVTServerDll;

RunFunc	runInterface;
CommandFunc	command;

bool LoadSVTServerDll()
{
	// Load DLL file  
	hinstSVTServerDll = LoadLibrary( L"SVTServer.dll" );
	if ( hinstSVTServerDll == NULL ) {  
		printf("ERROR: unable to load DLL: %d\n", GetLastError());
		return false;
	}

	// Get function pointer
	runInterface = (RunFunc)GetProcAddress( hinstSVTServerDll, "runInterface" );
	if ( runInterface == NULL ) {  
		printf("ERROR: unable to find DLL function: runInterface\n");  
		FreeLibrary(hinstSVTServerDll);
		return false;
	}

	command = (CommandFunc)GetProcAddress( hinstSVTServerDll, "command" );
	if ( command == NULL ) {  
		printf("ERROR: unable to find DLL function: command\n");  
		FreeLibrary(hinstSVTServerDll);
		return false;
	}

	return true;
}

void FreeSVTServerDll()
{
	// Unload DLL file  
	puts( "Unload DLL" );
	FreeLibrary( hinstSVTServerDll ); 
}

#define STOP_INTERFACE -1
#define START_SERVER 0
#define STOP_SERVER 1
#define SET_KEYS 2

int _tmain(int argc, _TCHAR* argv[])
{
	if ( LoadSVTServerDll() )
	{
		try {
			runInterface();
			command(SET_KEYS, "ca.crt;ca.key;ca.crt");
			command(START_SERVER, ":8800");
			getchar();
			command(STOP_SERVER, NULL);
			command(-1, NULL);
		}
		catch (exception& e) {
			cout << "Exception: " << e.what() << endl;
			getchar();
		}

		FreeSVTServerDll();
	}

	return 0;
}

