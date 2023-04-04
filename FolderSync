param(
    [string]$sourcePath,
    [string]$replicaPath,
    [string]$logPath
)


function Log-Message {
    param(
        [string]$message
    )

    Write-Host $message
    Add-Content $logPath $message
}

if (-not (Test-Path $sourcePath)) {
    Log-Message "Source folder does not exist"
    exit 1
}

if (-not (Test-Path $replicaPath)) {
    Log-Message "Replica folder does not exist, creating it now"
    New-Item -ItemType Directory -Path $replicaPath | Out-Null
}

$sourceFiles = Get-ChildItem -Path $sourcePath -Recurse -File

foreach ($file in $sourceFiles) {
    $replicaFile = Join-Path -Path $replicaPath -ChildPath $file.FullName.Substring($sourcePath.Length)

    if (-not (Test-Path $replicaFile)) {
        Log-Message "Copying file $($file.FullName) to $($replicaFile)"
        Copy-Item -Path $file.FullName -Destination $replicaFile
    }
}

$replicaFiles = Get-ChildItem -Path $replicaPath -Recurse -File

foreach ($file in $replicaFiles) {
    $sourceFile = Join-Path -Path $sourcePath -ChildPath $file.FullName.Substring($replicaPath.Length)

    if (-not (Test-Path $sourceFile)) {
        Log-Message "Deleting file $($file.FullName)"
        Remove-Item -Path $file.FullName
    }
}

Log-Message "Synchronization complete"