#!/usr/bin/env python

# Wat execution script.
# Copyright (C) 2021 Ariel Ortiz, ITESM CEM

from wasmer import engine, Module, wat2wasm, Store, Instance
from wasmer import ImportObject, Function
from wasmer_compiler_cranelift import Compiler

def make_import_object(store):

    def pow(base: int, expo: int) -> int:
        return base ** expo

    import_object = ImportObject()
    import_object.register(
        "math",
        {
            "pow": Function(store, pow)
        }
    )
    return import_object

def create_instance(file_name):
    store = Store(engine.JIT(Compiler))
    with open(file_name) as wat_file:
        wat_source_code = wat_file.read()
    module = Module(store, wat2wasm(wat_source_code))
    return Instance(module, make_import_object(store))

def main():
    instance = create_instance('output.wat')
    print(instance.exports.start())

main()
