project(
    newtoast-core
    VERSION 0.0
    LANGUAGES CXX
)
set(CMAKE_CXX_STANDARD 17) # FIXME revisit

set(SRC_DIR ${CMAKE_CURRENT_SOURCE_DIR}/src)
set(INCLUDE_DIR ${CMAKE_CURRENT_SOURCE_DIR}/include)

file(GLOB_RECURSE SRC_FILES ${SRC_DIR}/*.cpp)

file(GLOB_RECURSE INCLUDE_FILES_HPP ${INCLUDE_DIR}/*.hpp)
file(GLOB_RECURSE INCLUDE_FILES_H ${INCLUDE_DIR}/*.h)


message(STATUS "CMAKE_CURRENT_SOURE_DIR=${CMAKE_CURRENT_SOURCE_DIR}")
message(STATUS "SRC_FILES=${SRC_FILES}")
message(STATUS "INCLUDE_FILES_HPP=${INCLUDE_FILES_HPP}")
message(STATUS "INCLUDE_FILES_H=${INCLUDE_FILES_H}")


add_library(newtoast-core SHARED ${SRC_FILES} ${INCLUDE_FILES_HPP} ${INCLUDE_FILES_H})

target_include_directories(newtoast-core PRIVATE ${INCLUDE_DIR})
target_link_libraries(newtoast-core ${CMAKE_SOURCE_DIR}/deps/lib/nethost.lib)