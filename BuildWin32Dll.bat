set GOOS=windows
set GOARCH=386
go build -buildmode=c-archive .\SVTServer.go
gcc -shared -pthread -o SVTServer.dll SVTServer.c SVTServer.a -lWinMM -lntdll -lWS2_32 -m32
copy /y SVTServer.dll .\Debug\
copy /y SVTServer.dll .\Release\
copy /y SVTServer.dll "C:\Users\Test\Desktop\"