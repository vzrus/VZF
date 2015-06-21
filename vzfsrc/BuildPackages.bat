@SET FrameworkDir=C:\Windows\Microsoft.NET\Framework\v4.0.30319
@SET FrameworkVersion=v4.0.30319
@SET FrameworkSDKDir=
@SET PATH=%FrameworkDir%;%FrameworkSDKDir%;%PATH%
@SET LANGDIR=EN

msbuild.exe vzf.full.net.sln /p:Configuration=Release /p:Platform="Any CPU" /t:rebuild /p:WarningLevel=0;CreatePackages=true