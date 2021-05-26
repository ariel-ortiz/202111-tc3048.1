#!/usr/bin/env python

# Generic Wat execution script.
# Copyright (C) 2021 Ariel Ortiz, ITESM CEM
#

import sys
from wasmer import engine, Module, wat2wasm, Store, Instance
from wasmer_compiler_cranelift import Compiler
from draclib import make_import_object

def main():

    # Create a store
    store = Store(engine.JIT(Compiler))

    # Convert Wat file contents into Wasm binary code
    wat_file_name = 'example.wat'
    with open(wat_file_name) as wat_file:
        wat_source_code = wat_file.read()
    wasm_bytes = wat2wasm(wat_source_code)

    # Compile the Wasm module
    module = Module(store, wasm_bytes)

    # Obtain functions to be imported from the Wasm module
    import_object = make_import_object(store)

    # Instantiate the module
    instance = Instance(module, import_object)

    # Run start function and return to OS its exit code
    sys.exit(instance.exports.main())

main()
