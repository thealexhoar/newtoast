// build.rs
// Sets up linking to a library at LIB_PATH for rustc

// use build_print::println as bpln;

fn main() {
    // bpln!("------------------------");
    // bpln!("-   ENV:   -------------");
    // for (k, v) in std::env::vars() {
    //     bpln!("  {}={}", k, v);
    // }
    // bpln!("------------------------");

    let nethost_lib_path_res = std::env::var("NETHOST_LIB_PATH");

    match nethost_lib_path_res {
        Ok(nethost_path) => {
            // Tell cargo to tell rustc to link the library at LIB_PATH
            println!("cargo:rustc-link-search=native={}", nethost_path);
            // If you know the library name, e.g., "foo", uncomment and set below:
            println!("cargo:rerun-if-changed={}", nethost_path);
        },
        Err(_) => {
            panic!("NETHOST_LIB_PATH environment variable not set");
        }
    };

    println!("cargo:rustc-link-lib=dylib=nethost");
    // Re-run build.rs if this file or the library path changes
    println!("cargo:rerun-if-changed=build.rs");
}
