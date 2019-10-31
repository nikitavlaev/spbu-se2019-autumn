cmake_minimum_required(VERSION 3.15)
project(Task01 C)

set(CMAKE_C_STANDARD 11)
set(CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -fopenmp")

add_executable(Task01 main.c gauss_algos.h gauss_algos.c)

find_package(GSL REQUIRED)    # See below (2)
target_link_libraries(Task01 GSL::gsl GSL::gslcblas)