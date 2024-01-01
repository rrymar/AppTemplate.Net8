 param (
	[string]$project,
	[string]$sourcePath,
	[string]$destPath,
	[string]$contextName,
	[string]$startupProject
 )


$rootPath =  [IO.Path]::GetFullPath((Join-Path $PSScriptRoot "..\.."))
$migrationsPath = $rootPath + $sourcePath
$scriptspath = $rootPath + $destPath

$migrationsPaths = Get-ChildItem -Path $migrationsPath -Exclude *ContextModelSnapshot.cs,*.Designer.cs*,*.sql -Force | Sort-Object Name


Try
{
    pushd $rootPath

	$prevMigration = "0"

	foreach ($migrationPath in $migrationsPaths)
	{
		$migration = $migrationPath.Name.Replace(".cs","")

		$scriptPath = $scriptspath + "\" + $migration + ".sql"

		dotnet ef migrations script $prevMigration $migration -i -o $scriptPath --project $project --startup-project $startupProject --context $contextName

		$prevMigration = $migration

		$newContent = get-content -Path $scriptPath | select -Skip 12 | select -SkipLast 3 #skipping header and footer
		Set-Content -Path $scriptPath -Value ($newContent)
		"Done " + $migration
    }
    popd

}
Catch
{
    $error[0]
    popd
}


