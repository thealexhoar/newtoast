// use build_print::println as bpln;

fn main() {
    // bpln!("------------------------");
    // bpln!("-   ENV:   -------------");
    // for (k, v) in std::env::vars() {
    //     bpln!("  {}={}", k, v);
    // }
    // bpln!("------------------------");

    let lib_path_res = std::env::var("NEWTOAST_DEP_LIB_PATH");

    match lib_path_res {
        Ok(lib_path) => {
            println!("cargo:rustc-link-search=native={}", lib_path);
            println!("cargo:rerun-if-changed={}", lib_path);
        },
        Err(_) => {
            panic!("NEWTOAST_DEP_LIB_PATH environment variable not set");
        }
    };

    println!("cargo:rustc-link-lib=dylib=nethost");
    println!("cargo:rustc-link-lib=dylib=SDL3");
    // Re-run build.rs if this file or the library path changes
    println!("cargo:rerun-if-changed=build.rs");
}
