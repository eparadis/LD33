#!/bin/sh

UNITY="/Applications/Unity 5/Unity.app/Contents/MacOS/Unity"
OPTIONS="-batchmode -quit -nographics"

mkdir Builds

"$UNITY" $OPTIONS -buildOSXPlayer Builds/OSXPlayer.app -logFile Builds/osx_log.txt
"$UNITY" $OPTIONS -buildWebPlayer Builds/WebPlayer -logFile Builds/web_log.txt
"$UNITY" $OPTIONS -buildWindowsPlayer Builds/WindowsPlayer.exe -logFile Builds/windows_log.txt
