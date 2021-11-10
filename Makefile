all:
	@msbuild /r /nologo /v:quiet
	@mono bin/Debug/alc.exe
