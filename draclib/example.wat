(module
  (import "drac" "printi" (func $printi (param i32) (result i32)))
  (import "drac" "println" (func $println (result i32)))
  (import "drac" "reads" (func $reads (result i32)))
  (import "drac" "size" (func $size (param i32) (result i32)))
  (import "drac" "get" (func $get (param i32 i32) (result i32)))
  (import "drac" "printc" (func $printc (param i32) (result i32)))
  (import "drac" "prints" (func $prints (param i32) (result i32)))
  (import "drac" "new" (func $new (param i32) (result i32)))
  (import "drac" "add" (func $add (param i32 i32) (result i32)))

  (func
    (export "main")
    (result i32)
    (local $data i32)
    (local $_temp i32)

    i32.const 42
    call $printi
    drop

    call $println
    drop

    call $reads
    local.set $data

    local.get $data
    call $printi
    drop

    call $println
    drop

    local.get $data
    call $size
    call $printi
    drop

    call $println
    drop

    ;; local.get $data
    local.get $data
    i32.const 0
    call $get
    call $printc
    drop

    call $println
    drop

    ;; Código de Drac
    ;;                 prints("hola");

    i32.const 0
    call $new
    local.set $_temp

    local.get $_temp
    local.get $_temp
    local.get $_temp
    local.get $_temp
    local.get $_temp

    i32.const 104 ;; h
    call $add
    drop

    i32.const 111 ;; o
    call $add
    drop

    i32.const 108 ;; l
    call $add
    drop

    i32.const 97 ;; a
    call $add
    drop

    call $prints
    drop

    i32.const 0
  )
)
