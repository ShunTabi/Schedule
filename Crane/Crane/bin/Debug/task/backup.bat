@echo off
pushd %CD%
cd ..\resources\db
if not exist Crane.db_bk%DATE:/=% (
  copy Crane.db Crane.db_bk%DATE:/=%
  echo [Sucess]Backup
) else (
  echo [Error]The file exists.
)