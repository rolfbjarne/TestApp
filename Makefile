all:
	@msbuild /r /nologo /v:quiet /p:Configuration=Debug
	@mono bin/Debug/alc.exe
