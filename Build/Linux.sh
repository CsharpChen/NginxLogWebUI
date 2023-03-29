#!/bin/sh
echo "State-Time"
echo $(date)
echo "-------------------------------------------------------------------------------------"
dotnet publish .. -c Release -r linux-x64 -o ./bin/Linux --self-contained false
echo "End-Time"
echo $(date)
echo "-------------------------------------------------------------------------------------"
echo "Publish Success!!!"