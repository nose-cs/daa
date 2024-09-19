from solution import solve
from tests import run_tests


test_from_terminal = False

if test_from_terminal:
    x = int(input("Enter x: "))
    y = int(input("Enter y: "))
    n, edges = solve(x, y)
    if edges is None:
        print("No such tree exists")
    else:
        print(f"Tree with {n} vertices:")
        for u, v in edges:
            print(f"{u} - {v}")
else:
    tests_count = 1_000_000
    run_tests(1000)
