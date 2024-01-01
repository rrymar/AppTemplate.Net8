 param (
	[string]$sourcePath,
	[string]$destPath
 )

$createMigrationHistorySQL = "IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
	CREATE TABLE [__EFMigrationsHistory] (
		[MigrationId] nvarchar(150) NOT NULL,
		[ProductVersion] nvarchar(32) NOT NULL,
		CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
	);
END;
";

$startTemplate = "SET ANSI_PADDING ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION
";

$endTemplate = "IF @@TRANCOUNT > 0
	COMMIT TRANSACTION
ELSE
	PRINT 'Transaction should be rolled back by XACT_ABORT property'
GO
";

$replacePatterntFrom = "IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory]";
$replacePatterntTo = "IF @@TRANCOUNT>0 AND NOT EXISTS(SELECT * FROM [__EFMigrationsHistory]";

$rootPath = [System.IO.Path]::GetFullPath((Join-Path $PSScriptRoot ..\..))
$migrationsPath = $rootPath + $sourcePath
$outputPath = $rootPath + $destPath

Write-Host $migrationsPath

$migrationsPaths = Get-ChildItem -Path $migrationsPath -Filter *.sql -Force | Sort-Object Name

New-Item $outputPath -type file -force

Add-Content $outputPath $createMigrationHistorySQL

Add-Content $outputPath $startTemplate
foreach ($migrationPath in $migrationsPaths)
{  
   $content = Get-Content -Path $migrationPath.FullName | % {$_.replace($replacePatterntFrom,$replacePatterntTo)}
   Add-Content $outputPath $content -Encoding UTF8
}
Add-Content $outputPath $endTemplate


Write-Host "Finished"
Write-Host $outputPath



