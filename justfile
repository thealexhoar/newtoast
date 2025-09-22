
alias b := build
alias r := run

alias br := build-and-run


# set shell := ["bash"]
set windows-shell := ["C:/msys64/usr/bin/bash.exe", ""]

msbuild := "C:/Program Files (x86)/Microsoft Visual Studio/2022/BuildTools/MSBuild/Current/Bin/MSBuild.exe"
cmake_intermediate_target := "Visual Studio 17 2022"

dir_build := "build"
dir_build_engine := dir_build + "/engine"
dir_build_engine_exe := dir_build_engine + "/exe"
dir_build_engine_lib := dir_build_engine + "/lib"
dir_build_framework := dir_build + "/framework"
dir_cmake_src_engine := "engine"

cleanup-build-dir:
    #!C:/msys64/usr/bin/bash.exe
    rm -rf "{{dir_build}}"

build-engine:
    #!C:/msys64/usr/bin/bash.exe
    mkdir -p "{{dir_build_engine}}"
    cmake -S "{{dir_cmake_src_engine}}" -B "{{dir_build_engine}}" -G "{{cmake_intermediate_target}}"
    "{{msbuild}}" {{dir_build_engine}}/Project.sln
    cp {{dir_build_engine_lib}}/Debug/newtoast-core.dll {{dir_build_engine_exe}}/Debug
    cp {{dir_cmake_src_engine}}/deps/lib/nethost.dll {{dir_build_engine_exe}}/Debug

build-framework:
    #!C:/msys64/usr/bin/bash.exe
    mkdir -p "{{dir_build_framework}}"
    dotnet build framework/NT.csproj -o "{{dir_build_framework}}"


build: cleanup-build-dir build-engine build-framework

run:
    #!C:/msys64/usr/bin/bash.exe
    ./"{{dir_build_engine}}"/exe/Debug/newtoast.exe


build-and-run: build run