@echo off
@echo State-Time
date/t
@echo -------------------------------------------------------------------------------------
dotnet publish .. -c Release -r linux-x64 -o ./bin/Linux --self-contained false
@echo End-Time
date/t
@echo -------------------------------------------------------------------------------------
@echo Publish Success!!!
pause