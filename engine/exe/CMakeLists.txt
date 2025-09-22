project(
    newtoast
    VERSION 0.0
    LANGUAGES CXX
)
set(CMAKE_CXX_STANDARD 17) # FIXME revisit

add_executable(newtoast ${CMAKE_CURRENT_SOURCE_DIR}/main.cpp)
target_include_directories(newtoast PRIVATE ${CMAKE_SOURCE_DIR}/lib/include)

add_dependencies(newtoast newtoast-core)

target_link_libraries(newtoast newtoast-core)