
alias b := build
alias c := check
alias cb := clean-build
alias r := run

alias br := build-and-run

set quiet := false
set shell := ["bash", "-eu", "-c"]
set windows-shell := ["C:/Program Files/Git/usr/bin/bash.exe", "-eu", "-c"]

msbuild := "'C:/Program Files (x86)/Microsoft Visual Studio/2022/BuildTools/MSBuild/Current/Bin/MSBuild.exe'"

current_timestamp := `date +%s`

dir_build := "build"
dir_build_engine := dir_build + "/engine"
dir_build_framework := dir_build + "/framework"
dir_build_binaries := dir_build_engine + "/debug"

dir_src_engine := "engine"
dir_src_framework := "framework"

dir_lib := "deps/lib"

full_path_lib := `echo $(pwd)/deps/lib`

path_nethost_dll := dir_lib + "/nethost.dll"
path_sdl_dll := dir_lib + "/SDL3.dll"

file_cargo_toml := dir_src_engine + "/Cargo.toml"
file_framework_csproj := dir_src_framework + "/NT.csproj"

file_buildstamp_engine := dir_build + "/.engine.buildstamp"
file_buildstamp_framework := dir_build + "/.framework.buildstamp"

buildstamp_engine := shell("head -n 1 " + file_buildstamp_engine + " 2> /dev/null || echo '0'")
buildstamp_framework := shell("head -n 1 " + file_buildstamp_framework + " 2> /dev/null || echo '0'")

update_buildstamp_engine := "echo " + current_timestamp + " > " + file_buildstamp_engine
update_buildstamp_framework := "echo " + current_timestamp + " > " + file_buildstamp_framework

# will be used as a regex to exclude files from update-needed checks
update_prunes_engine := "(/target|Cargo.lock)"
update_prunes_framework := "(/bin|/obj)"


_latest_update_engine_full := ("find "+dir_src_engine+" -type f -printf '%T@ %p\\n' | grep -ivE '"+update_prunes_engine+"' | sort -n | tail -n 1 ")
latest_update_engine_full := shell(_latest_update_engine_full)

latest_update_engine := shell("echo " + latest_update_engine_full + " | cut -d '.' -f 1")

_latest_update_framework_full := ("find "+dir_src_framework+" -type f -printf '%T@ %p\\n' | grep -ivE '"+update_prunes_framework+"' | sort -n | tail -n 1 ")
latest_update_framework_full := shell(_latest_update_framework_full)

latest_update_framework := shell("echo " + latest_update_framework_full + " | cut -d '.' -f 1")

file_pattern_dlls := "deps/lib/*.dll"

show-timestamps:
    echo 'current timestamp: {{current_timestamp}}'
    echo 'Engine last update timestamp: {{latest_update_engine_full}}'
    echo 'Engine last build timestamp: {{buildstamp_engine}}'
    echo 'Framework last update timestamp: {{latest_update_framework_full}}'
    echo 'Framework last build timestamp: {{buildstamp_framework}}'


_actually_build_engine := (
    "rm -rf '"+dir_build_engine+"' && " +
    "mkdir -p '"+dir_build_engine+"' && " +
    "export NEWTOAST_DEP_LIB_PATH=\""+full_path_lib+"\" && " +
    "cargo build --manifest-path '"+file_cargo_toml+"' --target-dir '"+dir_build_engine+"' && " +

    update_buildstamp_engine
)

_skip_build_engine := "echo 'No changes in engine source since last build. Skipping build.'"

build-engine:
    if [[ {{buildstamp_engine}} -lt {{latest_update_engine}} ]]; then {{_actually_build_engine}}; else {{_skip_build_engine}} ; fi
    cp -f {{file_pattern_dlls}} {{dir_build_binaries}}

_actually_build_framework := (
    "rm -rf '"+dir_build_framework+"' && " +
    "mkdir -p '"+dir_build_framework+"' && " +
    "dotnet build '"+file_framework_csproj+"' -o '"+dir_build_framework+"' && " +
    update_buildstamp_framework
)

_skip_build_framework := "echo 'No changes in framework source since last build. Skipping build.'"

build-framework:
    if [[ {{buildstamp_framework}} -lt {{latest_update_framework}} ]]; then {{_actually_build_framework}} ; else {{_skip_build_framework}} ; fi


build: build-engine build-framework


clean-engine:
    cd {{dir_src_engine}} && cargo clean

clean-framework:
    cd {{dir_src_framework}} && dotnet clean

clean: clean-engine clean-framework
    rm -rf '{{dir_build}}'
clean-build: clean build

run:
    env RUST_BACKTRACE=1 ./'{{dir_build_engine}}'/debug/newtoast.exe


build-and-run: build run


check:
    export NETHOST_LIB_PATH="$(pwd)/deps/lib/"
    cargo check --manifest-path '{{dir_src_engine}}'/Cargo.toml


build-sdl:
    mkdir -p deps/SDL/build
    cmake -S deps/SDL -B deps/SDL/build
    {{msbuild}} deps/SDL/build/SDL3.sln
    cp deps/SDL/build/Debug/SDL3.dll deps/lib/SDL3.dll
    cp deps/SDL/build/Debug/SDL3.lib deps/lib/SDL3.lib