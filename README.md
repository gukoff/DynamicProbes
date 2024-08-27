# DynamicProbes

🚧 **Work in progress** 🚧 [[task board]](https://github.com/users/gukoff/projects/2)

Create USDT probes and instrument your .NET application on Linux.

## How it works

This library is a wrapper around the C library [linux-usdt/libstapsdt](https://github.com/linux-usdt/libstapsdt).

It lets you create USDT probes in runtime and fire them with the int or string arguments.

The resulting probes will be traceable by any USDT tracer, e.g. [bpftrace](https://github.com/bpftrace/bpftrace).

## Prerequisites

[Install libstapsdt](https://github.com/linux-usdt/libstapsdt?tab=readme-ov-file#install).
While this will require building the library from source, the process is very straightforward.
