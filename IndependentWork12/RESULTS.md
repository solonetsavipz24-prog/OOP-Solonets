# Runtime LINQ vs PLINQ results

| Elements | LINQ ms | PLINQ ms | Difference |
|---:|---:|---:|---:|
| 1000000 | 32 | 31 | 1,770E-008 |
| 5000000 | 117 | 124 | 1,614E-006 |
| 10000000 | 246 | 43 | 2,813E-006 |

## Side effect test
Expected counter each run: 1000000
Unsafe attempt 1: 181250
Unsafe attempt 2: 234903
Unsafe attempt 3: 121239
Unsafe attempt 4: 146149
Unsafe attempt 5: 195823
Safe counter (Interlocked): 1000000
