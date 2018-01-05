#include "SVTServer.h"

#ifdef __cplusplus
extern "C" {
#endif

__declspec(dllexport) void runInterface();
__declspec(dllexport) void command( int cmd, const char * param );

#ifdef __cplusplus
}
#endif

void runInterface()
{
    RunInterface();
}

void command( int cmd, const char * param )
{
    Command( cmd, (char*)param );
}