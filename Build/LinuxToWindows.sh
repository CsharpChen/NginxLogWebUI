#!/bin/sh
echo "State-Time"
echo $(date)
echo "-------------------------------------------------------------------------------------"
dotnet publish .. -c Release -r win10-x64 -o ./bin/Windows --self-contained false
echo "End-Time"
echo $(date)
echo "-------------------------------------------------------------------------------------"
echo "Publish Success!!!"