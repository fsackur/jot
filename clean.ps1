
$ErrorActionPreference = "Stop"

$ToClean = "bin", "obj" #, "paket.lock", "paket.dependencies"

pushd $PSScriptRoot

try {
    foreach ($Path in (".", "src/Shared", "src/Server", "src/Client", "src/Client/.elmish-land/Base")) {
        pushd $Path
        try {
            $ToClean | ? {Test-Path $_} | % {"$_/*"} | del -Recurse -Force
        } finally {
            popd
        }
    }
} finally {
    popd
}
