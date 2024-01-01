$destPath = "\AppTemplate.App.Database.Migrations\Migrations"
$outpufFile = "\AppTemplate.App.Database.Migrations\MigrationsScript.sql"

./create-migration-script.ps1 -sourcePath $destPath -destPath $outpufFile