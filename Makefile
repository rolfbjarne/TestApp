mono:
	@msbuild /r /nologo /v:quiet /p:Configuration=Debug alc.csproj
	@mono bin/Debug/alc.exe

dotnet:
	dotnet run --project alc-dotnet.csproj

