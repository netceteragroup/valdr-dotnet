# Runs every time a package is installed in a project

param($installPath, $toolsPath, $package, $project)

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is a reference to the project the package was installed to.

Import-Module (Join-Path $toolsPath "build.psm1")

function Copy-BuildTasks {
	$solutionDir = Get-SolutionDir
	$tasksToolsPath = (Join-Path $solutionDir ".build")

	if(!(Test-Path $tasksToolsPath)) {
		mkdir $tasksToolsPath | Out-Null
	}

	Write-Host "Copying Nca.Valdr files to $tasksToolsPath"
	Copy-Item "$toolsPath\Nca.Valdr.exe" $tasksToolsPath -Force | Out-Null
	Copy-Item "$toolsPath\Newtonsoft.Json.dll" $tasksToolsPath -Force | Out-Null
}

Copy-BuildTasks
