dotnet build ../../Oxygen-Dapr.EshopSample.sln
REM docker run -dit -p 80:80 -v /run/desktop/mnt/host/e/dotnet_project/Oxygen-Dapr.EshopSample/Services/AccountService/Host/bin/Debug/net5.0:/app -v /run/desktop/mnt/host/c/Users/Administrator/vsdbg/vs2017u5:/remote_debugger:rw accountservice:debug
docker run -dit -p 80:80 -v /run/desktop/mnt/host/e/dotnet_project/Oxygen-Dapr.EshopSample/Services/ImageService/bin/Debug/net5.0:/app -v /run/desktop/mnt/host/c/Users/Administrator/vsdbg/vs2017u5:/remote_debugger:rw imageservice:debug