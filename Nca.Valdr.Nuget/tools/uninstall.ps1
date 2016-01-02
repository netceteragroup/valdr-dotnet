# Runs every time a package is uninstalled

param($installPath, $toolsPath, $package, $project)

# $installPath is the path to the folder where the package is installed.
# $toolsPath is the path to the tools directory in the folder where the package is installed.
# $package is a reference to the package object.
# $project is a reference to the project the package was installed to.

Import-Module (Join-Path $toolsPath "build.psm1")

function Remove-BuildTasks {
    $solutionDir = Get-SolutionDir
    $tasksToolsPath = (Join-Path $solutionDir ".build")

    if(!(Test-Path $tasksToolsPath)) {
        return
    }

    Write-Host "Removing Nca.Valdr build files from $tasksToolsPath"
    Remove-Item "$tasksToolsPath\Nca.Valdr.exe" | Out-Null
	Remove-Item "$tasksToolsPath\Newtonsoft.Json.dll" | Out-Null

	rmdir $tasksToolsPath | Out-Null
}

Remove-BuildTasks
