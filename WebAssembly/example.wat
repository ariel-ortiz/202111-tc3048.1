;; Simple WebAssembly example.

(module
  (func
    ;; Function signature: i32 duplicate(i32 $x)
    (export "duplicate")
    (param $x i32)
    (result i32)

    ;; Body of the function
    local.get $x
    i32.const 2
    i32.mul
  )
  (func
    (export "times_three_plus_one")
    (param $a f64)
    (result f64)
    local.get $a
    f64.const 3.0
    f64.mul
    i32.const 1
    f64.convert_i32_s
    f64.add
  )
)
