# Runtime LINQ vs PLINQ results

| Elements | LINQ ms | PLINQ ms | Difference |
|---:|---:|---:|---:|
| 1000000 | 33 | 35 | 1,770E-008 |
| 5000000 | 115 | 128 | 1,614E-006 |
| 10000000 | 236 | 42 | 2,813E-006 |

## Side effect test
Expected counter each run: 1000000
Unsafe attempt 1: 190304
Unsafe attempt 2: 232254
Unsafe attempt 3: 200754
Unsafe attempt 4: 150212
Unsafe attempt 5: 89440
Safe counter (Interlocked): 1000000
