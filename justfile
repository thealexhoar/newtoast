
alias b := build
alias c := check
alias cb := clean-build
alias r := run

alias br := build-and-run


# set shell := ["bash"]
set windows-shell := ["C:/msys64/usr/bin/bash.exe", ""]

dir_build := "build"
dir_build_engine := dir_build + "/engine"
dir_build_framework := dir_build + "/framework"

dir_src_engine := "engine"

cleanup-build-dir:
    #!C:/msys64/usr/bin/bash.exe
    set -euxo pipefail
    rm -rf "{{dir_build}}"

build-engine:
    #!C:/msys64/usr/bin/bash.exe
    set -euxo pipefail
    mkdir -p "{{dir_build_engine}}"
    export NETHOST_LIB_PATH="$(pwd)/deps/lib/"
    cargo build --manifest-path "{{dir_src_engine}}"/Cargo.toml --target-dir "{{dir_build_engine}}"
    cp deps/lib/nethost.dll {{dir_build_engine}}/debug


build-framework:
    #!C:/msys64/usr/bin/bash.exe
    set -euxo pipefail
    mkdir -p "{{dir_build_framework}}"
    dotnet build framework/NT.csproj -o "{{dir_build_framework}}"


build: build-engine build-framework
clean-build: cleanup-build-dir build

run:
    #!C:/msys64/usr/bin/bash.exe
    set -euxo pipefail
    ./"{{dir_build_engine}}"/debug/newtoast.exe


build-and-run: build run


check:
    #!C:/msys64/usr/bin/bash.exe
    set -euxo pipefail
    export NETHOST_LIB_PATH="$(pwd)/deps/lib/"
    cargo check --manifest-path "{{dir_src_engine}}"/Cargo.toml