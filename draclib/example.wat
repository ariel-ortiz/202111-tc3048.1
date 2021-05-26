(module
  (import "drac" "printi" (func $printi (param i32) (result i32)))
  (import "drac" "println" (func $println (result i32)))
  (func
    (export "main")
    (result i32)

    i32.const 42
    call $printi
    drop

    call $println
    drop

    i32.const 0
  )
)
