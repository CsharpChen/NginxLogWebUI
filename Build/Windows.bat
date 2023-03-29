@echo off
@echo State-Time
date/t
@echo -------------------------------------------------------------------------------------
dotnet publish .. -c Release -r win10-x64 -o ./bin/Windows --self-contained false
@echo End-Time
date/t
@echo -------------------------------------------------------------------------------------
@echo Publish Success!!!
pause