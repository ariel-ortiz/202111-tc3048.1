# Simple Example using WebAssembly

In order to run a WAT (WebAssembly text format) file you should meet the following software requirements:

* Python 3
* The Wasmer Python package

## Python 3

You’ll need Python version 3.6 or newer. To check that you have the correct version of Python, at the terminal type:

    python -V

**NOTE:** You might need to use the command `python3` instead.

The output should be something like this:

    Python 3.9.5

Go to the [Python website](https://www.python.org/downloads/) if you don’t have Python installed or if it’s older than 3.6.

## Wasmer

The [Wasmer Python package](https://github.com/wasmerio/wasmer-python) brings the required API to execute WebAssembly modules from within a Python runtime system.

To install Wasmer, type at the terminal the following two commands:

    pip install wasmer==1.0.0

    pip install wasmer_compiler_cranelift==1.0.0

**NOTE:** If in the previous section you had to use the `python3` command, then you should use `pip3` here as well. Also, you might need run these commands with admin privileges (for example, prepending `sudo` to the command).

## How to Run

At the terminal type:

    python execute.py <wat-file>

where `<wat-file>` is the name of the source file to execute, such as ` example.wat`.
