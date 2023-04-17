@SET EXCEL_FOLDER=%1
@SET JSON_FOLDER=%2
@SET CSharp_FOLDER=%3
@SET EXE=.\excel2json\excel2json.exe

@ECHO Converting excel files in folder %EXCEL_FOLDER% ...
for /f "delims=" %%i in ('dir /b /a-d /s %EXCEL_FOLDER%\*.xlsx') do (
    @echo   processing %%~nxi 
    @CALL %EXE% --excel %EXCEL_FOLDER%\%%~nxi --json %JSON_FOLDER%\%%~ni.json --csharp %CSharp_FOLDER%\%%~ni.cs --header 3 --cell_json true
)